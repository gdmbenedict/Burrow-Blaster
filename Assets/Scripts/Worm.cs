using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    [SerializeField] private GameObject[] segments;
    private bool destroy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        destroy = true;

        for (int i=0; i<segments.Length; i++)
        {
            if (segments[i] != null)
            {
                destroy = false;
                break;
            }
        }

        if (destroy)
        {
            Destroy(gameObject);
        }
    }
}
