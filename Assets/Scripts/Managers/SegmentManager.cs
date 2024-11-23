using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Struct to store variables on segments
[System.Serializable] //makes visible in inspector
public struct Segment
{
    public SceneField scene;
    public Vector3 position;
    public bool loaded;
}

public class SegmentManager : MonoBehaviour
{
    [Header("Segments")]
    [SerializeField] private List<Segment> segments;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function to load segments if player is close to segment position;
    private void LoadSegment(Segment segment)
    {

    }
}
