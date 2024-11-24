using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int piercing;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float checkTime = 0.05f;
    [SerializeField] private Collider collider;

    private Camera cam;
    private Plane[] cameraFrustem;
    int collisions;

    // Start is called before the first frame update
    void Awake()
    {
        //set variables to 
        collisions = 0;
        cam = Camera.main;

        //Set direction of projectile (forward if direction is not set)
        if (direction == Vector3.zero)
        {
            SetDirection(Vector3.forward);
        }
        else
        {
            SetDirection(direction);
        }

        //start removal logic
        StartCoroutine(Removal());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    // Accessor method that sets the stats of the projectile
    public void SetStats(int damage, int piercing)
    {
        this.damage = damage;
        this.piercing = piercing;
    }

    // Go through hit logic
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision with " + other.gameObject.name);

        //detect if entity has health to target
        HealthSystem target = other.GetComponent<HealthSystem>();

        //damage health
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
        this.direction = direction.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

    }

    //Function that acts as a timer to remove the instantiated projectile
    private IEnumerator Removal()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkTime); // Check every x seconds to reduce computation

            //check if projectile is outside of camera space
            cameraFrustem = GeometryUtility.CalculateFrustumPlanes(cam);
            if (!GeometryUtility.TestPlanesAABB(cameraFrustem, collider.bounds))
            {
                Destroy(gameObject);
            }
        } 
    }

}
