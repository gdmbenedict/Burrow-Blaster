using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public enum EntityType
    {
        player,
        enemy,
        boss
    }

    [Header("Object References")]
    [SerializeField] private GameObject Model;
    [SerializeField] private ParticleSystem hitParticles;
    [SerializeField] private GameObject explosion;

    [Header("Health Varaibles")]
    [SerializeField] private int maxHealth;
    private int health;
    private float chipDamage =0;

    [Header("Invulnerability Variables")]
    [SerializeField] private float invulnerabilityTime;
    [SerializeField] private float flashingIntervals;
    private bool canTakeDamage;

    [Header("Shield Variables")]
    [SerializeField] private GameObject shieldModel;
    [SerializeField] private float shieldCooldown;
    [SerializeField] private float shieldTransition;
    [SerializeField] private bool hasShield;
    private bool shieldUnlocked;

    [Header("Entity Type")]
    [SerializeField] private EntityType entityType;
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        canTakeDamage = true;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage() called");

        if (canTakeDamage)
        {
            if (!hasShield)
            {
                //taking damage
                health -= damage;
                if (health <= 0)
                {
                    if (entityType == EntityType.enemy)
                    {
                        //Debug.Log("Calling Die Function");
                        Enemy enemy = GetComponent<Enemy>();
                        enemy.Die(explosion);
                        return;
                    }
                    else if (entityType == EntityType.boss)
                    {
                        Boss boss = GetComponent<Boss>();
                        boss.Die(explosion);
                        return;
                    }
                    else
                    {
                        Player player = GetComponent<Player>();
                        player.Die(explosion);
                        return;
                    }
                }

                hitParticles.Play();

                if (invulnerabilityTime > 0)
                {
                    StartCoroutine(Invulnerability());
                }


            }

            if (shieldUnlocked)
            {
                StopAllCoroutines();
                StartCoroutine(BreakShield());
            }
        }
    }

    public void TakeChipDamage(float damage)
    {
        if (canTakeDamage)
        {
            if (!hasShield)
            {
                //taking damage
                chipDamage += damage;
                if (chipDamage >= 1)
                {
                    health -= (int)chipDamage;
                    if (health <= 0)
                    {
                        if (entityType == EntityType.enemy)
                        {
                            //Debug.Log("Calling Die Function");
                            Enemy enemy = GetComponent<Enemy>();
                            enemy.Die(explosion);
                        }
                        else if (entityType == EntityType.boss)
                        {
                            Boss boss = GetComponent<Boss>();
                            boss.Die(explosion);
                        }
                        else
                        {
                            Player player = GetComponent<Player>();
                            player.Die(explosion);
                        }
                    }

                    //implement something to show taking damage

                    chipDamage = 0;
                }
            }

            if (shieldUnlocked)
            {
                StopAllCoroutines();
                StartCoroutine(BreakShield());
            }
        }
        
    }

    public void SetShield(bool shieldUnlocked)
    {
        this.shieldUnlocked = shieldUnlocked;

        if (shieldUnlocked)
        {
            Debug.Log("Setting Shield On");
            hasShield = true;
            shieldModel.SetActive(true);
        }
        else
        {
            Debug.Log("Setting Shield off");
            hasShield = false;
            shieldModel.SetActive(false);
        }
    }

    public void HealDamage(int healing)
    {
        health += healing;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public int GetHealth()
    {
        return health;
    }

    public void ModifyMaxHealth(int maxHealthChange)
    {
        maxHealth += maxHealthChange;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public bool GetInvulnerable()
    {
        return canTakeDamage;
    }

    public void SetTakeDamage(bool takeDamage)
    {
        canTakeDamage = takeDamage;
    }

    private IEnumerator BreakShield()
    {
        if (hasShield)
        {
            hasShield = false;
            shieldModel.SetActive(false);

            //shield break SFX
        }

        //reset shield cooldown timer
        float timer = 0f;

        while (timer < shieldCooldown)
        {
            Debug.Log(timer);
            yield return null;
            timer += Time.deltaTime;       
        }

        StartCoroutine(ActivateShield());
    }

    private IEnumerator ActivateShield()
    {
        Debug.Log("ActivateShield Called");

        hasShield = true;
        Vector3 originScale = shieldModel.transform.localScale;
        shieldModel.transform.localScale = Vector3.zero;
        shieldModel.SetActive(true);
        //shiled activation SFX

        float growtimer = 0f;
        while (growtimer < shieldTransition)
        {
            shieldModel.transform.localScale = Vector3.Lerp(Vector3.zero, originScale, growtimer/shieldTransition);
            growtimer += Time.deltaTime;

            yield return null;
        }

        shieldModel.transform.localScale = originScale;      
    }

    private IEnumerator Invulnerability()
    {
        float invulnTimer = 0;
        float flashingTimer = 0;
        float flashIntervalTime = invulnerabilityTime / flashingIntervals;
        canTakeDamage = false;
        Model.SetActive(false);

        while (invulnTimer < invulnerabilityTime)
        {
            invulnTimer += Time.deltaTime;
            flashingTimer += Time.deltaTime;

            if (flashingTimer >= flashIntervalTime)
            {
                Model.SetActive(!Model.activeSelf);
                flashingTimer = 0;
            }

            yield return null;
        }

        canTakeDamage = true;
        Model.SetActive(true);
    }
}
