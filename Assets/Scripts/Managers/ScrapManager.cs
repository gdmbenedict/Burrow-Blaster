using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapManager : MonoBehaviour
{
    [SerializeField] private int totalScrap;
    [SerializeField] private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        totalScrap = 0;
    }

    public void SetScrap(int scrap)
    {
        totalScrap = scrap;
    }

    public void AddScrap(int scrap)
    {
        totalScrap += scrap;
    }

    public void RemoveScrap(int scrap)
    {
        totalScrap -= scrap;
    }

    public int GetScrap()
    {
        return totalScrap;
    }
}
