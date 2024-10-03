using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private bool isFiring;
    [SerializeField] private Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            weapon.Fire();
        }
    }

    public void GetFiring(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            isFiring = true;
        }
        else if (input.canceled)
        {
            isFiring = false;
        }
    }


}
