using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool everythingFree = false;

    [Header("Object References")]
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private ScrapManager scrapManager;

    [Header("Variables")]
    [SerializeField] private int numOfCategories = 9;
    [SerializeField] private int[] specialPrices;
    [SerializeField] private int[] spreadShotPrices;
    [SerializeField] private int[] fireRatePrices;
    [SerializeField] private int[] piercingPrices;
    [SerializeField] private int[] damagePrices;
    [SerializeField] private int[] movementPrices;
    [SerializeField] private int[] collectionRangePrices;
    [SerializeField] private int[] collectionMultPrices;
    [SerializeField] private int[] healthPrices;
    private int[,] upgradePrices; //must align with upgrade manager order

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI[] priceLabels; //must align with upgrade manager order
    [SerializeField] private Button[] upgradeButtons; //must align with upgrade manager order
    [SerializeField] private Image[] upgradeBars; //must align with upgrade manager order

    [Header("Upgrade Bar Images")]
    [SerializeField] private Sprite[] upgradeBarLevels;


    // Start is called before the first frame update
    void Start()
    {
        //upgradePrices Declaration and Instantiation
        upgradePrices = new int[numOfCategories, upgradeManager.GetUpgradeMax()];

        //special
        for (int i=0; i<upgradeManager.GetUpgradeMax(); i++)
        {
            upgradePrices[0, i] = specialPrices[i];
        }

        //spread shot
        for (int i = 0; i < upgradeManager.GetUpgradeMax(); i++)
        {
            upgradePrices[1, i] = spreadShotPrices[i];
        }

        //fire rate
        for (int i = 0; i < upgradeManager.GetUpgradeMax(); i++)
        {
            upgradePrices[2, i] = fireRatePrices[i];
        }

        //piercing
        for (int i = 0; i < upgradeManager.GetUpgradeMax(); i++)
        {
            upgradePrices[3, i] = piercingPrices[i];
        }

        //damage
        for (int i = 0; i < upgradeManager.GetUpgradeMax(); i++)
        {
            upgradePrices[4, i] = damagePrices[i];
        }

        //movement speed
        for (int i = 0; i < upgradeManager.GetUpgradeMax(); i++)
        {
            upgradePrices[5, i] = movementPrices[i];
        }

        //collection range
        for (int i = 0; i < upgradeManager.GetUpgradeMax(); i++)
        {
            upgradePrices[6, i] = collectionRangePrices[i];
        }

        //collection mult
        for (int i = 0; i < upgradeManager.GetUpgradeMax(); i++)
        {
            upgradePrices[7, i] = collectionMultPrices[i];
        }

        //health
        for (int i = 0; i < upgradeManager.GetUpgradeMax(); i++)
        {
            upgradePrices[8, i] = healthPrices[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (everythingFree)
        {
            for (int i =0; i<upgradePrices.GetLength(0); i++)
            {
                for (int j =0; j < upgradePrices.GetLength(1); j++)
                {
                    upgradePrices[i, j] = 0;
                }
                
            }

            everythingFree = false;
        }
    }

    public void UpdatePrices()
    {
        //looping through updating costs
        for (int i =0; i < upgradeManager.GetAllUpgradeLevels().Length; i++)
        {
            string cost = "Cost: ";

            //Update costs of non-special upgrades
            if (i<upgradeManager.GetNumSpecialUpgrade())
            {
                if (upgradeManager.GetUpgradeLevel(i) < upgradeManager.GetSpecialUpgradeMax())
                {
                    cost += upgradePrices[0, upgradeManager.GetSpecialUppgradeLevel()];
                }
                else
                {
                    cost += "NA";
                }
            }
            //Updates the costss of special upgrades
            else
            {
                if (upgradeManager.GetUpgradeLevel(i) < upgradeManager.GetUpgradeMax())
                {
                    cost += upgradePrices[i - (upgradeManager.GetNumSpecialUpgrade() - 1), upgradeManager.GetUpgradeLevel(i)];
                }
                else
                {
                    cost += "NA";
                }
                
            }
            priceLabels[i].text = cost;
        }
    }

    public void UpdateButtons()
    {
        int[] upgradeLevels = upgradeManager.GetAllUpgradeLevels();

        //loop through all buttons to update them
        for (int i = 0; i < upgradeLevels.Length; i++)
        {
            if (i < upgradeManager.GetNumSpecialUpgrade())
            {
                //check if special button should be interactable
                if (upgradeLevels[i] < upgradeManager.GetSpecialUpgradeMax() && scrapManager.GetScrap() >= upgradePrices[0, upgradeManager.GetSpecialUppgradeLevel()])
                {
                    upgradeButtons[i].interactable = true;
                }
                else
                {
                    upgradeButtons[i].interactable = false;
                }
            }
            else
            {
                //update the upgrade bar sprites
                upgradeBars[i - (upgradeManager.GetNumSpecialUpgrade())].sprite = upgradeBarLevels[upgradeLevels[i]];

                //check if button should be interactable
                if (upgradeLevels[i] < upgradeManager.GetUpgradeMax() && scrapManager.GetScrap() >= upgradePrices[i - (upgradeManager.GetNumSpecialUpgrade() - 1), upgradeLevels[i]])
                {
                    upgradeButtons[i].interactable = true;
                }
                else
                {
                    upgradeButtons[i].interactable = false;
                }
            }
        }
    }

    public void Purchase(int targetUpgrade)
    {
        switch (targetUpgrade)
        {
            case 0:
                scrapManager.RemoveScrap(upgradePrices[0, upgradeManager.GetSpecialUppgradeLevel()]);
                upgradeManager.SetDodge(true);
                break;

            case 1:
                scrapManager.RemoveScrap(upgradePrices[0, upgradeManager.GetSpecialUppgradeLevel()]);
                upgradeManager.SetShield(true);
                break;

            case 2:
                scrapManager.RemoveScrap(upgradePrices[0, upgradeManager.GetSpecialUppgradeLevel()]);
                upgradeManager.SetSideShots(true);
                break;

            case 3:
                scrapManager.RemoveScrap(upgradePrices[0, upgradeManager.GetSpecialUppgradeLevel()]);
                upgradeManager.SetSuperLaser(true);
                break;

            case 4:
                scrapManager.RemoveScrap(upgradePrices[1, upgradeManager.GetSpreadShotUpgradeLevel()]);
                upgradeManager.UpgradeSpreadShot();
                break;

            case 5:
                scrapManager.RemoveScrap(upgradePrices[2, upgradeManager.GetFireRateUpgradeLevel()]);
                upgradeManager.UpgradeFireRate();
                break;

            case 6:
                scrapManager.RemoveScrap(upgradePrices[3, upgradeManager.GetPiercingUpgradeLevel()]);
                upgradeManager.UpgradePiercing();
                break;

            case 7:
                scrapManager.RemoveScrap(upgradePrices[4,upgradeManager.GetDamageUpgradeLevel()]);
                upgradeManager.UpgradeDamage();
                break;

            case 8:
                scrapManager.RemoveScrap(upgradePrices[5, upgradeManager.GetMovementSpeedUpgradeLevel()]);
                upgradeManager.UpgradeMovementSpeed();
                break;

            case 9:
                scrapManager.RemoveScrap(upgradePrices[6, upgradeManager.GetCollectionRangeUpgradeLevel()]);
                upgradeManager.UpgradeCollectionRange();
                break;

            case 10:
                scrapManager.RemoveScrap(upgradePrices[7, upgradeManager.GetCollectionMultUpgradeLevel()]);
                upgradeManager.UpgradeCollectionMult();
                break;

            case 11:
                scrapManager.RemoveScrap(upgradePrices[8, upgradeManager.GetMaxHealthUpgradeLevel()]);
                upgradeManager.UpgradeMaxHealth();
                break;
        }

        UpdatePrices();
        UpdateButtons();
    }
}
