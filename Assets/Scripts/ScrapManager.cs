using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapManager : MonoBehaviour
{
    [SerializeField] private int totalScrap;
    [SerializeField] private UiManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UiManager>();
        totalScrap = 0;
    }

    public void AddScrap(int scrap)
    {
        totalScrap += scrap;
        uiManager.UpdateScrapUI(totalScrap);
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
