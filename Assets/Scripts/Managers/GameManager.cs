using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private ScrapManager scrapManager;

    [Header("Scenes")]
    [SerializeField] private string GameSceneName;
    [SerializeField] private string UpgradeSceneName;
    [SerializeField] private string TitleSceneName;

    private int score;
    private bool paused;
    private GameState gameState;
    private bool win;
    private Collector scrapCollector;

    // Start is called before the first frame update
    void Start()
    {
        win = false;
        paused = false;
        gameState = GameState.TitleMenu;

        musicManager.PlayMusic(Song.SongType.MenuMusic);
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
        else if (gameState == GameState.UpgradeMenu)
        {
            if (!paused)
            {
                Time.timeScale = 0;
                uiManager.ChangeUIScreen(UIManager.UIState.UpgradeMenuScreen);
            }
            else
            {
                Time.timeScale = 1;
                uiManager.ChangeUIScreen(UIManager.UIState.UpgradeScreen);
            }

            paused = !paused;
        }
    }

    //function that loads into the gameplay scene
    public void GoToGame(bool firstPlay)
    {
        musicManager.PlayMusic(Song.SongType.GameplayMusic);

        if (firstPlay)
        {
            Time.timeScale = 0;
            paused = true;

            score = 0;
            levelManager.LoadScene(GameSceneName);
            gameState = GameState.Gameplay;
            uiManager.ChangeUIScreen(UIManager.UIState.InfoScreen);
            uiManager.ResetGameplayUI();
        }
        else
        {
            Save();
            //return game to normal timescale if paused
            if (Time.timeScale <= 0)
            {
                Time.timeScale = 1;
            }

            score = 0;
            levelManager.LoadScene(GameSceneName);
            gameState = GameState.Gameplay;
            uiManager.ChangeUIScreen(UIManager.UIState.GameplayScreen);
            uiManager.ResetGameplayUI();

            if (win)
            {
                win = false;
            }
        }     
    }

    public void WinGame()
    {
        win = true;
        Time.timeScale = 0;
        scrapCollector.DepositScrap();
        uiManager.ChangeUIScreen(UIManager.UIState.ResultScreen);
        musicManager.PlayMusic(Song.SongType.MenuMusic);
    }

    public void LoseGame()
    {
        Time.timeScale = 0;
        scrapCollector.DepositScrap();
        uiManager.ChangeUIScreen(UIManager.UIState.ResultScreen);
        musicManager.PlayMusic(Song.SongType.MenuMusic);
    }

    public void GoToUpgrade(bool loadingGame)
    {
        if (loadingGame)
        {
            LoadSave();
        }
        else
        {
            Save();
        }

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

    public void ConnectScrapCollector(Collector scrapCollector)
    {
        this.scrapCollector = scrapCollector;
    }

    //Function that saves game data to a file
    private void Save()
    {
        //creating save tools and file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        //writting save data with serializable class
        SaveData data = new SaveData();
        data.scrap = scrapManager.GetScrap();
        data.upgradeLevels = upgradeManager.GetAllUpgradeLevels();

        //Serializing Data and saving to file
        bf.Serialize(file, data);
        file.Close();
    }

    //function that loads saved data
    private void LoadSave()
    {
        //check for file existance
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            //opening save file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            //loading data
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            //rading data
            scrapManager.SetScrap(data.scrap);
            upgradeManager.SetAllUpgradeLevels(data.upgradeLevels);
        }
    }

    //function that checks if there is save data on system
    public bool ValidateSave()
    {
        //check for file existance
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

[Serializable]
public class SaveData
{
    public int scrap;
    public int[] upgradeLevels;
}
