using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{
    [SerializeField] private int value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ScrapManager scrapManager = other.GetComponent<ScrapManager>();

        if (scrapManager != null)
        {
            scrapManager.AddScrap(value);
            Destroy(gameObject);
        }   
    }
}
