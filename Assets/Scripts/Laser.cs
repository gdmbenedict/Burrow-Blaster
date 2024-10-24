using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float scale;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float lifetime;

    // Start is called before the first frame update
    void Start()
    {
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

    public void SetStats(int damage, float scale)
    {
        this.damage = damage;
        this.scale = scale;
        transform.localScale *= (1+scale);
    }

    private void OnTriggerStay(Collider other)
    {
        HealthSystem target = other.GetComponent<HealthSystem>();

        if (target != null)
        {
            target.TakeDamage(damage);
        }
    }

    //Function that makes the projectile rotate towards movement direction.
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
        transform.LookAt(direction + transform.position);
        transform.Rotate(90f, 0f, 0f);

    }

    //Function that handles the removal of the super laser
    private IEnumerator Removal()
    {
        float timer = 0;
        Vector3 currentScale = transform.localScale;

        while (timer < lifetime)
        {
            if (timer >= lifetime/2)
            {
                float shrinkTime = (timer - lifetime / 2) / (lifetime/2);
                transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, shrinkTime);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
