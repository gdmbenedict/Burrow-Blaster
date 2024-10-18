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

    [Header("Blaster Muzzles")]
    public List<Transform> blasterMuzzles;
    public List<Vector3> blasterDirections;

    [Header("Sideshot Muzzles")]
    public List<Transform> sideShotMuzzles;
    public List<Vector3> sideShotDirections;

    [Header("Super Laser Muzzle")]
    public Transform superLaserMuzzle;
    public Vector3 suplerLasterDirection;

    //external connections
    private GameManager gameManager;
    private UpgradeManager upgradeManager;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        GrabReferences();
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
        uiManager.player = this;
    }

    private void SetupPlayer()
    {
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

        if (sideShot != null && upgradeManager.GetSideShots())
        {
            sideShot.enabled = true;
            sideShot.SetWeaponStats(upgradeManager.GetFireRate(), upgradeManager.GetDamage(), upgradeManager.GetPiercing());

            //setting blaster muzzle positions
            switch (upgradeManager.GetSpreadShot())
            {
                case 1:
                    blaster.SetMuzzles(
                        new Transform[] { sideShotMuzzles[2] },
                        new Vector3[] { sideShotDirections[2] }
                        );
                    break;

                case 2:
                    blaster.SetMuzzles(
                        new Transform[] { sideShotMuzzles[1], sideShotMuzzles[3] },
                        new Vector3[] { sideShotDirections[1], sideShotDirections[3] }
                        );
                    break;

                case 3:
                    blaster.SetMuzzles(
                        new Transform[] { sideShotMuzzles[1], sideShotMuzzles[2], sideShotMuzzles[3] },
                        new Vector3[] { sideShotDirections[1], sideShotDirections[2], sideShotDirections[3] }
                        );
                    break;

                case 4:
                    blaster.SetMuzzles(
                        new Transform[] { sideShotMuzzles[0], sideShotMuzzles[1], sideShotMuzzles[3], sideShotMuzzles[4] },
                        new Vector3[] { sideShotDirections[0], sideShotDirections[1], sideShotDirections[3], sideShotDirections[4] }
                        );
                    break;

                case 5:
                    blaster.SetMuzzles(
                        new Transform[] { sideShotMuzzles[0], sideShotMuzzles[1], sideShotMuzzles[2], sideShotMuzzles[3], sideShotMuzzles[4] },
                        new Vector3[] { sideShotDirections[0], sideShotDirections[1], sideShotDirections[2], sideShotDirections[3], sideShotDirections[4] }
                        );
                    break;

                default:
                    blaster.SetMuzzles(
                        new Transform[] { sideShotMuzzles[2] },
                        new Vector3[] { sideShotDirections[2] }
                        );
                    break;
            }
        }
        else
        {
            sideShot.Disable();
        }

        if (superLaser != null && upgradeManager.GetSuperLaser())
        {
            //TODO: implement super laser
        }
    }

    public void Die()
    {
        DisablePlayerVisual();

        //deactivating colliders
        for (int i=0; i < colliders.Count; i++)
        {
            colliders[i].enabled = false;
        }

        //disabling weapons
        blaster.Disable();
        sideShot.Disable();
        superLaser.Disable();

        gameManager.LoseGame();
    }
    
    public void DisablePlayerVisual()
    {
        model.SetActive(false);
    }

    public void EnablePlayerVisual()
    {
        model.SetActive(true);
    }
}
