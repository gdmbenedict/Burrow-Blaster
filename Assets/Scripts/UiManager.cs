using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
                break;
            case UIState.UpgradeScreen:
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
                break;

            case UIState.UpgradeScreen:
                uiState = UIState.UpgradeScreen;
                UpgradeScreen.SetActive(true);
                break;

            case UIState.GameplayScreen:
                uiState = UIState.GameplayScreen;
                GameplayUI.SetActive(true);
                break;

            case UIState.PauseScreen:
                uiState = UIState.PauseScreen;
                PauseScreen.SetActive(true);
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
                distanceResult.text = "Distance Travelled: " + player.transform.position.z;
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
    }

    public void UpdateGameplayUI()
    {
        scrapTextGameplay.text = "Scrap ";
    }
}
