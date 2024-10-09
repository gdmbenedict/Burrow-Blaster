using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Menu,
        Gameplay
    }

    [Header("Player References")]
    [SerializeField] private Player player;
    [SerializeField] private HealthSystem playerHealth;

    private int score;

    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
