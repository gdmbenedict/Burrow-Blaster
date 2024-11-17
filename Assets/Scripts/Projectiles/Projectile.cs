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

    private Camera cam;
    private Plane[] cameraFrustem;
    private Collider collider;
    int collisions;

    // Start is called before the first frame update
    void Awake()
    {
        collisions = 0;

        cam = Camera.main;
        collider = GetComponent<Collider>();

        if (direction == null)
        {
            SetDirection(Vector3.forward);
        }
        else
        {
            SetDirection(direction);
        }

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
        this.direction = direction;
        transform.LookAt(direction + transform.position);
        transform.Rotate(0f, 0f, 0f);

    }

    //Function that acts as a timer to remove the instantiated projectile
    private IEnumerator Removal()
    {
        while (true)
        {
            cameraFrustem = GeometryUtility.CalculateFrustumPlanes(cam);
            bool detectedOnScreen = GeometryUtility.TestPlanesAABB(cameraFrustem, collider.bounds);

            if (!detectedOnScreen)
            {
                //Debug.Log("Destroyed projectile cause off screen");
                Destroy(gameObject);
            }

            yield return null;
        }
        
    }


}
