using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Object References")]
    public HealthSystem bossHealth;
    [SerializeField] private Weapon bossWeapon;
    [SerializeField] private GameObject model;

    [Header("Explosion Variables")]
    [SerializeField] private int numExplosions = 10;
    [SerializeField] private float explosionSpawnRadius = 2;
    [SerializeField] private float explosionInterval = 0.3f;
    [SerializeField] private float bigExplosionScale = 10;

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
        bossWeapon.Enable();
        bossWeapon.ActivateCooldown();
        StartCoroutine(bossHealth.BreakShield());
        bossBattleStarted = true;
    }

    public bool GetBossBattleStarted()
    {
        return bossBattleStarted;
    }

    public void Die(GameObject explosion)
    {
        bossWeapon.Disable();
        bossHealth.SetTakeDamage(false);
        StartCoroutine(BossExplode(explosion));
    }

    private IEnumerator BossExplode(GameObject explosion)
    {
        GameObject explosionInstance = null;

        for (int i=0; i<numExplosions; i++)
        {
            //generate explosion spawn position
            float exPosX = transform.position.x + Random.Range(-explosionSpawnRadius, explosionSpawnRadius);
            float exPosY = transform.position.y + Random.Range(-explosionSpawnRadius, explosionSpawnRadius);
            float exPosZ = transform.position.z + Random.Range(-explosionSpawnRadius, explosionSpawnRadius);
            Vector3 exPos = new Vector3(exPosX, exPosY, exPosZ);

            //spawn explosion and wait
            explosionInstance = Instantiate(explosion, exPos, Quaternion.identity);

            if (i<numExplosions-1)
            {
                yield return new WaitForSeconds(explosionInterval);
            }
        }

        //wait for final small explosion to stop
        while (explosionInstance != null)
        {
            yield return null;
        }

        explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
        explosionInstance.transform.localScale *= bigExplosionScale;
        model.SetActive(false);

        //wait for big explosion to stop
        while (explosionInstance != null)
        {
            yield return null;
        }

        gameManager.WinGame();
    }


}
