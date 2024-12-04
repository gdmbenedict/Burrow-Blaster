using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private ScrapManager scrapManager;
    public LayerMask pickUpLayer;
    public float collectionRange;
    public float attractionSpeed;
    public Transform playerCenter;

    private float collectionRangeMult;
    private float collectionMult;
    private int scrapCollected;

    // Start is called before the first frame update
    void Awake()
    {
        scrapCollected = 0;
        collectionRangeMult = 1;
        collectionMult = 1;
        scrapManager = FindObjectOfType<ScrapManager>();
        FindObjectOfType<GameManager>().ConnectScrapCollector(this);
    }

    // Update is called once per frame
    void Update()
    {
        AttractPickups();
    }

    public void CollectScrap(int scrapAmount)
    {
        int scrapAdd = (int)(scrapAmount * collectionMult);
        scrapCollected += scrapAdd;
    }

    public int GetScrapCollected()
    {
        return scrapCollected;
    }

    public void DepositScrap()
    {
        scrapManager.AddScrap(scrapCollected);
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
        pickups = Physics.OverlapSphere(playerCenter.position, collectionRange * collectionRangeMult, pickUpLayer,QueryTriggerInteraction.Collide);

        foreach (Collider collider in pickups)
        {
            Vector3 direction = playerCenter.position - collider.transform.position;
            collider.transform.position += direction.normalized * attractionSpeed * Time.deltaTime;
        }
    }
}
