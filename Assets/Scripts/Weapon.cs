using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Characteristics")] 
    [SerializeField] private float firerate;
    [SerializeField] private float damage;
    [SerializeField] private int piercing;

    [Header("Projectiles")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private List<Transform> muzzlePositions;
    [SerializeField] private List<Vector3> projectileDirections;

    [Header("Laser")]
    [SerializeField] private bool isLaser = false;
    [SerializeField] private GameObject laser;
    [SerializeField] private Transform laserMuzzle;
    [SerializeField] private Vector3 laserDirection;
    [SerializeField] private float weaponCharge =0;
    [SerializeField] private float laserScaleFactor = 0.25f;
    [SerializeField] private float laserLifetime;

    private bool canFire = true;
    private float fireRateMult;
    private float damageMult;
    private int laserScaleIncrement;

    private void Start()
    {
        if (muzzlePositions == null)
        {
            muzzlePositions = new List<Transform>();
        }

        if (projectileDirections == null)
        {
            projectileDirections = new List<Vector3>();
        }
        
        fireRateMult = 1;
        damageMult = 1;
        piercing = 1;
        laserScaleIncrement = 1;
    }

    public void SetWeaponStats(float fireRateMult, float damageMult, int piercing)
    {
        this.fireRateMult = fireRateMult;
        this.damageMult = damageMult;
        this.piercing = piercing;
    }

    public void SetLaserStats(int laserScaleIncrement, float laserLifetime)
    {
        this.laserScaleIncrement = laserScaleIncrement;
        this.laserLifetime = laserLifetime;
    }

    //Function that fires a projectile
    public void Fire()
    {
        if (canFire)
        {
            if (!isLaser)
            {
                for (int i = 0; i < muzzlePositions.Count; i++)
                {
                    GameObject projectileInstance = Instantiate(projectile, muzzlePositions[i].position, Quaternion.identity);
                    Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
                    projectileScript.SetDirection(projectileDirections[i]);
                    projectileScript.SetStats((int)(damage * damageMult), piercing);
                }
            }
            else
            {
                if (weaponCharge >= 0.5f)
                {
                    //determine laser scale
                    float laserScale = laserScaleIncrement * laserScaleFactor;
                    int laserDamage = (int)(damage * damageMult * weaponCharge);

                    //Creating laser
                    GameObject laserInstance = Instantiate(laser, laserMuzzle.position, Quaternion.identity);
                    laserInstance.transform.parent = transform;

                    //setting laser properties
                    Laser laserScript = laserInstance.GetComponent<Laser>();
                    laserScript.SetDirection(laserDirection);
                    laserScript.SetStats(laserDamage, laserScale);

                    //play laser SFX
                    
                    weaponCharge = 0;
                }
                else
                {
                    //play laser misfire effect

                    weaponCharge = 0;
                    return;
                }
                
            }            

            canFire = false;
            StartCoroutine(Cooldown());
        }
    }

    public void Charge()
    {
        if (canFire)
        {
            if (weaponCharge < 1f)
            {
                weaponCharge += firerate * fireRateMult * Time.deltaTime;

                //play charging SFX
            }
            else
            {
                //play maxxed SFX
            }
        }
    }

    //Function that allows for changing of direction in which a projectile is fired
    public void ChangeProjectileDirection(int index, Vector3 direction)
    {
        projectileDirections[index] = direction;
    }

    public void SetMuzzles(Transform[] muzzlePositions, Vector3[] projectileDirections)
    {
        for (int i=0; i<muzzlePositions.Length; i++)
        {
            this.muzzlePositions.Add(muzzlePositions[i]);
        }

        for (int i=0; i<projectileDirections.Length; i++)
        {
            this.projectileDirections.Add(projectileDirections[i]);
        }
    }

    public Transform GetMuzzlePos(int index)
    {
        return muzzlePositions[index];
    }

    public bool GetCanFire()
    {
        return canFire;
    }

    public void ActivateCooldown()
    {
        canFire = false;
        StartCoroutine(Cooldown());
    }

    public void Disable()
    {
        StopAllCoroutines();
        canFire = false;
    }

    public void Enable()
    {
        canFire = true;
    }

    //Function that puts weapon operation on a timer
    private IEnumerator Cooldown()
    {
        float timer = 0f;
        float cooldown = 1f / (firerate * fireRateMult);

        while (timer < cooldown)
        {
            yield return null;

            timer += Time.deltaTime;
        }

        //allow player to fire again
        canFire = true;
    }
}
