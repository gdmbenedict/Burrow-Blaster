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
    [SerializeField] private GameObject[] fireRateVisuals0;
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
    [SerializeField] private GameObject[] magnetVisuals0;
    [SerializeField] private GameObject[] magnetVisuals1;
    [SerializeField] private GameObject[] magnetVisuals2;
    [SerializeField] private GameObject[] magnetVisuals3;
    [SerializeField] private GameObject[] magnetVisuals4;

    [Header("Collection Upgrades")]
    [SerializeField] private List<GameObject[]> allCollectionVisuals;
    [SerializeField] private GameObject[] collectionVisuals0;
    [SerializeField] private GameObject[] collectionVisuals1;
    [SerializeField] private GameObject[] collectionVisuals2;
    [SerializeField] private GameObject[] collectionVisuals3;
    [SerializeField] private GameObject[] collectionVisuals4;

    [Header("Health Upgrades")]
    [SerializeField] private List<GameObject[]> allHealthVisuals;
    [SerializeField] private GameObject[] healthVisuals0;
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
        UpdateSpecialVisuals(upgradeManager.GetShield(), shieldVisuals);
    }

    // Function that updates the player model's side shot visuals
    public void UpdateSideshotVisuals()
    {
        UpdateSpecialVisuals(upgradeManager.GetSideShots(), sideShotVisuals);
    }

    // Function that updates the player model's super laser visuals
    public void UpdateSuperLaserVisuals()
    {
        UpdateSpecialVisuals(upgradeManager.GetSuperLaser(), superLaserVisuals);
    }

    // Function that updates the player model's spread shot visuals
    public void UpdateSpreadShotVisuals()
    {
        UpdateVisualsFromList(upgradeManager.GetSpreadShotUpgradeLevel(), false, allSpreadShotVisuals);
    }

    // Function that updates the player model's fire rate visuals
    public void UpdateFireRateVisuals()
    {
        UpdateVisualsFromList(upgradeManager.GetFireRateUpgradeLevel(), true, allFireRateVisuals);
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
        UpdateVisualsFromList(upgradeManager.GetDamageUpgradeLevel(), false, allSpeedVisuals);
    }

    // Function that updates the player model's movement speed visuals
    public void UpdateMovementSpeedVisuals()
    {
        UpdateVisualsFromList(upgradeManager.GetMovementSpeedUpgradeLevel(), false, allSpeedVisuals);
    }

    // Function that updates the player model's magnet visuals
    public void UpdateMagnetVisuals()
    {
        UpdateVisualsFromList(upgradeManager.GetCollectionMultUpgradeLevel(), false, allMagnetVisuals);
    }

    // Function that updates the player model's collection visuals
    public void UpdateCollectionVisuals()
    {
        UpdateVisualsFromList(upgradeManager.GetCollectionMultUpgradeLevel(), true, allCollectionVisuals);
    }

    // Function that updates the player model's health visuals
    public void UpdateHealthVisuals()
    {
        UpdateVisualsFromList(upgradeManager.GetMaxHealthUpgradeLevel(), true, allHealthVisuals);
    }

    //Function that updates the visuals for a special upgrade
    private void UpdateSpecialVisuals(bool unlocked, GameObject[] visuals)
    {
        //turn off visual
        foreach (GameObject visual in visuals)
        {
            if (visual.activeSelf)
            {
                visual.SetActive(true);
            }
        }

        //activate if special upgrade unlocked
        if (unlocked)
        {
            foreach (GameObject visual in visuals)
            {
                visual.SetActive(true);
            }
        }
    }

    //Function that updates the visuals for a badger
    private void UpdateVisualsFromList(int upgradeLevel, bool addative, List<GameObject[]> visualsList)
    {
        //Turn off visuals if visuals are on
        foreach (GameObject[] visuals in visualsList)
        {
            foreach (GameObject visual in visuals)
            {
                if (visual.activeSelf)
                {
                    visual.SetActive(false);
                }
            }
        }

        //Determine which types of visual it is (addative or replacing)
        if (addative)
        {
            for (int i=0; i<upgradeLevel+1; i++)
            {
                foreach (GameObject visual in visualsList[i])
                {
                    visual.SetActive(true);
                }
            }
        }
        else
        {
            //determine which version of the collection health to use
            switch (upgradeLevel)
            {
                //level 1
                case 1:
                    foreach (GameObject visual in visualsList[1])
                    {
                        visual.SetActive(true);
                    }
                    break;

                //level 2
                case 2:
                    foreach (GameObject visual in visualsList[2])
                    {
                        visual.SetActive(true);
                    }
                    break;

                //level 3
                case 3:
                    foreach (GameObject visual in visualsList[3])
                    {
                        visual.SetActive(true);
                    }
                    break;

                //level 4
                case 4:
                    foreach (GameObject visual in visualsList[4])
                    {
                        visual.SetActive(true);
                    }
                    break;

                //level 0 or wrong level
                default:
                    foreach (GameObject visual in visualsList[0])
                    {
                        visual.SetActive(true);
                    }
                    break;
            }
        }
    }
}
