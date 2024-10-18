using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameWin : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            gameManager.WinGame();
        }
    }
}
