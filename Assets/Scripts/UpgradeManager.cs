using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Upgrade Increments")]
    [SerializeField] private int spreadShotIncrement = 1;
    [SerializeField] private float fireRateIncrement = 0.25f;
    [SerializeField] private int piercingIncrement = 1;
    [SerializeField] private float damageIncrement = 0.5f;
    [SerializeField] private float movementSpeedIncrement = 0.25f;
    [SerializeField] private float fuelIncrement = 1;
    [SerializeField] private float collectionRangeIncrement = 1;
    [SerializeField] private float collectionMultIncrement = 0.25f;
    [SerializeField] private int maxHealthIncrement = 2;

    [Header("Special Upgrades")]
    [SerializeField] private bool dodge = false; //ability to dodge to reposition quickly / avoid projectiles
    [SerializeField] private bool shield = false; //ability to receive temporary shield on avoiding damage for set period
    [SerializeField] private bool sideShots = false; //ability to fire from the side of your character
    [SerializeField] private bool superLaser = false; //ability to use the super laser weapon

    [Header("Weapon Upgrades")]
    [SerializeField] private int baseSpreadShot = 1;
    private int spreadShot; //firing additional projectiles
    [SerializeField] private float baseFireRate = 1; 
    private float fireRate; //speed at which projectiles are fired multiplier
    [SerializeField] private int basePiercing = 1;
    private int piercing; //how many enemies projectiles pierce through
    [SerializeField] private float baseDamage = 1; 
    private float damageMult; //damage done by projectiles

    [Header("Movement Upgrades")]
    [SerializeField] private float baseMovementSpeed = 1;
    private float movementSpeed; //movement speed multiplier
    [SerializeField] private float baseFuel = 1;
    private float fuel; //fuel duration multiplier

    [Header("Collection Upgrades")]
    [SerializeField] private float baseCollectionRange = 1;
    private float collectionRange; //collection range multiplier
    [SerializeField] private float baseCollectionMult = 1;
    private float collectionMult; //collection amount multiplier

    [Header("Other Upgrades")]
    [SerializeField] private int baseMaxHealth = 3;
    private int maxHealth; //max amounts of hits that can be taken

    //Upgrade levels array
    private int[] upgradeLevels = new int[13];

    // Start is called before the first frame update
    void Start()
    {
        //setting attributes to base levels
        spreadShot = baseSpreadShot;
        fireRate = baseFireRate;
        piercing = basePiercing;
        damageMult = baseDamage;
        movementSpeed = baseMovementSpeed;
        fuel = baseFuel;
        collectionRange = baseCollectionRange;
        collectionMult = baseCollectionMult;
        maxHealth = baseMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Method that sets all attributes according to a passed in upgrade levels array
    public void SetUpgradeLevels(int[] upgradeLevels)
    {
        this.upgradeLevels = upgradeLevels;

        //dodge
        if (upgradeLevels[0] == 1)
        {
            dodge = true;
        }
        else
        {
            dodge = false;
        }

        //shield
        if (upgradeLevels[1] == 1)
        {
            shield = true;
        }
        else
        {
            shield = false;
        }

        //side shots
        if (upgradeLevels[2] == 1)
        {
            sideShots = true;
        }
        else
        {
            sideShots= false;
        }

        //super laser
        if (upgradeLevels[3] == 1)
        {
            superLaser = true;
        }
        else
        {
            superLaser = false;
        }

        //spread shot
        if (upgradeLevels[4] >= 0 || upgradeLevels[4] <= 4)
        {
            spreadShot = baseSpreadShot + upgradeLevels[4] * spreadShotIncrement;
        }
        else
        {
            spreadShot = baseSpreadShot;
        }

        //fire rate
        if (upgradeLevels[5] >= 0 || upgradeLevels[5] <= 4)
        {
            fireRate = baseFireRate + upgradeLevels[5] * fireRateIncrement;
        }
        else
        {
            fireRate= baseFireRate;
        }

        //piercing
        if (upgradeLevels[6] >= 0 || upgradeLevels[6] <= 4)
        {
            piercing = basePiercing + upgradeLevels[6] * piercingIncrement;
        }
        else
        {
            piercing= basePiercing;
        }

        //damage
        if (upgradeLevels[7] >= 0 || upgradeLevels[7] <= 4)
        {
            damageMult = baseDamage + upgradeLevels[7] * damageIncrement;
        }
        else
        {
            damageMult= baseDamage;
        }

        //movement speed
        if (upgradeLevels[8] >= 0 || upgradeLevels[8] <= 4)
        {
            movementSpeed = baseMovementSpeed + upgradeLevels[8] * movementSpeedIncrement;
        }
        else
        {
            movementSpeed= baseMovementSpeed;
        }

        //fuel
        if (upgradeLevels[9] >= 0 || upgradeLevels[9] <= 4)
        {
            fuel = baseFuel + upgradeLevels[9] * fuelIncrement;
        }
        else
        {
            fuel= baseFuel;
        }

        //collection range
        if (upgradeLevels[10] >= 0 || upgradeLevels[10] <= 4)
        {
            collectionRange = baseCollectionRange + upgradeLevels[10] * collectionRangeIncrement;
        }
        else
        {
            collectionRange= baseCollectionRange;
        }

        //collection multiplier
        if (upgradeLevels[11] >= 0 || upgradeLevels[11] <= 4)
        {
            collectionMult = baseCollectionMult + upgradeLevels[11] * collectionMultIncrement;
        }
        else
        {
            collectionMult= baseCollectionMult;
        }

        //max health
        if (upgradeLevels[12] >= 0 || upgradeLevels[12] <= 4)
        {
            maxHealth = baseMaxHealth + upgradeLevels[12] * maxHealthIncrement;
        }
        else
        {
            maxHealth= baseMaxHealth;
        }
    }

    //Accessor method used to get the full array of upgrade levels
    public int[] GetUpgradeLevels()
    {
        return upgradeLevels;
    }

    //Mutator method that sets if this ability is accessible 
    public void SetDodge(bool active)
    {
        dodge = active;

        if (dodge)
        {
            upgradeLevels[0] = 1;
        }
        else
        {
            upgradeLevels[0] = 0;
        }
    }

    //Accessor method that returns if the ability is accessible
    public bool GetDodge()
    {
        return dodge;
    }

    //Accessor method that returns the upgrade level of the ability
    public int GetDodgeUpgradeLevel()
    {
        return upgradeLevels[0];
    }

    //Mutator method that sets if this ability is accessible 
    public void SetShield(bool active)
    {
        shield = active;

        if (shield)
        {
            upgradeLevels[1] = 1;
        }
        else
        {
            upgradeLevels[1] = 0;
        }
    }

    //Accessor method that returns if the ability is accessible
    public bool GetShield()
    {
        return shield;
    }

    //Accessor method that returns the upgrade level of the ability
    public int GetShieldUpgradeLevel()
    {
        return upgradeLevels[1];
    }

    //Mutator method that sets if this ability is accessible 
    public void SetSideShots(bool active)
    {
        sideShots = active;

        if (sideShots)
        {
            upgradeLevels[2] = 1;
        }
        else
        {
            upgradeLevels[2] = 0;
        }
    }

    //Accessor method that returns if the ability is accessible
    public bool GetSideShots()
    {
        return sideShots;
    }

    //Accessor method that returns the upgrade level of the ability
    public int GetSideShotsUpgradeLevel()
    {
        return upgradeLevels[2];
    }

    //Mutator method that sets if this ability is accessible 
    public void SetSuperLaser(bool active)
    {
        superLaser = active;

        if (superLaser)
        {
            upgradeLevels[3] = 1;
        }
        else
        {
            upgradeLevels[3] = 0;
        }
    }

    //Accessor method that returns if the ability is accessible
    public bool GetSuperLaser()
    {
        return superLaser;
    }

    //Accessor method that returns the upgrade level of the ability
    public int GetSuperLaserUpgradeLevel()
    {
        return upgradeLevels[3];
    }

    //Mutator method that increments the attribute by one increment
    public void UpgradeSpreadShot()
    {
        if (upgradeLevels[4] <= 3)
        {
            upgradeLevels[4]++;
            spreadShot += spreadShotIncrement;
        }
    }

    //Accessor method that returns the attribute value
    public int GetSpreadShot()
    {
        return spreadShot;
    }

    //Accessor method that returns the attribute upgrade level
    public int GetSpreadShotUpgradeLevel()
    {
        return upgradeLevels[4];
    }

    //Mutator method that increments the attribute by one increment
    public void UpgradeFireRate()
    {
        if (upgradeLevels[5] <= 3)
        {
            upgradeLevels[5]++;
            fireRate += fireRateIncrement;
        }
    }

    //Accessor method that returns the attribute value
    public float GetFireRate()
    {
        return fireRate;
    }

    //Accessor method that returns the attribute upgrade level
    public int GetFireRateUpgradeLevel()
    {
        return upgradeLevels[5];
    }

    //Mutator method that increments the attribute by one increment
    public void UpgradePiercing()
    {
        if (upgradeLevels[6] <= 3)
        {
            upgradeLevels[6]++;
            piercing += piercingIncrement;
        }
    }

    //Accessor method that returns the attribute value
    public int GetPiercing()
    {
        return piercing;
    }

    //Accessor method that returns the attribute upgrade level
    public int GetPiercingUpgradeLevel()
    {
        return upgradeLevels[6];
    }

    //Mutator method that increments the attribute by one increment
    public void UpgradeDamage()
    {
        if (upgradeLevels[7] <= 3)
        {
            upgradeLevels[7]++;
            damageMult += damageIncrement;
        }
    }

    //Accessor method that returns the attribute value
    public float GetDamage()
    {
        return damageMult;
    }

    //Accessor method that returns the attribute upgrade level
    public int GetDamageUpgradeLevel()
    {
        return upgradeLevels[7];
    }

    //Mutator method that increments the attribute by one increment
    public void UpgradeMovementSpeed()
    {
        if (upgradeLevels[8] <= 3)
        {
            upgradeLevels[8]++;
            movementSpeed += movementSpeedIncrement;
        }
    }

    //Accessor method that returns the attribute value
    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    //Accessor method that returns the attribute upgrade level
    public int GetMovementSpeedUpgradeLevel()
    {
        return upgradeLevels[8];
    }

    //Mutator method that increments the attribute by one increment
    public void UpgradeFuel()
    {
        if (upgradeLevels[9] <= 3)
        {
            upgradeLevels[9]++;
            fuel += fuelIncrement;
        }
    }

    //Accessor method that returns the attribute value
    public float GetFuel()
    {
        return fuel;
    }

    //Accessor method that returns the attribute upgrade level
    public int GetFuelUpgradeLevel()
    {
        return upgradeLevels[9];
    }

    //Mutator method that increments the attribute by one increment
    public void UpgradeCollectionRange()
    {
        if (upgradeLevels[10] <= 3)
        {
            upgradeLevels[10]++;
            collectionRange += collectionRangeIncrement;
        }
    }

    //Accessor method that returns the attribute value
    public float GetCollectionRange()
    {
        return collectionRange;
    }

    //Accessor method that returns the attribute upgrade level
    public int GetCollectionRangeUpgradeLevel()
    {
        return upgradeLevels[10];
    }

    //Mutator method that increments the attribute by one increment
    public void UpgradeCollectionMult()
    {
        if (upgradeLevels[11] <= 3)
        {
            upgradeLevels[11]++;
            collectionMult += collectionMultIncrement;
        }
    }

    //Accessor method that returns the attribute value
    public float GetCollectionMult()
    {
        return collectionMult;
    }

    //Accessor method that returns the attribute upgrade level
    public int GetCollectionMultUpgradeLevel()
    {
        return upgradeLevels[11];
    }

    //Mutator method that increments the attribute by one increment
    public void UpgradeMaxHealth()
    {
        if (upgradeLevels[12] <= 3)
        {
            upgradeLevels[12]++;
            maxHealth += maxHealthIncrement;
        }
    }

    //Accessor method that returns the attribute value
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    //Accessor method that returns the attribute upgrade level
    public int GetMaxHealthUpgradeLevel()
    {
        return upgradeLevels[12];
    }
}
