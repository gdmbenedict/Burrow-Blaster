using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Player Movement Variables")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float screenTopBuffer = 1f;
    [SerializeField] private float screenBottomBuffer = 1f;

    [Header("Player Dodge Movement Variables")]
    [SerializeField] private float dodgeSpeed = 10f;
    [SerializeField] private float dodgeTime = 0.5f;

    [Header("Object references")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera cam;
    private float cameraSpeed;

    private Vector3 movementDir;
    private float maxZ;
    private float minZ;
    [SerializeField] private float moveSpeedMult;
    private bool canDodge;
    private bool isDodging;

    // Start is called before the first frame update
    void Start()
    {
        canDodge = false;
        if (moveSpeedMult <= 0)
        {
            moveSpeedMult = 1;
        }
        cameraSpeed = cam.GetComponent<CameraController>().GetSpeed();
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
        if ((gameObject.transform.position.z + movementDir.z * moveSpeed * moveSpeedMult * Time.deltaTime) >= (maxZ - screenTopBuffer) && movementDir.z > 0)
        {
            //Debug.Log("Boundry Being Called");
            movementDir.z = 0;
        }
        //checking bottom boundary
        else if ((gameObject.transform.position.z + movementDir.z * moveSpeed * moveSpeedMult  * Time.deltaTime) <= (minZ + screenBottomBuffer) && movementDir.z < 0)
        {
            //Debug.Log("Boundry Being Called");
            movementDir.z = 0;
        }

        //checking for collisions
        if (Physics.Raycast(rb.position, movementDir, out RaycastHit hit, moveSpeed * moveSpeedMult * Time.deltaTime, obstacleLayer))
        {
            //Debug.Log("Collision Detected");

            Vector3 pos = hit.collider.transform.position;
            float differenceX = Mathf.Abs(pos.x - transform.position.x);
            float differenceZ = Mathf.Abs(pos.z - transform.position.z);
            if (differenceX <= differenceZ)
            {
                //Debug.Log("Freezing X movement");
                movementDir.x = 0;
            }
            else
            {
                //Debug.Log("Freezing Z movement");
                movementDir.z = 0;
            }
        }

        //moving player
        rb.MovePosition(rb.position + movementDir * moveSpeed * moveSpeedMult * Time.deltaTime);
        rb.MovePosition(rb.position + Vector3.forward * cameraSpeed * Time.deltaTime);

        if (rb.velocity.magnitude != 0 && !isDodging)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void SetMoveSpeedMult(float moveSpeedMult)
    {
        this.moveSpeedMult = moveSpeedMult;
    }

    public void SetDodge(bool dodge)
    {
        canDodge = dodge;
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
