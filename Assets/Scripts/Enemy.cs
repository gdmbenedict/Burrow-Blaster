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
        Debug.Log("ToggleScreenCalled");

        onScreen = !onScreen;
        if (onScreen)
        {
            //added in to give player moment before the enemy starts shooting
            weapon.ActivateCooldown();
        }
    }

    public bool GetOnScreen()
    {
        return onScreen;
    }

    public void Die()
    {
        //instantiate scrap object
        Instantiate(scrap, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    //Method that makes the enemy attack with its weapon
    public void Attack(Transform target)
    {
        if (weapon.GetCanFire())
        {
            Debug.Log("WeaponFired");
            weapon.ChangeProjectileDirection(target.position - weapon.GetMuzzlePos().position);
            weapon.Fire();
        }
    }
}
