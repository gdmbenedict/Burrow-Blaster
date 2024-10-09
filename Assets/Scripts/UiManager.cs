using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private GameObject TitleScreen;
    [SerializeField] private GameObject UpgradeScreen;
    [SerializeField] private GameObject GameplayUI;
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LoseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
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
                TitleScreen.SetActive(true);
                break;

            case UIState.UpgradeScreen:
                UpgradeScreen.SetActive(true);
                break;

            case UIState.GameplayScreen:
                GameplayUI.SetActive(true);
                break;

            case UIState.PauseScreen:
                PauseScreen.SetActive(true);
                break;

            case UIState.WinScreen:
                WinScreen.SetActive(true);
                break;

            case UIState.LoseScreen:
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
}
