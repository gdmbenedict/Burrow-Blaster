using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Variables")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Object references")]
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private Camera cam;

    private Vector3 movementDir;
    private float maxZ;
    private float minZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //moving player
        playerRB.MovePosition(playerRB.position + movementDir * moveSpeed * Time.deltaTime);
        GetBoudaries();
    }

    //Method that updates the movement direction of the player
    public void GetMovement(InputAction.CallbackContext input)
    {
        //Getting input from controls and translating to a 3D vector
        Vector2 inputDir = input.ReadValue<Vector2>();
        movementDir = new Vector3(inputDir.x, 0f, inputDir.y);
    }

    private void GetBoudaries()
    {
        Ray ray;

        //Getting maxZ
        ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height, 0));
        if (Physics.Raycast(ray, out RaycastHit raycastHitmax))
        {
            maxZ = raycastHitmax.point.z;
            Debug.Log(maxZ);
        }

        //Getting minZ
        ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, 0, 0));
        if (Physics.Raycast(ray, out RaycastHit raycastHitmin))
        {
            minZ = raycastHitmin.point.z;
            Debug.Log(minZ);
        }
    }
}
