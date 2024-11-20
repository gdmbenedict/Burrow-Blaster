using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private int damage; //damage done by laser
    [SerializeField] private float lifetime; // lifetime of the laser
    [SerializeField] private Vector3 shrinkTargetScale = new Vector3(0, 0, 1); //scale laser shrinks down to

    // Start is called before the first frame update
    void Start()
    {
        //Starts the coroutine that controls laser lifetime
        StartCoroutine(Removal());
    }

    // Function that sets the stats of the laser
    public void SetStats(int damage, float scale, float lifetime)
    {
        this.damage = damage;
        transform.localScale = new Vector3(transform.localScale.x + scale, transform.localScale.y + scale, transform.localScale.z);
        this.lifetime = lifetime;
    }

    // Function that calls damage on entities in the laser
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Laser Collision Detected: " + other.gameObject.name);

        HealthSystem target = other.GetComponent<HealthSystem>();

        if (target != null)
        {
            //Debug.Log("Calling damage function");
            target.TakeChipDamage((float)(damage*Time.deltaTime));
        }
    }

    //Function that handles the removal of the super laser
    private IEnumerator Removal()
    {
        float timer = 0;
        Vector3 currentScale = transform.localScale;

        //loop through laser lifetime
        while (timer < lifetime)
        {
            //if timer is half done, start shrinking laser
            if (timer >= lifetime * 0.5f)
            {
                //lerp from original scale to target scale
                float shrinkTime = (timer - lifetime * 0.5f) / (lifetime * 0.5f);
                transform.localScale = Vector3.Lerp(currentScale, shrinkTargetScale, shrinkTime);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        //remove laser when done
        Destroy(gameObject);
    }
}
