using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    //Types of entities
    public enum EntityType
    {
        player,
        enemy,
        boss
    }

    [Header("Object References")]
    [SerializeField] private GameObject model; //model of entity
    [SerializeField] private ParticleSystem hitParticles; //particle system for hits
    [SerializeField] private GameObject explosion; //explosion prefab

    [Header("Health Varaibles")]
    [SerializeField] private int maxHealth; //max health of entity
    private int health; //current health of entity
    private float chipDamage =0; //stored chip damage of entity

    [Header("Invulnerability Variables")]
    [SerializeField] private float invulnerabilityTime; //time entity has in invulnerability mode
    [SerializeField] private float flashingIntervals; //how many time the entity flashes in invulnerability
    private bool canTakeDamage; //bool determining if entity can take damage

    [Header("Shield Variables")]
    [SerializeField] private GameObject shieldmodel; //model for the shield of entity
    [SerializeField] private float shieldCooldown; //shield cooldown time
    [SerializeField] private float shieldTransition; //shield activation transition time
    [SerializeField] private bool hasShield; //bool handling if shield is active
    private bool shieldUnlocked; //bool determining if shield is unlocked
    private Coroutine shieldCoroutine; //holder for active coroutine

    [Header("Entity Type")]
    [SerializeField] private EntityType entityType; //enum that determines the type of entity the health system is attached to
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        canTakeDamage = true;
    }

    //Function that handles taking single time damage
    public void TakeDamage(int damage)
    {
        //Debug.Log("TakeDamage() called");

        //exit if entity can't take damage
        if (!canTakeDamage) return;

        //no damage if shield is active
        if (!hasShield)
        {
            //taking damage
            health -= damage;
            if (health <= 0)
            {
                HandleEntityDeath();
            }

            //playing particle hit effect
            hitParticles.Play();

            //start invulneravility period
            if (invulnerabilityTime > 0)
            {
                StartCoroutine(Invulnerability());
            }
        }

        //Handle shield functions
        HandleShield();
    }

    //Function to handle damage that is repeted over time
    public void TakeChipDamage(float damage)
    {
        //exit if entity can't take damage
        if (!canTakeDamage) return;

        //no damage if shield is active
        if (!hasShield)
        {
            //storing damage
            chipDamage += damage;
            if (chipDamage >= 1)
            {
                //applying damage
                health -= (int)chipDamage;
                if (health <= 0)
                {
                    HandleEntityDeath();
                }

                //playing particle hit effect
                hitParticles.Play();

                chipDamage = 0;
            }
        }

        //Handle shield functions
        HandleShield();
    }

    //Function that handles death of the entity
    private void HandleEntityDeath()
    {
        //Call die function on correct entity type
        switch (entityType)
        {
            case EntityType.enemy:
                GetComponent<Enemy>()?.Die(explosion);
                break;
            case EntityType.boss:
                GetComponent<Boss>()?.Die(explosion);
                break;
            case EntityType.player:
                GetComponent<Player>()?.Die(explosion);
                break;
            default:
                Debug.LogWarning("Unhandled EntityType in HandleEntityDeath");
                break;
        }
    }

    //Function for healing damage taken by entity
    public void HealDamage(int healing)
    {
        health = Mathf.Min(health + healing, maxHealth);
    }

    //Function that returns the current health of entity
    public int GetHealth()
    {
        return health;
    }

    //Function that sets the Max Health of entity
    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    //Function that returns max health of entity
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    // Function that returns if entity is currently invulnerable
    public bool GetInvulnerable()
    {
        return canTakeDamage;
    }

    // Function that sets if entity can take damage
    public void SetTakeDamage(bool takeDamage)
    {
        canTakeDamage = takeDamage;
    }

    //Function that handles shield function
    private void HandleShield()
    {
        //check if shield is unlocked
        if (shieldUnlocked)
        {
            //stop any active shield coroutine and start new coroutine
            if (shieldCoroutine != null)
            {
                StopCoroutine(shieldCoroutine);
            }
            shieldCoroutine = StartCoroutine(BreakShield());
        }
    }

    //Function that sets the shield
    public void SetShield(bool shieldUnlocked)
    {
        this.shieldUnlocked = shieldUnlocked;

        //set visual to correct activation
        if (shieldUnlocked)
        {
            //Debug.Log("Setting Shield On");
            hasShield = true;
            shieldmodel.SetActive(true);
        }
        else
        {
            //Debug.Log("Setting Shield off");
            hasShield = false;
            shieldmodel.SetActive(false);
        }
    }

    //Function that breaks shield
    private IEnumerator BreakShield()
    {
        //deactivate shield if activated
        if (hasShield)
        {
            hasShield = false;
            shieldmodel.SetActive(false);
            //shield break SFX
        }

        //reset shield cooldown timer
        float timer = 0f;

        //wait for shield cooldown time
        yield return new WaitForSeconds(shieldCooldown);

        //start activation 
        StartCoroutine(ActivateShield());
        shieldCoroutine = null;
    }

    //Function that grows shield from 0 scale to 1
    private IEnumerator ActivateShield()
    {
        //Debug.Log("ActivateShield Called");

        //setting initial conditions
        hasShield = true;
        Vector3 originScale = shieldmodel.transform.localScale;
        shieldmodel.transform.localScale = Vector3.zero;
        shieldmodel.SetActive(true);
        float growtimer = 0f;

        //shieled activation SFX here

        //looping thorugh transition time.
        while (growtimer < shieldTransition)
        {
            //Lerping from zero to desired scale
            shieldmodel.transform.localScale = Vector3.Lerp(Vector3.zero, originScale, growtimer/shieldTransition);
            growtimer += Time.deltaTime;
            yield return null;
        }

        //snap to original scale
        shieldmodel.transform.localScale = originScale;      
    }

    // Function that grants invulnerability to entity and makes model flash for invulnerability period
    private IEnumerator Invulnerability()
    {
        //set initial conditions
        float invulnTimer = 0;
        float flashIntervalTime = invulnerabilityTime / flashingIntervals;
        canTakeDamage = false;

        //loop while inder the invulnerability timer
        while (invulnTimer < invulnerabilityTime)
        {
            //toggle model and wait for interval time
            model.SetActive(!model.activeSelf);
            yield return new WaitForSeconds(flashIntervalTime);
            invulnTimer += flashIntervalTime;
        }

        //return entity to normal conditions
        canTakeDamage = true;
        model.SetActive(true);
    }
}
