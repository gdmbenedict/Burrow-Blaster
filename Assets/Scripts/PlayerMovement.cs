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
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float detectionScale =2f;

    [Header("Player Movement Variables")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float screenTopBuffer = 1f;
    [SerializeField] private float screenBottomBuffer = 1f;

    [Header("Player Dodge Movement Variables")]
    [SerializeField] private bool dodgeUnlocked = false;
    [SerializeField] private float dodgeSpeed = 10f;
    [SerializeField] private float dodgeTime = 0.5f;

    [Header("Object references")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera cam;
    [SerializeField] private CameraController camController;
    private float cameraSpeed;

    private Vector3 movementDir;
    private float maxZ;
    private float minZ;
    [SerializeField] private float moveSpeedMult;
    private bool isDodging;

    private bool forcedMovement;
    private float forcedMovementTime = 0.3f;

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

        //Debug.Log("Top boundary = " + (maxZ - screenTopBuffer));
        //Debug.Log("Bottom boundary = " + (minZ + screenBottomBuffer));
        //Debug.Log("Player Pos Z = " + (gameObject.transform.position.z + (movementDir.z * moveSpeed * Time.deltaTime)));

        //checking top boundary
        if ((gameObject.transform.position.z + movementDir.z * moveSpeed * moveSpeedMult * Time.deltaTime * detectionScale) >= (maxZ - screenTopBuffer) && movementDir.z > 0)
        {
            if (rb.velocity.magnitude != 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
            }
            else
            {
                //Debug.Log("Freezing Z movement");
                movementDir.z = 0;
            }
        }
        //checking bottom boundary
        else if ((gameObject.transform.position.z + movementDir.z * moveSpeed * moveSpeedMult  * Time.deltaTime * detectionScale) <= (minZ + screenBottomBuffer) && movementDir.z < 0)
        {
            if (rb.velocity.magnitude != 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
            }
            else
            {
                //Debug.Log("Freezing Z movement");
                movementDir.z = 0;
            }
        }

        if (rb.velocity.magnitude != 0)
        {
            movementDir = rb.velocity.normalized;
        }

        //checking for collisions
        if (Physics.Raycast(rb.position, movementDir, out RaycastHit hit, moveSpeed * moveSpeedMult * Time.deltaTime * detectionScale, obstacleLayer))
        {
            //Debug.Log("Collision Detected");

            Vector3 pos = hit.collider.transform.position;
            float differenceX = Mathf.Abs(pos.x - transform.position.x);
            float differenceZ = Mathf.Abs(pos.z - transform.position.z);
            if (differenceX <= differenceZ)
            {
                if (rb.velocity.magnitude != 0)
                {
                    rb.velocity = new Vector3(0f, 0f, rb.velocity.z);
                }
                else
                {
                    //Debug.Log("Freezing X movement");
                    movementDir.x = 0;
                }
                
            }
            else
            {
                if (rb.velocity.magnitude != 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
                }
                else
                {
                    //Debug.Log("Freezing Z movement");
                    movementDir.z = 0;
                }
                
            }

            
        }

        //moving player
        if (!isDodging && !forcedMovement)
        {
            rb.MovePosition(rb.position + movementDir * moveSpeed * moveSpeedMult * Time.deltaTime);   
        }

        //move with camera if not arrived at boss
        if (!camController.HasArrived())
        {
            rb.MovePosition(rb.position + Vector3.forward * cameraSpeed * Time.deltaTime);
        }


        if (rb.velocity.magnitude != 0 && !isDodging && !forcedMovement)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void AddForce(Vector3 force)
    {
        forcedMovement = true;
        rb.AddForce(force, ForceMode.Impulse);
        StartCoroutine(ForcedMovement());
    }

    private IEnumerator ForcedMovement()
    {
        float forcedMovementTimer = 0;

        while (forcedMovementTimer < forcedMovementTime)
        {
            forcedMovementTimer += Time.deltaTime;
            yield return null;
        }

        forcedMovement = false;
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

    public void DodgeButton(InputAction.CallbackContext input)
    {
        if (input.action.WasPressedThisFrame() && dodgeUnlocked)
        {
            Dodge();
        }
    }

    public void Dodge()
    {
        rb.velocity = movementDir * dodgeSpeed;
        isDodging = true;
        StartCoroutine(dodgeCooldown());
    }

    private IEnumerator dodgeCooldown()
    {
        float timer = 0f;
        Vector3 originalVelocity = rb.velocity;

        while (timer < dodgeTime)
        {
            rb.velocity = Vector3.Lerp(originalVelocity, Vector3.zero, timer/dodgeTime);
            timer += Time.deltaTime;
            yield return null;
        }

        isDodging = false;
        rb.velocity = Vector3.zero;
    }
}
