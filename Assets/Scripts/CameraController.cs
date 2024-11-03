using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Varaibles")]
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private float stopPos;
    private bool arrived = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //moves camera up along the level
        transform.position += Vector3.forward * scrollSpeed * Time.fixedDeltaTime;

        if (transform.position.z >= stopPos && !arrived)
        {
            scrollSpeed = 0;
            arrived = true;
            FindObjectOfType<Boss>().StartBossBattle();
        }
    }

    public float GetSpeed()
    {
        return scrollSpeed;
    }

    public bool HasArrived()
    {
        return arrived;    
    }
}
