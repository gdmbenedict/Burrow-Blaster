using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualsManager : MonoBehaviour
{

    [Header("Special Upgrades")]
    [SerializeField] private GameObject[] shieldVisuals;
    [SerializeField] private GameObject[] dodgeVisuals;
    [SerializeField] private GameObject[] sideShotVisuals;
    [SerializeField] private GameObject[] superLaserVisuals;

    [Header("Spreadshot Upgrades")]
    [SerializeField] private List<GameObject[]> allSpreadShotVisuals;
    [SerializeField] private GameObject[] spreadShotVisuals0;
    [SerializeField] private GameObject[] spreadShotVisuals1;
    [SerializeField] private GameObject[] spreadShotVisuals2;
    [SerializeField] private GameObject[] spreadShotVisuals3;
    [SerializeField] private GameObject[] spreadShotVisuals4;

    [Header("FireRate Upgrades")]
    [SerializeField] private List<GameObject[]> allFireRateVisuals;
    [SerializeField] private GameObject[] fireRateVisuals1;
    [SerializeField] private GameObject[] fireRateVisuals2;
    [SerializeField] private GameObject[] fireRateVisuals3;
    [SerializeField] private GameObject[] fireRateVisuals4;

    [Header("Piercing Upgrades")]
    [SerializeField] private GameObject gunBarrel;
    [SerializeField] private float yPosPiercingVisual0;
    [SerializeField] private float yPosPiercingVisual1;
    [SerializeField] private float yPosPiercingVisual2;
    [SerializeField] private float yPosPiercingVisual3;
    [SerializeField] private float yPosPiercingVisual4;

    [Header("Damage Upgrades")]
    [SerializeField] private List<GameObject[]> allDamageVisuals;
    [SerializeField] private GameObject[] damageVisuals0;
    [SerializeField] private GameObject[] damageVisuals1;
    [SerializeField] private GameObject[] damageVisuals2;
    [SerializeField] private GameObject[] damageVisuals3;
    [SerializeField] private GameObject[] damageVisuals4;

    [Header("Speed Upgrades")]
    [SerializeField] private List<GameObject[]> allSpeedVisuals;
    [SerializeField] private GameObject[] speedVisuals0;
    [SerializeField] private GameObject[] speedVisuals1;
    [SerializeField] private GameObject[] speedVisuals2;
    [SerializeField] private GameObject[] speedVisuals3;
    [SerializeField] private GameObject[] speedVisuals4;

    [Header("Magnet Upgrades")]
    [SerializeField] private List<GameObject[]> allMagnetVisuals;
    [SerializeField] private GameObject[] magnetVisuals1;
    [SerializeField] private GameObject[] magnetVisuals2;
    [SerializeField] private GameObject[] magnetVisuals3;
    [SerializeField] private GameObject[] magnetVisuals4;

    [Header("Collection Upgrades")]
    [SerializeField] private List<GameObject[]> allCollectionVisuals;
    [SerializeField] private GameObject[] collectionVisuals1;
    [SerializeField] private GameObject[] collectionVisuals2;
    [SerializeField] private GameObject[] collectionVisuals3;
    [SerializeField] private GameObject[] collectionVisuals4;

    [Header("Health Upgrades")]
    [SerializeField] private List<GameObject[]> allHealthVisuals;
    [SerializeField] private GameObject[] healthVisuals1;
    [SerializeField] private GameObject[] healthVisuals2;
    [SerializeField] private GameObject[] healthVisuals3;
    [SerializeField] private GameObject[] healthVisuals4;

    private UpgradeManager upgradeManager;

    // Start is called before the first frame update
    void Start()
    {
        //get upgrade manager from scene
        upgradeManager = FindObjectOfType<UpgradeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function that updates the player model to reflect upgrades
    public void UpdateModel()
    {
        //call each  visual update
    }

    // Function that updates the player model's dodge visuals
    public void UpdateDodgeVisuals()
    {
        //turn off laser visual
        foreach (GameObject visual in dodgeVisuals)
        {
            if (visual.activeSelf)
            {
                visual.SetActive(true);
            }
        }

        //activate if laser unlocked
        if (upgradeManager.GetDodge())
        {
            foreach (GameObject visual in dodgeVisuals)
            {
                visual.SetActive(true);
            }
        }
    }

    // Function that updates the player model's shield visuals
    public void UpdateShieldVisuals()
    {
        //turn off laser visual
        foreach (GameObject visual in shieldVisuals)
        {
            if (visual.activeSelf)
            {
                visual.SetActive(true);
            }
        }

        //activate if shield unlocked
        if (upgradeManager.GetShield())
        {
            foreach (GameObject visual in shieldVisuals)
            {
                visual.SetActive(true);
            }
        }
    }

    // Function that updates the player model's side shot visuals
    public void UpdateSideshotVisuals()
    {
        //turn off side shots visual
        foreach (GameObject visual in sideShotVisuals)
        {
            if (visual.activeSelf)
            {
                visual.SetActive(true);
            }
        }

        //activate if sides shots are unlocked
        if (upgradeManager.GetSideShots())
        {
            foreach (GameObject visual in sideShotVisuals)
            {
                visual.SetActive(true);
            }
        }
    }

    // Function that updates the player model's super laser visuals
    public void UpdateSuperLaserVisuals()
    {
        //turn off laser visual
        foreach (GameObject visual in superLaserVisuals)
        {
            if (visual.activeSelf)
            {
                visual.SetActive(true);
            }    
        }

        //activate if laser unlocked
        if (upgradeManager.GetSuperLaser())
        {
            foreach (GameObject visual in superLaserVisuals)
            {
                visual.SetActive(true);
            }
        }
    }

    // Function that updates the player model's spread shot visuals
    public void UpdateSpreadShotVisuals()
    {
        //Turn off visuals if visuals are on
        foreach (GameObject[] visuals in allSpreadShotVisuals)
        {
            foreach (GameObject visual in visuals)
            {
                if (visual.activeSelf)
                {
                    visual.SetActive(false);
                }
            }
        }

        //determine which version of the spread shot visual to use
        switch (upgradeManager.GetSpreadShotUpgradeLevel())
        {
            //level 1
            case 1:
                foreach (GameObject visual in spreadShotVisuals1)
                {
                    visual.SetActive(true);
                }
                break;

            //level 2
            case 2:
                foreach (GameObject visual in spreadShotVisuals2)
                {
                    visual.SetActive(true);
                }
                break;

            //level 3
            case 3:
                foreach (GameObject visual in spreadShotVisuals3)
                {
                    visual.SetActive(true);
                }
                break;

            //level 4
            case 4:
                foreach (GameObject visual in spreadShotVisuals4)
                {
                    visual.SetActive(true);
                }
                break;

            //level 0
            default:
                foreach (GameObject visual in spreadShotVisuals0)
                {
                    visual.SetActive(true);
                }
                break;
        }
    }

    // Function that updates the player model's fire rate visuals
    public void UpdateFireRateVisuals()
    {
        //Turn off visuals if visuals are on
        foreach (GameObject[] visuals in allFireRateVisuals)
        {
            foreach (GameObject visual in visuals)
            {
                if (visual.activeSelf)
                {
                    visual.SetActive(false);
                }
            }
        }

        //determine which version of the fire rate visual to use
        switch (upgradeManager.GetFireRateUpgradeLevel())
        {
            //level 1
            case 1:
                foreach (GameObject visual in fireRateVisuals1)
                {
                    visual.SetActive(true);
                }
                break;

            //level 2
            case 2:
                foreach (GameObject visual in fireRateVisuals2)
                {
                    visual.SetActive(true);
                }
                break;

            //level 3
            case 3:
                foreach (GameObject visual in fireRateVisuals3)
                {
                    visual.SetActive(true);
                }
                break;

            //level 4
            case 4:
                foreach (GameObject visual in fireRateVisuals4)
                {
                    visual.SetActive(true);
                }
                break;

            //level 0
            default:
                break;
        }
    }

    // Function that updates the player model's piercing visuals
    public void UpdatePiercingVisuals()
    {
        float barrelYPos; //barrel Y pos holder

        //determine legnth of barrel
        switch (upgradeManager.GetPiercingUpgradeLevel())
        {
            case 1:
                barrelYPos = yPosPiercingVisual1;
                break;

            case 2:
                barrelYPos = yPosPiercingVisual2;
                break;

            case 3:
                barrelYPos = yPosPiercingVisual3;
                break;

            case 4:
                barrelYPos = yPosPiercingVisual4;
                break;

            default:
                barrelYPos = yPosPiercingVisual0;
                break;
        }

        //set position of gun barrel
        gunBarrel.transform.position = new Vector3(gunBarrel.transform.position.x, yPosPiercingVisual1, gunBarrel.transform.position.z);
    }

    // Function that updates the player model's damage visuals
    public void UpdateDamageVisuals()
    {
        //Turn off visuals if visuals are on
        foreach (GameObject[] visuals in allDamageVisuals)
        {
            foreach (GameObject visual in visuals)
            {
                if (visual.activeSelf)
                {
                    visual.SetActive(false);
                }
            }
        }

        //determine which version of the damage visual to use
        switch (upgradeManager.GetDamageUpgradeLevel())
        {
            //level 1
            case 1:
                foreach (GameObject visual in damageVisuals1)
                {
                    visual.SetActive(true);
                }
                break;

            //level 2
            case 2:
                foreach (GameObject visual in damageVisuals2)
                {
                    visual.SetActive(true);
                }
                break;

            //level 3
            case 3:
                foreach (GameObject visual in damageVisuals3)
                {
                    visual.SetActive(true);
                }
                break;

            //level 4
            case 4:
                foreach (GameObject visual in damageVisuals4)
                {
                    visual.SetActive(true);
                }
                break;

            //level 0
            default:
                foreach (GameObject visual in damageVisuals0)
                {
                    visual.SetActive(true);
                }
                break;
        }
    }

    // Function that updates the player model's movement speed visuals
    public void UpdateMovementSpeedVisuals()
    {
        //Turn off visuals if visuals are on
        foreach (GameObject[] visuals in allSpeedVisuals)
        {
            foreach (GameObject visual in visuals)
            {
                if (visual.activeSelf)
                {
                    visual.SetActive(false);
                }
            }
        }

        //determine which version of the movement speed visual to use
        switch (upgradeManager.GetMovementSpeedUpgradeLevel())
        {
            //level 1
            case 1:
                foreach (GameObject visual in speedVisuals1)
                {
                    visual.SetActive(true);
                }
                break;

            //level 2
            case 2:
                foreach (GameObject visual in speedVisuals2)
                {
                    visual.SetActive(true);
                }
                break;

            //level 3
            case 3:
                foreach (GameObject visual in speedVisuals3)
                {
                    visual.SetActive(true);
                }
                break;

            //level 4
            case 4:
                foreach (GameObject visual in speedVisuals4)
                {
                    visual.SetActive(true);
                }
                break;

            //level 0
            default:
                foreach (GameObject visual in speedVisuals0)
                {
                    visual.SetActive(true);
                }
                break;
        }
    }

    // Function that updates the player model's magnet visuals
    public void UpdateMagnetVisuals()
    {
        //Turn off visuals if visuals are on
        foreach (GameObject[] visuals in allMagnetVisuals)
        {
            foreach (GameObject visual in visuals)
            {
                if (visual.activeSelf)
                {
                    visual.SetActive(false);
                }
            }
        }

        //determine which version of the magnet visual to use
        switch (upgradeManager.GetCollectionRangeUpgradeLevel())
        {
            //level 1
            case 1:
                foreach (GameObject visual in magnetVisuals1)
                {
                    visual.SetActive(true);
                }
                break;

            //level 2
            case 2:
                foreach (GameObject visual in magnetVisuals2)
                {
                    visual.SetActive(true);
                }
                break;

            //level 3
            case 3:
                foreach (GameObject visual in magnetVisuals3)
                {
                    visual.SetActive(true);
                }
                break;

            //level 4
            case 4:
                foreach (GameObject visual in magnetVisuals4)
                {
                    visual.SetActive(true);
                }
                break;

            //level 0
            default:
                break;
        }
    }

    // Function that updates the player model's collection visuals
    public void UpdateCollectionVisuals()
    {
        //Turn off visuals if visuals are on
        foreach (GameObject[] visuals in allCollectionVisuals)
        {
            foreach (GameObject visual in visuals)
            {
                if (visual.activeSelf)
                {
                    visual.SetActive(false);
                }
            }
        }

        //determine which version of the collection visual to use
        switch (upgradeManager.GetCollectionMultUpgradeLevel())
        {
            //level 1
            case 1:
                foreach (GameObject visual in collectionVisuals1)
                {
                    visual.SetActive(true);
                }
                break;

            //level 2
            case 2:
                foreach (GameObject visual in collectionVisuals2)
                {
                    visual.SetActive(true);
                }
                break;

            //level 3
            case 3:
                foreach (GameObject visual in collectionVisuals3)
                {
                    visual.SetActive(true);
                }
                break;

            //level 4
            case 4:
                foreach (GameObject visual in collectionVisuals4)
                {
                    visual.SetActive(true);
                }
                break;

            //level 0
            default:
                break;
        }
    }

    // Function that updates the player model's health visuals
    public void UpdateHealthVisuals()
    {
        //Turn off visuals if visuals are on
        foreach (GameObject[] visuals in allHealthVisuals)
        {
            foreach (GameObject visual in visuals)
            {
                if (visual.activeSelf)
                {
                    visual.SetActive(false);
                }
            }
        }

        //determine which version of the collection health to use
        switch (upgradeManager.GetMaxHealthUpgradeLevel())
        {
            //level 1
            case 1:
                foreach (GameObject visual in healthVisuals1)
                {
                    visual.SetActive(true);
                }
                break;

            //level 2
            case 2:
                foreach (GameObject visual in healthVisuals2)
                {
                    visual.SetActive(true);
                }
                break;

            //level 3
            case 3:
                foreach (GameObject visual in healthVisuals3)
                {
                    visual.SetActive(true);
                }
                break;

            //level 4
            case 4:
                foreach (GameObject visual in healthVisuals4)
                {
                    visual.SetActive(true);
                }
                break;

            //level 0
            default:
                break;
        }
    }
}
