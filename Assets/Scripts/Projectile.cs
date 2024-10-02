using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;

    // Start is called before the first frame update
    void Awake()
    {
        if (direction == null)
        {
            SetDirection(Vector3.forward);
        }
        else
        {
            SetDirection(direction);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthSystem target = other.GetComponent<HealthSystem>();

        if (target != null)
        {
            target.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    //Function that makes the projectile rotate towards movement direction.
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
        transform.LookAt(direction + transform.position);
        transform.Rotate(90f, 0f, 0f);

    }


}
