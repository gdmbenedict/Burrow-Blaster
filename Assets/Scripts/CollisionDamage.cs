using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
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
        Collider other = collision.collider;

        if (other.gameObject.layer == playerLayer)
        {
            Rigidbody playerRB = other.GetComponent<Rigidbody>();
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();

            //dealing damage
            playerHealth.TakeDamage(contactDamage);

            //throwing player away
            Vector3 direction = other.transform.position - transform.position;
            Vector3 force = direction.normalized * repulsionForce;
            playerRB.AddForce(force, ForceMode.Impulse);
        }
    }
}
