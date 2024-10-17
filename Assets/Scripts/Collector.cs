using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private ScrapManager scrapManager;
    public LayerMask pickUpLayer;

    // Start is called before the first frame update
    void Awake()
    {
        scrapManager = FindObjectOfType<ScrapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectScrap(int scrapAmount)
    {
        scrapManager.AddScrap(scrapAmount);
    }
}
