using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject scrap;
    [SerializeField] private Weapon weapon;
    
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
        player = FindAnyObjectByType<Player>();
        collider = GetComponent<Collider>();
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
            Attack(player.transform);
        }
    }

    public void ToggleOnScreen()
    {
        //Debug.Log("ToggleScreenCalled");

        onScreen = !onScreen;
        if (onScreen && weapon != null)
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
                //Debug.Log("WeaponFired");
                Vector3 direction = target.position - weapon.GetMuzzlePos(0).position;
                direction.y = 0;
                weapon.ChangeProjectileDirection(0, direction);
                weapon.Fire();
            }
        }
    }
}
