using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Components")]
    public GameObject model;
    public Weapon blaster;
    public Weapon sideShot;
    public Weapon superLaser;
    public List<Collider> colliders;
    public HealthSystem playerHealth;
    public PlayerMovement playerMovement;
    public Collector collector;

    [Header("Blaster Muzzles")]
    public List<Transform> blasterMuzzles;
    public List<Vector3> blasterDirections;
    public Vector3 blasterProjectileOffset;

    [Header("Sideshot Muzzles")]
    public List<Transform> sideShotMuzzles;
    public List<Vector3> sideShotDirections;
    public Vector3 sideShotOffset;

    [Header("Super Laser Muzzle")]
    public Transform superLaserMuzzle;
    public Vector3 superLaserDirection;
    public Vector3 laserOffset;

    //external connections
    private GameManager gameManager;
    private UpgradeManager upgradeManager;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        GrabReferences();
        SetupPlayer();
        SetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GrabReferences()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
        upgradeManager = FindObjectOfType<UpgradeManager>();
    }

    private void SetReferences()
    {
        //set to UI manager and set up health visual
        uiManager.player = this;
        uiManager.SetupPlayerHealthVisual();
    }

    private void SetupPlayer()
    {
        //Debug.Log("Setting player values");

        //make player active
        model.SetActive(true);

        //activating colliders
        for (int i = 0; i < colliders.Count; i++)
        {
            colliders[i].enabled = true;
        }

        //setting up weapons
        if (blaster != null)
        {
            blaster.enabled = true;
            blaster.SetWeaponStats(upgradeManager.GetFireRate(), upgradeManager.GetDamage(), upgradeManager.GetPiercing());
            blaster.SetProjectileOffset(blasterProjectileOffset);

            //setting blaster muzzle positions
            switch (upgradeManager.GetSpreadShot())
            {
                case 1:
                    blaster.SetMuzzles(
                        new Transform[] { blasterMuzzles[2] },
                        new Vector3[] { blasterDirections[2] }
                        );
                    break;

                case 2:
                    blaster.SetMuzzles(
                        new Transform[] { blasterMuzzles[1], blasterMuzzles[3] },
                        new Vector3[] { blasterDirections[1], blasterDirections[3] }
                        );
                    break;

                case 3:
                    blaster.SetMuzzles(
                        new Transform[] { blasterMuzzles[1],blasterMuzzles[2], blasterMuzzles[3] },
                        new Vector3[] { blasterDirections[1],blasterDirections[2], blasterDirections[3] }
                        );
                    break;

                case 4:
                    blaster.SetMuzzles(
                        new Transform[] { blasterMuzzles[0], blasterMuzzles[1], blasterMuzzles[3], blasterMuzzles[4] },
                        new Vector3[] { blasterDirections[0], blasterDirections[1], blasterDirections[3], blasterDirections[4] }
                        );
                    break;

                case 5:
                    blaster.SetMuzzles(
                        new Transform[] { blasterMuzzles[0], blasterMuzzles[1], blasterMuzzles[2], blasterMuzzles[3], blasterMuzzles[4] },
                        new Vector3[] { blasterDirections[0], blasterDirections[1], blasterDirections[2], blasterDirections[3], blasterDirections[4] }
                        );
                    break;

                default:
                    blaster.SetMuzzles(
                        new Transform[] { blasterMuzzles[2] },
                        new Vector3[] { blasterDirections[2] }
                        );
                    break;
            }
        }

        //Debug.Log(upgradeManager.GetSideShots());

        //Setting up sideshots
        if (sideShot != null && upgradeManager.GetSideShots())
        {
            sideShot.enabled = true;
            sideShot.SetWeaponStats(upgradeManager.GetFireRate(), upgradeManager.GetDamage(), upgradeManager.GetPiercing());
            sideShot.SetProjectileOffset(sideShotOffset);

            //Debug.Log("Getting to switch statement");

            //setting blaster muzzle positions
            switch (upgradeManager.GetSpreadShot())
            {
                case 1:
                    sideShot.SetMuzzles(
                        new Transform[] { sideShotMuzzles[2], sideShotMuzzles[7] },
                        new Vector3[] { sideShotDirections[2], sideShotDirections[7] }
                        );
                    break;

                case 2:
                    sideShot.SetMuzzles(
                        new Transform[] { sideShotMuzzles[1], sideShotMuzzles[3], sideShotMuzzles[6], sideShotMuzzles[8] },
                        new Vector3[] { sideShotDirections[1], sideShotDirections[3], sideShotDirections[6], sideShotDirections[8] }
                        );
                    break;

                case 3:
                    sideShot.SetMuzzles(
                        new Transform[] { sideShotMuzzles[1], sideShotMuzzles[2], sideShotMuzzles[3], sideShotMuzzles[6], sideShotMuzzles[7], sideShotMuzzles[8] },
                        new Vector3[] { sideShotDirections[1], sideShotDirections[2], sideShotDirections[3], sideShotDirections[6], sideShotDirections[7], sideShotDirections[8] }
                        );
                    break;

                case 4:
                    sideShot.SetMuzzles(
                        new Transform[] { sideShotMuzzles[0], sideShotMuzzles[1], sideShotMuzzles[3], sideShotMuzzles[4], sideShotMuzzles[5], sideShotMuzzles[6], sideShotMuzzles[8], sideShotMuzzles[9] },
                        new Vector3[] { sideShotDirections[0], sideShotDirections[1], sideShotDirections[3], sideShotDirections[4], sideShotDirections[5], sideShotDirections[6], sideShotDirections[8], sideShotDirections[9] }
                        );
                    break;

                case 5:
                    sideShot.SetMuzzles(
                        new Transform[] { sideShotMuzzles[0], sideShotMuzzles[1], sideShotMuzzles[2], sideShotMuzzles[3], sideShotMuzzles[4], sideShotMuzzles[5], sideShotMuzzles[6], sideShotMuzzles[7], sideShotMuzzles[8], sideShotMuzzles[9], },
                        new Vector3[] { sideShotDirections[0], sideShotDirections[1], sideShotDirections[2], sideShotDirections[3], sideShotDirections[4], sideShotDirections[5], sideShotDirections[6], sideShotDirections[7], sideShotDirections[8], sideShotDirections[9]}
                        );
                    break;

                default:
                    sideShot.SetMuzzles(
                        new Transform[] { sideShotMuzzles[2], sideShotMuzzles[7] },
                        new Vector3[] { sideShotDirections[2], sideShotDirections[7] }
                        );
                    break;
            }

            //sDebug.Log(sideShot.GetMuzzles());
        }
        else if (sideShot != null)
        {
            sideShot.Disable();
        }

        //Debug.Log(upgradeManager.GetSuperLaser());
        if (superLaser != null && upgradeManager.GetSuperLaser())
        {
            superLaser.enabled = true;
            superLaser.SetWeaponStats(upgradeManager.GetFireRate(), upgradeManager.GetDamage(), upgradeManager.GetPiercing());
            superLaser.SetLaserStats(upgradeManager.GetSpreadShot(), upgradeManager.GetPiercing());
            superLaser.SetLaserOffset(laserOffset);
            superLaser.SetMuzzles(
                new Transform[] {superLaserMuzzle },
                new Vector3[] {superLaserDirection }
                );
        }
        else if (sideShot != null)
        {
            superLaser.Disable();
        }

        //Dodge
        playerMovement.SetDodge(upgradeManager.GetDodge());

        //Shield
        playerHealth.SetShield(upgradeManager.GetShield());

        //setting up player movement
        playerMovement.SetMoveSpeedMult(upgradeManager.GetMovementSpeed());

        //setting up collection
        collector.SetCollectionRangeMult(upgradeManager.GetCollectionRange());
        collector.SetCollectionMult(upgradeManager.GetCollectionMult());

        //setting up player health
        playerHealth.SetMaxHealth(upgradeManager.GetMaxHealth());

    }

    public void Die(GameObject explosion)
    {
        DisablePlayerVisual();

        //deactivating colliders
        for (int i=0; i < colliders.Count; i++)
        {
            colliders[i].enabled = false;
        }

        //disabling weapons
        blaster.Disable();

        if (sideShot != null)
        {
            sideShot.Disable();
        }

        if (superLaser != null)
        {
            superLaser.Disable();
        }

        StartCoroutine(WaitForPlayerDeath(explosion));
    }
    
    public void DisablePlayerVisual()
    {
        model.SetActive(false);
    }

    public void EnablePlayerVisual()
    {
        model.SetActive(true);
    }

    public IEnumerator WaitForPlayerDeath(GameObject explosion)
    {
        ParticleSystem particleExplosion;

        //getting particle system instance
        GameObject explosionInstance = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        particleExplosion = explosionInstance.GetComponent<ParticleSystem>();
        
        //wait for explosion to start
        yield return null;

        //wait till explosion is over
        while (particleExplosion != null)
        {
            yield return null;
        }

        gameManager.LoseGame();

    }
}
