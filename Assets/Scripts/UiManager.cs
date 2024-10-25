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
        UpgradeScreen,
        GameplayScreen,
        PauseScreen,
        ResultScreen
    }

    [SerializeField] private UIState uiState;

    [Header("ObjectReferences")]
    [SerializeField] private GameManager gameManager;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI scrapTextGameplay;
    [SerializeField] private TextMeshProUGUI scrapTextUpgrade;
    [SerializeField] private TextMeshProUGUI healthText;
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

    [Header("First Selected Objects")]
    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject upgradeMenuFirst;
    [SerializeField] private GameObject pauseScreenFirst;
    [SerializeField] private GameObject resultScreenFirst;

    [Header("Outisde Connections")]
    //Outside connections
    public ScrapManager scrapManager;
    public Player player;
    public Transform goal;
    

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
        UpgradeScreen.SetActive(false);
        PauseScreen.SetActive(false);
        GameplayUI.SetActive(false);
        ResultScreen.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void UpdateGameplayUI()
    {
        if (player != null)
        {
            scrapTextGameplay.text = "Scrap Collected: " + player.collector.GetScrapCollected();
            distanceText.text = "Travelled " + (int)player.transform.position.z + "m / " + (int)goal.position.z + "m";
            healthText.text = "Health: " + player.playerHealth.GetHealth() + " / " + player.playerHealth.GetMaxHealth();
        }
        
    }

    public void UpdateUpgradeUI()
    {
        scrapTextUpgrade.text = "Scrap: " + scrapManager.GetScrap();
    }
}
