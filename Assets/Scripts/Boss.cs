using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Object References")]
    public HealthSystem bossHealth;
    [SerializeField] private GameObject bossShield;
    [SerializeField] private Weapon bossWeapon;

    [Header("Explosion Variables")]
    [SerializeField] private int numExplosions = 10;
    [SerializeField] private float explosionSpawnRadius = 2;
    [SerializeField] private float explosionInterval = 0.3f; 
    private ParticleSystem particleExplosion;

    private GameManager gameManager;
    private bool bossBattleStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        FindObjectOfType<UIManager>().boss = this;
        bossWeapon.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBossBattle()
    {
        bossHealth.ToggleTakeDamage();
        bossWeapon.Enable();
        bossShield.SetActive(false);
        bossBattleStarted = true;
    }

    public bool GetBossBattleStarted()
    {
        return bossBattleStarted;
    }

    public void Die(GameObject explosion)
    {
        StartCoroutine(BossExplode(explosion));
    }

    private IEnumerator BossExplode(GameObject explosion)
    {
        for (int i=0; i<numExplosions; i++)
        {
            //generate explosion spawn position
            float exPosX = transform.position.x + Random.Range(-explosionSpawnRadius, explosionSpawnRadius);
            float exPosY = transform.position.y + Random.Range(-explosionSpawnRadius, explosionSpawnRadius);
            float exPosZ = transform.position.z + Random.Range(-explosionSpawnRadius, explosionSpawnRadius);
            Vector3 exPos = new Vector3(exPosX, exPosY, exPosZ);

            //spawn explosion and wait
            GameObject explosionInstance = Instantiate(explosion, exPos, Quaternion.identity);
            particleExplosion = explosionInstance.GetComponent<ParticleSystem>();

            if (i<numExplosions-1)
            {
                yield return new WaitForSeconds(explosionInterval);
            }
        }

        //wait for final explosion to stop
        while (particleExplosion.isPlaying)
        {
            yield return null;
        }

        gameManager.WinGame();
    }


}
