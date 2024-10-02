using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Variables")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float screenTopBuffer = 1f;
    [SerializeField] private float screenBottomBuffer = 1f;

    [Header("Object references")]
    [SerializeField] private CharacterController playerCharacter;
    [SerializeField] private Camera cam;
    private float cameraSpeed;

    private Vector3 movementDir;
    private float maxZ;
    private float minZ;

    // Start is called before the first frame update
    void Start()
    {
        cameraSpeed = cam.GetComponent<CameraController>().GetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        //getting screen boundaries
        GetBoudaries();

        //Debug.Log("Top boundary = " + (maxZ - screenTopBuffer));
        //Debug.Log("Bottom boundary = " + (minZ + screenBottomBuffer));
        //Debug.Log("Player Pos Z = " + (gameObject.transform.position.z + (movementDir.z * moveSpeed * Time.deltaTime)));

        //checking top boundary
        if ((gameObject.transform.position.z + movementDir.z * moveSpeed * Time.deltaTime) >= (maxZ - screenTopBuffer) && movementDir.z > 0)
        {
            //Debug.Log("Boundry Being Called");
            movementDir.z = 0;
        }
        //checking bottom boundary
        else if ((gameObject.transform.position.z + movementDir.z * moveSpeed * Time.deltaTime) <= (minZ + screenBottomBuffer) && movementDir.z < 0)
        {
            //Debug.Log("Boundry Being Called");
            movementDir.z = 0;
        }


        //moving player
        playerCharacter.Move(movementDir * moveSpeed * Time.deltaTime);
        playerCharacter.Move(Vector3.forward * cameraSpeed * Time.deltaTime);
    }

    //Method that updates the movement direction of the player
    public void GetMovement(InputAction.CallbackContext input)
    {
        //Getting input from controls and translating to a 3D vector
        Vector2 inputDir = input.ReadValue<Vector2>();
        //Debug.Log(inputDir);
        movementDir = new Vector3(inputDir.x, 0f, inputDir.y);
    }

    private void GetBoudaries()
    {
        Ray ray;

        //Getting maxZ
        ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height, 0));
        if (Physics.Raycast(ray, out RaycastHit raycastHitmax))
        {
            maxZ = raycastHitmax.point.z;
            //Debug.Log(maxZ);
        }

        //Getting minZ
        ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, 0, 0));
        if (Physics.Raycast(ray, out RaycastHit raycastHitmin))
        {
            minZ = raycastHitmin.point.z;
            //Debug.Log(minZ);
        }
    }
}
