using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    static Singleton instance;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Item " + gameObject.GetInstanceID() + " spawned");

        if (instance != null)
        {
            //Debug.Log("Destroying item " + gameObject.GetInstanceID());
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //Debug.Log("Adding item " + gameObject.GetInstanceID() + " to don't destroy");
        }
    }
}