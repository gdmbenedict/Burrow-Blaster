using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Detection Variables")]
    [SerializeField] private LayerMask collisionsLayers;
    [SerializeField] private Vector3 detectionBoxSize;
    [SerializeField] private Transform playerPos;

    [Header("Player Movement Variables")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float screenTopBuffer = 1f;
    [SerializeField] private float screenBottomBuffer = 1f;

    [Header("Player Dodge Movement Variables")]
    [SerializeField] private bool dodgeUnlocked = false;
    [SerializeField] private float dodgeSpeedMult = 3f;
    [SerializeField] private float dodgeTime = 0.5f;

    [Header("Forced Movement Variables")]
    private bool lockedMovement;
    [SerializeField] private float forcedMovementTime = 0.15f;

    [Header("Object references")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera cam;
    [SerializeField] private CameraController camController;
    private float cameraSpeed;

    private Vector3 inputDir;
    private Vector3 movementAllow = Vector3.zero;
    private Vector3 movementDir = Vector3.zero;
    private float maxZ;
    private float minZ;
    private float moveSpeedMult;
    private float lockedMovementFactor = 1;


    // Start is called before the first frame update
    void Start()
    {
        if (moveSpeedMult <= 0)
        {
            moveSpeedMult = 1;
        }
        camController = cam.GetComponent<CameraController>();
        cameraSpeed = camController.GetSpeed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //getting screen boundaries
        GetBoudaries();

        float projectedMovementLength;
        //checking for boundaries and collisions
        if (lockedMovement)
        {
            projectedMovementLength = 1 * moveSpeed * lockedMovementFactor * Time.fixedDeltaTime;
        }
        else
        {
            projectedMovementLength = 1 * moveSpeed * moveSpeedMult * Time.fixedDeltaTime;
        }
        
        //check for collisions and bounderies
        CheckCollisions(projectedMovementLength);

        //moving player
        rb.MovePosition(rb.position + movementDir.normalized * projectedMovementLength);   

        //move with camera if not arrived at boss
        if (!camController.HasArrived())
        {
            rb.MovePosition(rb.position + Vector3.forward * cameraSpeed * Time.fixedDeltaTime);
        }
        
        //set velocity to zero to stop issues with maintained velocity
        rb.velocity = Vector3.zero;
    }

    //function that forces the movement of the player in specified direction
    public void ForceMovement(Vector3 direction, float repulsionStrength)
    { 
        inputDir = direction;
        StopAllCoroutines();
        StartCoroutine(LockedMovement(repulsionStrength, forcedMovementTime));
    }

    public void DodgeButton(InputAction.CallbackContext input)
    {
        if (input.action.WasPressedThisFrame() && dodgeUnlocked && !lockedMovement)
        {
            Dodge();
        }
    }

    public void Dodge()
    {
        StartCoroutine(LockedMovement(dodgeSpeedMult, dodgeTime));
    }

    private IEnumerator LockedMovement(float movementMult, float timerLength)
    {
        //setting locked movement to be true
        lockedMovement = true;

        //setting loop values
        float timer = 0f;
        lockedMovementFactor = movementMult;

        //lerp loop
        while (timer < timerLength)
        {
            lockedMovementFactor = Mathf.Lerp(movementMult, 1f, timer / timerLength);
            timer += Time.deltaTime;
            yield return null;
        }

        //unlocking movement
        lockedMovement = false;
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

    //Function that checks if the player is 
    private void CheckCollisions(float projectedMovementLength)
    {
        movementAllow = Vector3.one;

        float projectedZPos = playerPos.position.z + (inputDir.normalized.z * projectedMovementLength);
        float topBorderPos = maxZ - screenTopBuffer;
        float bottomBorderPos = minZ + screenBottomBuffer;

        //check that the projected Z direction is within screen zone
        if (projectedZPos >= topBorderPos && inputDir.z > 0 || projectedZPos <= bottomBorderPos && inputDir.z < 0)
        {
            movementAllow.z = 0;
        }    

        //use Boxcast to detect collisions with obstacles
        if (Physics.BoxCast(playerPos.position, detectionBoxSize, inputDir, out RaycastHit hitInfo, Quaternion.identity, projectedMovementLength, collisionsLayers, QueryTriggerInteraction.Ignore))
        {
            
            Vector3 obstaclePos = hitInfo.collider.transform.position;

            float zDifference = obstaclePos.z - playerPos.position.z;
            float xDifference = obstaclePos.x - playerPos.position.x;

            if (hitInfo.collider.tag == "Obstacles")
            {
                if (zDifference > 0 && inputDir.z > 0)
                {
                    Debug.Log("Detecting top collision");
                    movementAllow.z = 0;
                }
                if (zDifference < 0 && inputDir.z < 0)
                {
                    Debug.Log("Detecting bottom collision");
                    movementAllow.z = 0;
                }
                if (xDifference > 0 && inputDir.x > 0)
                {
                    Debug.Log("Detecting right collision");
                    movementAllow.x = 0;
                }
                if (xDifference < 0 && inputDir.x < 0)
                {
                    Debug.Log("Detecting left collision");
                    movementAllow.x = 0;
                }
            }
            else if (hitInfo.collider.tag == "Walls")
            {
                if (xDifference > 0 && inputDir.x > 0)
                {
                    Debug.Log("Detecting right collision");
                    movementAllow.x = 0;
                }
                if (xDifference < 0 && inputDir.x < 0)
                {
                    Debug.Log("Detecting left collision");
                    movementAllow.x = 0;
                }
            }

        }

        movementDir = new Vector3(inputDir.x * movementAllow.x, inputDir.y * movementAllow.y, inputDir.z * movementAllow.z);
    }

    public void SetMoveSpeedMult(float moveSpeedMult)
    {
        this.moveSpeedMult = moveSpeedMult;
    }

    public void SetDodge(bool dodge)
    {
        dodgeUnlocked = dodge;
    }

    //Method that updates the movement direction of the player
    public void GetMovement(InputAction.CallbackContext input)
    {
        if (!lockedMovement)
        {
            //Getting input from controls and translating to a 3D vector
            Vector2 inputDir = input.ReadValue<Vector2>();
            //Debug.Log(inputDir);
            this.inputDir = new Vector3(inputDir.x, 0f, inputDir.y);
        }  
    }
}
