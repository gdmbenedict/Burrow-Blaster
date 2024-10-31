using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        TitleMenu,
        Gameplay,
        UpgradeMenu
    }

    [Header("Player References")]
    //[SerializeField] private Player player;
    //[SerializeField] private HealthSystem playerHealth;

    [Header("Manager References")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ShopManager shopManager;

    [Header("Scenes")]
    [SerializeField] private string GameSceneName;
    [SerializeField] private string UpgradeSceneName;
    [SerializeField] private string TitleSceneName;

    private int score;
    private bool paused;
    private GameState gameState;
    private bool win;

    // Start is called before the first frame update
    void Start()
    {
        win = false;
        paused = false;
        gameState = GameState.TitleMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState newState)
    {

    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public bool GetWin()
    {
        return win;
    }

    public void PauseInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Pause Input Called");
        if (context.started)
        {
            TogglePause();
        }
    }

    //Function that toggles if the game is paused
    public void TogglePause()
    {
        //Debug.Log("Toggle Pause Called");

        //check if in the gameplay state so can't pause in menus
        if (gameState == GameState.Gameplay)
        {
            if (!paused)
            {
                Time.timeScale = 0;
                uiManager.ChangeUIScreen(UIManager.UIState.PauseScreen);        
            }
            else
            {
                Time.timeScale = 1;
                uiManager.ChangeUIScreen(UIManager.UIState.GameplayScreen);
            }

            paused = !paused;
        }
    }

    //function that loads into the gameplay scene
    public void GoToGame()
    {
        //return game to normal timescale if paused
        if (Time.timeScale <= 0)
        {
            Time.timeScale = 1; 
        }

        score = 0;
        levelManager.LoadScene(GameSceneName);
        gameState = GameState.Gameplay;
        uiManager.ChangeUIScreen(UIManager.UIState.GameplayScreen);

        if (win)
        {
            win = false;
        }
    }

    public void WinGame()
    {
        win = true;
        Time.timeScale = 0;
        uiManager.ChangeUIScreen(UIManager.UIState.ResultScreen);
    }

    public void LoseGame()
    {
        Time.timeScale = 0;
        uiManager.ChangeUIScreen(UIManager.UIState.ResultScreen);
    }

    public void GoToUpgrade()
    {
        //return game to normal timescale if paused
        if (Time.timeScale <= 0)
        {
            Time.timeScale = 1;
        }

        levelManager.LoadScene(UpgradeSceneName);
        gameState = GameState.UpgradeMenu;

        //Update UI
        shopManager.UpdatePrices();
        shopManager.UpdateButtons();
        uiManager.ChangeUIScreen(UIManager.UIState.UpgradeScreen);

        if (win)
        {
            win = false;
        }
    }

    public void GoToTitle()
    {
        //return game to normal timescale if paused
        if (Time.timeScale <= 0)
        {
            Time.timeScale = 1;
        }

        levelManager.LoadScene(TitleSceneName);
        gameState = GameState.TitleMenu;
        uiManager.ChangeUIScreen(UIManager.UIState.TitleScreen);

        if (win)
        {
            win = false;
        }
    }

    public void Quit()
    {
        Application.Quit();   
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
