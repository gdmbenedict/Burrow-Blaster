using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }
}
