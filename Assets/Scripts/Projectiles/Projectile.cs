using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int piercing;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float checkTime = 0.05f;

    private Camera cam;
    private Plane[] cameraFrustem;
    private Collider collider;
    int collisions;

    // Start is called before the first frame update
    void Awake()
    {
        //set variables to their starting values
        collisions = 0;
        cam = Camera.main;
        collider = GetComponent<Collider>();

        //if direction is not set, change direction to forward
        if (direction == Vector3.zero)
        {
            SetDirection(Vector3.forward);
        }
        else
        {
            SetDirection(direction);
        }

        //starting removal coroutine
        StartCoroutine(Removal());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    public void SetStats(int damage, int piercing)
    {
        this.damage = damage;
        this.piercing = piercing;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision with " + other.gameObject.name);

        HealthSystem target = other.GetComponent<HealthSystem>();

        if (target != null)
        {
            target.TakeDamage(damage);
            collisions++;
        }
        else
        {
            //Debug.Log("Collision Destroy: id - " + other.gameObject.name);
            Destroy(gameObject);
        }

        if (collisions >= piercing)
        {
            Destroy(gameObject);
        }
        
    }

    //Function that makes the projectile rotate towards movement direction.
    public void SetDirection(Vector3 direction)
    {
        //update direction & align to direction
        this.direction = direction.normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    //Function that acts as a timer to remove the instantiated projectile
    private IEnumerator Removal()
    {
        while (true)
        {
            //get camera view
            cameraFrustem = GeometryUtility.CalculateFrustumPlanes(cam);

            //check if bullet collider is in camera view
            if (!GeometryUtility.TestPlanesAABB(cameraFrustem, collider.bounds))
            {
                //Debug.Log("Destroyed projectile cause off screen");
                Destroy(gameObject);
            }

            //delay next check (because check is computationaly expensive)
            yield return new WaitForSeconds(checkTime);
        }
        
    }


}
