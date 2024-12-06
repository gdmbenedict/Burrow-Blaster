using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum UIState
    {
        TitleScreen,
        OptionsScreen,
        UpgradeScreen,
        UpgradeMenuScreen,
        GameplayScreen,
        PauseScreen,
        InfoScreen,
        OptionInfoScreen,
        ResultScreen,
        CreditsScreen
    }

    [SerializeField] private UIState uiState;

    [Header("Object References")]
    [SerializeField] private GameManager gameManager;

    [Header("Gameplay UI")]
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI scrapTextGameplay;
    [SerializeField] private GameObject bossHealth;
    [SerializeField] private MiddleFillBar bossHealthbar;
    [SerializeField] private GameObject[] playerHealthStates;
    [SerializeField] private GameObject[] healthBlockers;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI scrapTextUpgrade;
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
    [SerializeField] private GameObject CreditsScreen;
    [SerializeField] private GameObject UpgradeMenuScreen;

    [Header("UI Elements")]
    [SerializeField] private Button ContinueButton;

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
    public CameraController camera;


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

            case "CreditsScreen":
                state = UIState.CreditsScreen;
                break;

            case "UpgradeMenuScreen":
                state = UIState.UpgradeMenuScreen;
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
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        switch (targetScreen)
        {
            case UIState.TitleScreen:
                uiState = UIState.TitleScreen;
                TitleScreen.SetActive(true);

                //check continue button
                ContinueButton.interactable = gameManager.ValidateSave();

                EventSystem.current.SetSelectedGameObject(null);
                break;

            case UIState.OptionsScreen:
                uiState = UIState.OptionsScreen;
                if (gameManager.GetGameState() == GameManager.GameState.TitleMenu)
                {
                    OptionsMenuScreen.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                }
                else
                {
                    OptionsPauseScreen.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                }
                break;

            case UIState.UpgradeScreen:
                uiState = UIState.UpgradeScreen;
                UpgradeScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                break;

            case UIState.GameplayScreen:
                uiState = UIState.GameplayScreen;
                GameplayUI.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;

            case UIState.PauseScreen:
                uiState = UIState.PauseScreen;
                PauseScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                break;

            case UIState.InfoScreen:
                uiState = UIState.InfoScreen;
                InfoScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                break;

            case UIState.OptionInfoScreen:
                uiState = UIState.OptionInfoScreen;
                OptionsInfoScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
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

                scrapResult.text = "Scrap Collected:\n" + player.collector.GetScrapCollected();
                distanceResult.text = "Distance Travelled: " + (int)player.transform.position.z;
                EventSystem.current.SetSelectedGameObject(null);
                break;

            case UIState.CreditsScreen:
                uiState = UIState.CreditsScreen;
                CreditsScreen.SetActive(true);
                break;

            case UIState.UpgradeMenuScreen:
                uiState = UIState.UpgradeMenuScreen;
                UpgradeMenuScreen.SetActive(true);
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
        CreditsScreen.SetActive(false);
        UpgradeMenuScreen.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void UpdateGameplayUI()
    {
        if (player != null)
        {
            scrapTextGameplay.text = ":" + player.collector.GetScrapCollected();
            UpdatePlayerHealthVisual();
        }

        if (camera != null)
        {
            progressSlider.value = camera.GetProgress();
        }

        if (boss != null && boss.GetBossBattleStarted())
        {
            bossHealthbar.SetFillAmount((float)boss.bossHealth.GetHealth()/boss.bossHealth.GetMaxHealth());
            if (!bossHealth.activeSelf)
            {
                bossHealth.SetActive(true);
            }
        }
        
    }

    //function that resets the gameplay UI
    public void ResetGameplayUI()
    {
        scrapTextGameplay.text = ":\n" + 0;
        bossHealth.SetActive(false);
        progressSlider.value = 0;
    }

    //Function that updates the UI for the upgrade section
    public void UpdateUpgradeUI()
    {
        scrapTextUpgrade.text = "Scrap: " + scrapManager.GetScrap();
    }

    //Function that sets up the initial state for the player health
    public void SetupPlayerHealthVisual()
    {
        //turn off any health states that are on
        for (int i=0; i<playerHealthStates.Length; i++)
        {
            playerHealthStates[i].SetActive(false);
        }

        //turn off any health blockers that are on
        for (int i = 0; i < healthBlockers.Length; i++)
        {
            healthBlockers[i].SetActive(false);
        }

        Debug.Log(player.playerHealth.GetMaxHealth());

        //get number of health blockers
        int numHealthBlockers = playerHealthStates.Length - 1 - player.playerHealth.GetMaxHealth();
        int missinghealthBlockers = healthBlockers.Length - numHealthBlockers;

        //place health blockers
        for (int i = healthBlockers.Length; i>missinghealthBlockers; i--)
        {
            //Debug.Log(i-1);
            healthBlockers[i-1].SetActive(true);
        }

        //set the player health visual
        playerHealthStates[player.playerHealth.GetHealth()].SetActive(true);
    }

    //Function that updates the visual for playerhealth
    private void UpdatePlayerHealthVisual()
    {
        //Debug.Log(player.playerHealth.GetHealth());

        //check if current player health matches health state visual and update if it does not
        if (!playerHealthStates[player.playerHealth.GetHealth()].activeSelf)
        {
            if (player.playerHealth.GetHealth()+1 < playerHealthStates.Length)
            {
                playerHealthStates[player.playerHealth.GetHealth() + 1].SetActive(false);
            }

            //Debug.Log(playerHealthStates[player.playerHealth.GetHealth()].activeSelf);
            playerHealthStates[player.playerHealth.GetHealth()].SetActive(true);
            //Debug.Log(playerHealthStates[player.playerHealth.GetHealth()].activeSelf);
        }
    }
}
