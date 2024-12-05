using System.Collections;
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
    [Header("Segment Management")]
    [SerializeField] private GameObject cam; //moving camera of the level
    [SerializeField] private float distance = 60;

    [Header("Segments")]
    [SerializeField] private Segment[] segments;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distancefromPlayer;

        //loop through tiles to load them in
        for (int i=0; i<segments.Length; i++)
        {
            //check distance from player camera
            distancefromPlayer = Mathf.Abs(segments[i].position.z - cam.transform.position.z);
            //Debug.Log(distancefromPlayer);

            //load if close enough and unloaded
            if (distancefromPlayer <= distance && !segments[i].loaded)
            {
                //Debug.Log("Calling load segment");
                LoadSegment(segments[i]);
                segments[i].loaded = true;
            }
            //unloadd scene if too far
            else if (distancefromPlayer > distance && segments[i].loaded)
            {
                UnloadSegment(segments[i]);
                segments[i].loaded = false;
            }
        }
    }

    //Function to load segments if player is close to segment position;
    private void LoadSegment(Segment segment)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(segment.scene, LoadSceneMode.Additive);
    }

    //Function to un-load segments if player is far from segment position;
    private void UnloadSegment(Segment segment)
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(segment.scene);
    }
}
