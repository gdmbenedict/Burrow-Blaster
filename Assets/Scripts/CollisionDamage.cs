using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private int contactDamage;
    [SerializeField] private float repulsionForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected: " + collision.gameObject.name);

        Collider other = collision.collider;

        if (other.gameObject.tag == "Player")
        {

            PlayerMovement playerMove = other.GetComponent<PlayerMovement>();
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();

            if (playerHealth.GetInvulnerable())
            {
                //dealing damage
                playerHealth.TakeDamage(contactDamage);

                //throwing player away
                Vector3 direction = other.transform.position - transform.position;
                Vector3 force = direction.normalized * repulsionForce;
                playerMove.AddForce(force);
            }
  
        }
    }
}
