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
    private float goalDistance;
    private float distanceTravelled;

    // Start is called before the first frame update
    void Start()
    {
        goalDistance = stopPos - transform.position.z;

        //bind to UI Manager
        FindObjectOfType<UIManager>().camera = this;
    }

    //Update loop called in a fixed time frame
    private void FixedUpdate()
    {
        //moves camera up along the level
        float moveDistance = scrollSpeed * Time.fixedDeltaTime;
        transform.position += Vector3.forward * moveDistance;
        distanceTravelled += moveDistance;

        if (transform.position.z >= stopPos && !arrived)
        {
            scrollSpeed = 0;
            arrived = true;
            FindObjectOfType<Boss>().StartBossBattle();
        }
    }

    //function that returns the speed of the camera
    public float GetSpeed()
    {
        return scrollSpeed;
    }

    //function that returns if the player has arrived at the boss
    public bool HasArrived()
    {
        return arrived;    
    }

    //funtion that returns the progress made in the run
    public float GetProgress()
    {
        float progress = distanceTravelled / goalDistance;

        if (progress < 0)
        {
            progress = 0;
        }
        else if (progress > 1)
        {
            progress = 1;
        }

        return progress;
    }
}
