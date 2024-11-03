using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public enum UIState
    {
        TitleScreen,
        OptionsScreen,
        UpgradeScreen,
        GameplayScreen,
        PauseScreen,
        InfoScreen,
        OptionInfoScreen,
        ResultScreen
    }

    [SerializeField] private UIState uiState;

    [Header("ObjectReferences")]
    [SerializeField] private GameManager gameManager;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI scrapTextGameplay;
    [SerializeField] private TextMeshProUGUI scrapTextUpgrade;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI bossHealthText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI scrapResult;
    [SerializeField] private TextMeshProUGUI distanceResult;
    [SerializeField] private GameObject gameWinText;
    [SerializeField] private GameObject gameLostText;

    [Header("UI Screens")]
    [SerializeField] private GameObject TitleScreen;
    [SerializeField] private GameObject UpgradeScreen;
    [SerializeField] private GameObject GameplayUI;
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject ResultScreen;
    [SerializeField] private GameObject OptionsMenuScreen;
    [SerializeField] private GameObject OptionsPauseScreen;
    [SerializeField] private GameObject InfoScreen;
    [SerializeField] private GameObject OptionsInfoScreen;

    [Header("First Selected Objects")]
    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject upgradeMenuFirst;
    [SerializeField] private GameObject pauseScreenFirst;
    [SerializeField] private GameObject resultScreenFirst;
    [SerializeField] private GameObject optionsMenuFirst;
    [SerializeField] private GameObject optionsPauseFirst;
    [SerializeField] private GameObject infoScreenFirst;
    [SerializeField] private GameObject optionsInfoFirst;

    [Header("Outisde Connections")]
    //Outside connections
    public ScrapManager scrapManager;
    public Player player;
    public Boss boss;


    // Start is called before the first frame update
    void Start()
    {
        ChangeUIScreen(UIState.TitleScreen);
    }

    // Update is called once per frame
    void Update()
    {
        switch (uiState)
        {
            case UIState.GameplayScreen:
                UpdateGameplayUI();
                break;
            case UIState.UpgradeScreen:
                UpdateUpgradeUI();
                break;
        }
    }

    public void ChangeUIButton(string targetScreen)
    {
        UIState state;
        switch (targetScreen)
        {
            case "TitleScreen":
                state = UIState.TitleScreen;
                break;

            case "UpgradeScreen":
                state = UIState.UpgradeScreen;
                break;

            case "GameplayUI":
                state = UIState.GameplayScreen;
                break;

            case "PauseScreen":
                state = UIState.PauseScreen;
                break;

            case "ResultScreen":
                state = UIState.ResultScreen;
                break;

            case "OptionsScreen":
                state = UIState.OptionsScreen;
                break;

            case "InfoScreen":
                state = UIState.OptionInfoScreen;
                break;

            default:
                state = UIState.TitleScreen;
                break;
        }

        ChangeUIScreen(state);
    }

    public void ChangeUIScreen(UIState targetScreen)
    {
        DisableAllScreens();

        switch (targetScreen)
        {
            case UIState.TitleScreen:
                uiState = UIState.TitleScreen;
                TitleScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(mainMenuFirst);
                break;

            case UIState.OptionsScreen:
                uiState = UIState.OptionsScreen;
                if (gameManager.GetGameState() == GameManager.GameState.TitleMenu)
                {
                    OptionsMenuScreen.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(optionsMenuFirst);
                }
                else
                {
                    OptionsPauseScreen.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(optionsPauseFirst);
                }
                break;

            case UIState.UpgradeScreen:
                uiState = UIState.UpgradeScreen;
                UpgradeScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(upgradeMenuFirst);
                break;

            case UIState.GameplayScreen:
                uiState = UIState.GameplayScreen;
                GameplayUI.SetActive(true);
                break;

            case UIState.PauseScreen:
                uiState = UIState.PauseScreen;
                PauseScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(pauseScreenFirst);
                break;

            case UIState.InfoScreen:
                uiState = UIState.InfoScreen;
                InfoScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(infoScreenFirst);
                break;

            case UIState.OptionInfoScreen:
                uiState = UIState.OptionInfoScreen;
                OptionsInfoScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(optionsInfoFirst);
                break;

            case UIState.ResultScreen:
                uiState = UIState.ResultScreen;
                ResultScreen.SetActive(true);

                //Get result text
                if (gameManager.GetWin())
                {
                    gameWinText.SetActive(true);
                    gameLostText.SetActive(false);
                }
                else
                {
                    gameWinText.SetActive(false);
                    gameLostText.SetActive(true);
                }

                scrapResult.text = "Scrap Collected: " + player.collector.GetScrapCollected();
                distanceResult.text = "Distance Travelled: " + (int)player.transform.position.z;
                EventSystem.current.SetSelectedGameObject(resultScreenFirst);
                break;
        }
    }

    private void DisableAllScreens()
    {
        TitleScreen.SetActive(false);
        OptionsMenuScreen.SetActive(false);
        OptionsPauseScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        PauseScreen.SetActive(false);
        GameplayUI.SetActive(false);
        InfoScreen.SetActive(false);
        OptionsInfoScreen.SetActive(false);
        ResultScreen.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void UpdateGameplayUI()
    {
        if (player != null)
        {
            scrapTextGameplay.text = "Scrap Collected: " + player.collector.GetScrapCollected();    
            healthText.text = "Health: " + player.playerHealth.GetHealth() + " / " + player.playerHealth.GetMaxHealth();
        }

        if (player != null && boss != null)
        {
            distanceText.text = "Travelled:\n" + (int)player.transform.position.z + "m / " + (int)boss.transform.position.z + "m";
        }

        if (boss != null && boss.GetBossBattleStarted())
        {
            bossHealthText.text = "Boss Health:\n" + boss.bossHealth.GetHealth() + " / " + boss.bossHealth.GetMaxHealth();
            if (!bossHealthText.enabled)
            {
                bossHealthText.enabled = true;
            }
        }
        
    }

    public void ResetGameplayUI()
    {
        scrapTextGameplay.text = "Scrap Collected: " + 0;
        healthText.text = "Health: " + 0 + " / " + 0;
        distanceText.text = "Travelled:\n" + 0 + "m / " + 0 + "m";
        bossHealthText.text = "Boss Health:\n" + 0 + " / " + 0;
        bossHealthText.enabled = false;
    }

    public void UpdateUpgradeUI()
    {
        scrapTextUpgrade.text = "Scrap: " + scrapManager.GetScrap();
    }
}
