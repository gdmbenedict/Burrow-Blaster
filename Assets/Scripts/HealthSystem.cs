using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Varaibles")]
    [SerializeField] private int maxHealth;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //die
        }
        //implement something to show taking damage
    }

    public void HealDamage(int healing)
    {
        health += healing;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void ModifyMaxHealth(int maxHealthChange)
    {
        maxHealth += maxHealthChange;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
