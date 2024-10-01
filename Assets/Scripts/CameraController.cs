using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Varaibles")]
    [SerializeField] private float scrollSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        //moves camera up along the level
        transform.position += Vector3.forward * scrollSpeed * Time.deltaTime;
    }

    public float GetSpeed()
    {
        return scrollSpeed;
    }
}
