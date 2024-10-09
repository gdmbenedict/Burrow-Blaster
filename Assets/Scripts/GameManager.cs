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

    [Header("Scenes")]
    [SerializeField] private string GameSceneName;
    [SerializeField] private string UpgradeSceneName;
    [SerializeField] private string TitleSceneName;

    private int score;
    private bool paused;
    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
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

    public void PauseInput(InputAction.CallbackContext context)
    {
        Debug.Log("Pause Input Called");
        if (context.started)
        {
            TogglePause();
        }
    }

    //Function that toggles if the game is paused
    public void TogglePause()
    {
        Debug.Log("Toggle Pause Called");

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
    }

    public void WinGame()
    {
        Time.timeScale = 0;
        uiManager.ChangeUIScreen(UIManager.UIState.WinScreen);
    }

    public void LoseGame()
    {
        Time.timeScale = 0;
        uiManager.ChangeUIScreen(UIManager.UIState.LoseScreen);
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
        uiManager.ChangeUIScreen(UIManager.UIState.UpgradeScreen);
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
