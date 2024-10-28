using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Removal());
    }

    public void SetStats(int damage, float scale, float lifetime)
    {
        this.damage = damage;
        transform.localScale = new Vector3(transform.localScale.x + scale, transform.localScale.y + scale, transform.localScale.z);
        this.lifetime = lifetime;
    }

    private void OnTriggerStay(Collider other)
    {
        HealthSystem target = other.GetComponent<HealthSystem>();

        if (target != null)
        {
            target.TakeChipDamage((float)(damage*Time.deltaTime));
        }
    }

    //Function that handles the removal of the super laser
    private IEnumerator Removal()
    {
        float timer = 0;
        Vector3 currentScale = transform.localScale;
        Vector3 targetScale = new Vector3(0, 0, 1);

        while (timer < lifetime)
        {
            if (timer >= lifetime/2)
            {
                float shrinkTime = (timer - lifetime / 2) / (lifetime/2);
                transform.localScale = Vector3.Lerp(currentScale, targetScale, shrinkTime);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
