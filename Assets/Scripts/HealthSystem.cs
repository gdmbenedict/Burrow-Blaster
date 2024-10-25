using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Varaibles")]
    [SerializeField] private int maxHealth;
    private int health;

    [Header("Shield Variables")]
    [SerializeField] private GameObject shieldModel;
    [SerializeField] private float shieldCooldown;
    [SerializeField] private float shieldTransition;
    [SerializeField] private bool hasShield;
    private bool shieldUnlocked;
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        /*
        if (shieldModel != null)
        {
            shieldModel.GetComponent<MeshRenderer>().enabled = false;
        }
        */
        
    }

    public void TakeDamage(int damage)
    {
        if(!hasShield)
        {
            //taking damage

            health -= damage;
            if (health <= 0)
            {
                Enemy enemy = GetComponent<Enemy>();

                if (enemy != null)
                {
                    //Debug.Log("Calling Die Function");
                    enemy.Die();
                }
                else
                {
                    Player player = GetComponent<Player>();
                    player.Die();
                }
            }
            //implement something to show taking damage
        }

        if (shieldUnlocked)
        {
            StopAllCoroutines(); //disable active shield cooldown co-routine
            StartCoroutine(BreakShield());
        } 
    }

    public void SetShield(bool shieldUnlocked)
    {
        this.shieldUnlocked = shieldUnlocked;

        if (shieldUnlocked)
        {
            Debug.Log("Setting Shield On");
            hasShield = true;
            shieldModel.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            Debug.Log("Setting Shield off");
            hasShield = false;
            shieldModel.GetComponent<MeshRenderer>().enabled = false;
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

    private IEnumerator BreakShield()
    {
        if (hasShield)
        {
            hasShield = false;
            shieldModel.GetComponent<MeshRenderer>().enabled = false;

            //shield break SFX
        }

        //reset shield cooldown timer
        float timer = 0f;

        while (timer < shieldCooldown)
        {
            yield return null;
            timer += Time.deltaTime;
        }

        StartCoroutine(ActivateShield());
    }

    private IEnumerator ActivateShield()
    {
        hasShield = true;
        Vector3 originScale = shieldModel.transform.localScale;
        shieldModel.transform.localScale = Vector3.zero;
        shieldModel.GetComponent<MeshRenderer>().enabled = true;
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
}
