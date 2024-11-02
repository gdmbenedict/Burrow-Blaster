using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        FindObjectOfType<UIManager>().goal = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        gameManager.WinGame();
    }
}
