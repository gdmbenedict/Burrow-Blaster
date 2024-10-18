using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private ScrapManager scrapManager;
    public LayerMask pickUpLayer;
    public float collectionRange;
    public float attractionSpeed;

    private float collectionRangeMult;
    private float collectionMult;

    // Start is called before the first frame update
    void Awake()
    {
        collectionRangeMult = 1;
        collectionMult = 1;
        scrapManager = FindObjectOfType<ScrapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        AttractPickups();
    }

    public void CollectScrap(int scrapAmount)
    {
        scrapManager.AddScrap((int)(scrapAmount * collectionMult));
    }

    public void SetCollectionRangeMult(float collectionRangeMult)
    {
        this.collectionRangeMult = collectionRangeMult;
    }

    public void SetCollectionMult(float collectionMult)
    {
        this.collectionMult = collectionMult;
    }

    public void AttractPickups()
    {
        Collider[] pickups;
        pickups = Physics.OverlapSphere(transform.position, collectionRange * collectionRangeMult, pickUpLayer,QueryTriggerInteraction.Collide);

        foreach (Collider collider in pickups)
        {
            Vector3 direction = transform.position - collider.transform.position;
            collider.transform.position += direction.normalized * attractionSpeed * Time.deltaTime;
        }
    }
}
