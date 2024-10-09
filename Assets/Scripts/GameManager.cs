using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        TitleMenu,
        Gameplay,
        UpgradeMenu
    }

    [Header("Player References")]
    [SerializeField] private Player player;
    [SerializeField] private HealthSystem playerHealth;

    [Header("Manager References")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UiManager uiManager;

    [Header("Scenes")]
    [SerializeField] private string GameSceneName;
    [SerializeField] private string UpgradeSceneName;

    private int score;

    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.TitleMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState newState)
    {

    }

    public void StartGame()
    {
        score = 0;
        gameState = GameState.Gameplay;
    }

    public void WinGame()
    {

    }

    public void LoseGame()
    {

    }

    public void RestartGame()
    {
        
    }

    public void ReturnToUpgrade()
    {

    }

    public void Quit()
    {
        
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
