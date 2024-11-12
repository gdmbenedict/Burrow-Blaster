using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Unlocks")]
    [SerializeField] private bool sideShotUnlocked;
    [SerializeField] private bool laserUnlocked;

    [Header("Weapons")]
    [SerializeField] private Weapon blaster;
    [SerializeField] private Weapon sideShot;
    [SerializeField] private Weapon laser;

    private bool firingBlaster;
    private bool firingSideShots;
    private bool chargingLaser;

    void Update()
    {
        if (firingBlaster)
        {
            blaster.Fire();
        }
        else if (firingSideShots && sideShot != null)
        {
            sideShot.Fire();
        }
        else if (chargingLaser && laser != null)
        {
            laser.Charge();
        }
    }

    //method that gets input for the blaster
    public void GetBlaster(InputAction.CallbackContext input)
    {
        if (input.action.IsPressed())
        {
            firingBlaster = true;
        }
        else if (input.canceled)
        {
            firingBlaster = false;
        }

    }

    //method that gets input for the blaster
    public void GetSideShot(InputAction.CallbackContext input)
    {
        if (input.action.IsPressed())
        {
            //Debug.Log("GotSideshot Input");
            firingSideShots = true;
        }
        else if (input.canceled)
        {
            firingSideShots = false;
        }
    }

    //method that gets input for the blaster
    public void GetLaser(InputAction.CallbackContext input)
    {
        if (input.action.IsPressed())
        {
            chargingLaser = true;
        }
        else if (input.canceled)
        {
            chargingLaser = false;
            laser.Fire();
        }
    }




}
