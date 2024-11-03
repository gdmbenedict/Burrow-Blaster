using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject scrap;
    [SerializeField] private Weapon weapon;
    [SerializeField] private bool targetPlayer = true;
    
    //Checking if enemy is on screen
    private Camera cam;
    private Plane[] cameraFrustem;
    private bool onScreen;
    private Collider collider;

    //Player Info
    private Player player;

    // Start is called before the first frame update
    void Awake()
    {
        onScreen = false;
        cam = Camera.main;
        collider = GetComponent<Collider>();

        if (targetPlayer)
        {
            player = FindAnyObjectByType<Player>();
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        //getting camera field of vision
        cameraFrustem = GeometryUtility.CalculateFrustumPlanes(cam);
        bool detectedOnScreen = GeometryUtility.TestPlanesAABB(cameraFrustem, collider.bounds);
        if (detectedOnScreen && !onScreen || !detectedOnScreen && onScreen)
        {
            ToggleOnScreen();
        }

        if (onScreen)
        {
            if (targetPlayer)
            {
                Attack(player.transform);
            }
            else
            {
                FireWeapons();
            }
            
        }
    }

    public void ToggleOnScreen()
    {
        //Debug.Log("ToggleScreenCalled");

        onScreen = !onScreen;
        if (onScreen && weapon != null && !weapon.GetDisabled())
        {
            //added in to give player moment before the enemy starts shooting
            weapon.ActivateCooldown();
        }
    }

    public bool GetOnScreen()
    {
        return onScreen;
    }

    public void Die(GameObject explosion)
    {
        //instantiate scrap object & explosion
        Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(scrap, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    //Method that makes the enemy attack with its weapon
    public void Attack(Transform target)
    {
        if (weapon != null)
        {
            if (weapon.GetCanFire())
            {
                //getting muzzle positions
                List<Transform> muzzlePositions = weapon.GetMuzzles();

                //looping through muzzles to set directions
                for (int i=0; i<muzzlePositions.Count; i++)
                {
                    Vector3 direction = target.position - muzzlePositions[i].position;
                    direction.y = 0;
                    weapon.ChangeProjectileDirection(i, direction);
                }
                
                weapon.Fire();
            }
        }
    }

    //simple fire function
    public void FireWeapons()
    {
        weapon.Fire();
    }
}
