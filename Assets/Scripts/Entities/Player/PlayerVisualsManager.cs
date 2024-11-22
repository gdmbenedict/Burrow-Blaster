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
    [SerializeField] private GameObject[] spreadShotVisuals0;
    [SerializeField] private GameObject[] spreadShotVisuals1;
    [SerializeField] private GameObject[] spreadShotVisuals2;
    [SerializeField] private GameObject[] spreadShotVisuals3;
    [SerializeField] private GameObject[] spreadShotVisuals4;
    private List<GameObject[]> allSpreadShotVisuals;

    [Header("FireRate Upgrades")]
    [SerializeField] private GameObject[] fireRateVisuals0;
    [SerializeField] private GameObject[] fireRateVisuals1;
    [SerializeField] private GameObject[] fireRateVisuals2;
    [SerializeField] private GameObject[] fireRateVisuals3;
    [SerializeField] private GameObject[] fireRateVisuals4;
    private List<GameObject[]> allFireRateVisuals;

    [Header("Piercing Upgrades")]
    [SerializeField] private GameObject gunBarrel;
    [SerializeField] private float yPosPiercingVisual0;
    [SerializeField] private float yPosPiercingVisual1;
    [SerializeField] private float yPosPiercingVisual2;
    [SerializeField] private float yPosPiercingVisual3;
    [SerializeField] private float yPosPiercingVisual4;

    [Header("Damage Upgrades")]
    [SerializeField] private GameObject[] damageVisuals0;
    [SerializeField] private GameObject[] damageVisuals1;
    [SerializeField] private GameObject[] damageVisuals2;
    [SerializeField] private GameObject[] damageVisuals3;
    [SerializeField] private GameObject[] damageVisuals4;
    private List<GameObject[]> allDamageVisuals;

    [Header("Speed Upgrades")]
    [SerializeField] private GameObject[] speedVisuals0;
    [SerializeField] private GameObject[] speedVisuals1;
    [SerializeField] private GameObject[] speedVisuals2;
    [SerializeField] private GameObject[] speedVisuals3;
    [SerializeField] private GameObject[] speedVisuals4;
    private List<GameObject[]> allSpeedVisuals;

    [Header("Magnet Upgrades")]
    [SerializeField] private GameObject[] magnetVisuals0;
    [SerializeField] private GameObject[] magnetVisuals1;
    [SerializeField] private GameObject[] magnetVisuals2;
    [SerializeField] private GameObject[] magnetVisuals3;
    [SerializeField] private GameObject[] magnetVisuals4;
    private List<GameObject[]> allMagnetVisuals;

    [Header("Collection Upgrades")]
    [SerializeField] private GameObject[] collectionVisuals0;
    [SerializeField] private GameObject[] collectionVisuals1;
    [SerializeField] private GameObject[] collectionVisuals2;
    [SerializeField] private GameObject[] collectionVisuals3;
    [SerializeField] private GameObject[] collectionVisuals4;
    private List<GameObject[]> allCollectionVisuals;

    [Header("Health Upgrades")]
    [SerializeField] private GameObject[] healthVisuals0;
    [SerializeField] private GameObject[] healthVisuals1;
    [SerializeField] private GameObject[] healthVisuals2;
    [SerializeField] private GameObject[] healthVisuals3;
    [SerializeField] private GameObject[] healthVisuals4;
    private List<GameObject[]> allHealthVisuals;

    private UpgradeManager upgradeManager;
    private ShopManager shopManager;

    // Start is called before the first frame update
    void Start()
    {
        //populate the all lists
        PopulateAllLists();

        //get upgrade manager from scene
        upgradeManager = FindObjectOfType<UpgradeManager>();
        UpdateModel();

        //bind to UI Manager
        shopManager = FindFirstObjectByType<ShopManager>();
        if (shopManager != null)
        {
            shopManager.AddPlayerVisualsManager(this);
        }
    }

    // Function that updates the player model to reflect upgrades (doesn't proc upgade tramsition)
    public void UpdateModel()
    {
        //updates all visuals
        UpdateDodgeVisuals(false);
        UpdateShieldVisuals(false);
        UpdateSideshotVisuals(false);
        UpdateSuperLaserVisuals(false);
        UpdateSpreadShotVisuals(false);
        UpdateFireRateVisuals(false);
        UpdateDamageVisuals(false);
        UpdatePiercingVisuals(false);
        UpdateCollectionVisuals(false);
        UpdateMagnetVisuals(false);
        UpdateMovementSpeedVisuals(false);
        UpdateHealthVisuals(false);
    }

    // Function that updates the player model's dodge visuals
    public void UpdateDodgeVisuals(bool transition)
    {
        UpdateSpecialVisuals(upgradeManager.GetDodge(), dodgeVisuals, transition);
    }

    // Function that updates the player model's shield visuals
    public void UpdateShieldVisuals(bool transition)
    {
        UpdateSpecialVisuals(upgradeManager.GetShield(), shieldVisuals, transition);
    }

    // Function that updates the player model's side shot visuals
    public void UpdateSideshotVisuals(bool transition)
    {
        UpdateSpecialVisuals(upgradeManager.GetSideShots(), sideShotVisuals, transition);
    }

    // Function that updates the player model's super laser visuals
    public void UpdateSuperLaserVisuals(bool transition)
    {
        UpdateSpecialVisuals(upgradeManager.GetSuperLaser(), superLaserVisuals, transition);
    }

    // Function that updates the player model's spread shot visuals
    public void UpdateSpreadShotVisuals(bool transition)
    {
        UpdateVisualsFromList(upgradeManager.GetSpreadShotUpgradeLevel(), false, allSpreadShotVisuals, transition);
    }

    // Function that updates the player model's fire rate visuals
    public void UpdateFireRateVisuals(bool transition)
    {
        UpdateVisualsFromList(upgradeManager.GetFireRateUpgradeLevel(), true, allFireRateVisuals, transition);
    }

    // Function that updates the player model's piercing visuals
    public void UpdatePiercingVisuals(bool transition)
    {
        float barrelYPos; //barrel Y pos holder

        Debug.Log(upgradeManager.GetPiercingUpgradeLevel());
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
        gunBarrel.transform.localPosition = new Vector3(gunBarrel.transform.localPosition.x, barrelYPos, gunBarrel.transform.localPosition.z);

        //play transition effects
        if (transition)
        {
            PlayTransitionEffects();
        }
    }

    // Function that updates the player model's damage visuals
    public void UpdateDamageVisuals(bool transition)
    {
        UpdateVisualsFromList(upgradeManager.GetDamageUpgradeLevel(), false, allDamageVisuals, transition);
    }

    // Function that updates the player model's movement speed visuals
    public void UpdateMovementSpeedVisuals(bool transition)
    {
        UpdateVisualsFromList(upgradeManager.GetMovementSpeedUpgradeLevel(), false, allSpeedVisuals, transition);
    }

    // Function that updates the player model's magnet visuals
    public void UpdateMagnetVisuals(bool transition)
    {
        UpdateVisualsFromList(upgradeManager.GetCollectionRangeUpgradeLevel(), false, allMagnetVisuals, transition);
    }

    // Function that updates the player model's collection visuals
    public void UpdateCollectionVisuals(bool transition)
    {
        UpdateVisualsFromList(upgradeManager.GetCollectionMultUpgradeLevel(), true, allCollectionVisuals, transition);
    }

    // Function that updates the player model's health visuals
    public void UpdateHealthVisuals(bool transition)
    {
        UpdateVisualsFromList(upgradeManager.GetMaxHealthUpgradeLevel(), true, allHealthVisuals, transition);
    }

    //Function that updates the visuals for a special upgrade
    private void UpdateSpecialVisuals(bool unlocked, GameObject[] visuals, bool transition)
    {
        //turn off visual
        foreach (GameObject visual in visuals)
        {
            if (visual.activeSelf)
            {
                visual.SetActive(false);
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

        //play transition effects
        if (transition)
        {
            PlayTransitionEffects();
        }
    }

    //Function that updates the visuals for a badger
    private void UpdateVisualsFromList(int upgradeLevel, bool addative, List<GameObject[]> visualsList, bool transition)
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

        //play transition effects
        if (transition)
        {
            PlayTransitionEffects();
        }
    }

    public void PlayTransitionEffects()
    {
        //TODO: implement transition effects
    }

    //function that populates the all lists
    private void PopulateAllLists()
    {
        allSpreadShotVisuals = new List<GameObject[]> {spreadShotVisuals0, spreadShotVisuals1, spreadShotVisuals1, spreadShotVisuals2, spreadShotVisuals3, spreadShotVisuals4 };
        allFireRateVisuals = new List<GameObject[]> { fireRateVisuals0, fireRateVisuals1, fireRateVisuals2, fireRateVisuals3, fireRateVisuals4 };
        allDamageVisuals = new List<GameObject[]> { damageVisuals0, damageVisuals1, damageVisuals2, damageVisuals3, damageVisuals4 };
        allMagnetVisuals = new List<GameObject[]> { magnetVisuals0, magnetVisuals1, magnetVisuals2, magnetVisuals3, magnetVisuals4 };
        allCollectionVisuals = new List<GameObject[]> { collectionVisuals0, collectionVisuals1, collectionVisuals2, collectionVisuals3, collectionVisuals4 };
        allSpeedVisuals = new List<GameObject[]> { speedVisuals0, speedVisuals1, speedVisuals2, speedVisuals3, speedVisuals4 };
        allHealthVisuals = new List<GameObject[]> { healthVisuals0, healthVisuals1, healthVisuals2, healthVisuals3, healthVisuals4 };
    }
}
