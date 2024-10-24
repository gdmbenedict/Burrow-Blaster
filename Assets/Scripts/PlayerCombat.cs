using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private bool blasterIsFiring;
    private bool sideShotIsFiring;
    private bool laserIsFiring;

    [Header("Unlocks")]
    [SerializeField] private bool sideShotUnlocked;
    [SerializeField] private bool laserUnlocked;

    [Header("Weapons")]
    [SerializeField] private Weapon blaster;
    [SerializeField] private Weapon sideShot;
    [SerializeField] private Weapon laser;

    // Start is called before the first frame update
    void Start()
    {
        blasterIsFiring = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (blasterIsFiring)
        {
            blaster.Fire();
        }

        if (sideShotIsFiring && sideShotUnlocked)
        {
            sideShot.Fire();
        }

        if (laserIsFiring && laserUnlocked)
        {
            //laser 
        }
    }

    //method that gets input for the blaster
    public void GetBlaster(InputAction.CallbackContext input)
    {
        blasterIsFiring = input.action.IsPressed();
    }

    //method that gets input for the blaster
    public void GetSideShot(InputAction.CallbackContext input)
    {
        sideShotIsFiring = input.action.IsPressed();
    }

    //method that gets input for the blaster
    public void GetLaser(InputAction.CallbackContext input)
    {
        laserIsFiring = input.action.IsPressed();
    }


}
