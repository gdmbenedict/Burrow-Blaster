using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private ScrapManager scrapManager;

    [Header("Variables")]
    [SerializeField] private int[,] upgradePrices; //must align with upgrade manager order

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI[] priceLabels; //must align with upgrade manager order
    [SerializeField] private Button[] upgradeButtons; //must align with upgrade manager order
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePrices()
    {
        //looping through updating costs
        for (int i =0; i < upgradeManager.GetAllUpgradeLevels().Length; i++)
        {
            string cost;

            if (i<upgradeManager.GetNumSpecialUpgrade())
            {
                cost = "Cost: " + upgradePrices[0, upgradeManager.GetSpecialUppgradeLevel()];
            }
            else
            {
                cost = "Cost: " + upgradePrices[i-upgradeManager.GetNumSpecialUpgrade()-1, upgradeManager.GetUpgradeLevel(i)];
                
            }
            priceLabels[i].text = cost;
        }
    }

    public void UpdateButtons()
    {
        int[] upgradeLevels = upgradeManager.GetAllUpgradeLevels();

        for (int i = 0; i < upgradeLevels.Length; i++)
        {
            if (i < upgradeManager.GetNumSpecialUpgrade())
            {
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
                if (upgradeLevels[i] < upgradeManager.GetUpgradeMax() && scrapManager.GetScrap() >= upgradePrices[i - upgradeManager.GetNumSpecialUpgrade() - 1, upgradeLevels[i]])
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
