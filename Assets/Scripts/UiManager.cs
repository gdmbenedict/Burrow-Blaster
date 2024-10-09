using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public enum UIState
    {
        TitleScreen,
        UpgradeScreen,
        GameplayScreen,
        PauseScreen,
        WinScreen,
        LoseScreen,
    }

    [SerializeField] private UIState uiState;

    [SerializeField] private TextMeshProUGUI scrapText;

    [SerializeField] private GameObject TitleScreen;
    [SerializeField] private GameObject UpgradeScreen;
    [SerializeField] private GameObject GameplayUI;
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LoseScreen;

    // Start is called before the first frame update
    void Start()
    {
        ChangeUIScreen(UIState.TitleScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
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

            case UIState.WinScreen:
                uiState = UIState.WinScreen;
                WinScreen.SetActive(true);
                break;

            case UIState.LoseScreen:
                uiState = UIState.LoseScreen;
                LoseScreen.SetActive(true);
                break;
        }
    }

    private void DisableAllScreens()
    {
        TitleScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        PauseScreen.SetActive(false);
        GameplayUI.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }

    public void UpdateScrapUI(int scrap)
    {
        scrapText.text = "Scrap Collected:\n" + scrap;
    }
}
