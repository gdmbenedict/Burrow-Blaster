using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
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

    //method that gets input for the blaster
    public void GetBlaster(InputAction.CallbackContext input)
    {
        if (input.action.IsPressed())
        {
            blaster.Fire();
        }
    }

    //method that gets input for the blaster
    public void GetSideShot(InputAction.CallbackContext input)
    {
        if (input.action.IsPressed())
        {
            sideShot.Fire();
        }
    }

    //method that gets input for the blaster
    public void GetLaser(InputAction.CallbackContext input)
    {
        if (input.action.IsPressed())
        {
            laser.Charge();
        }
        else if (input.canceled)
        {
            laser.Fire();
        }
    }


}
