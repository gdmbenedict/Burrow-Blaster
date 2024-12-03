using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private PlayerMovement playerMovement;

    public void SetPlayerCombat(PlayerCombat playerCombat)
    {
        this.playerCombat = playerCombat;
    }

    public void SetPlayerMovement(PlayerMovement playerMovement)
    {
        this.playerMovement = playerMovement;
    }

    public void PauseInput(InputAction.CallbackContext context)
    {
        gameManager.PauseInput(context);
    }

    public void MoveInput(InputAction.CallbackContext input)
    {
        playerMovement.GetMovement(input);
    }

    public void Fire1Input(InputAction.CallbackContext input)
    {
        playerCombat.GetBlaster(input);
    }

    public void Fire2Input(InputAction.CallbackContext input)
    {
        playerCombat.GetSideShot(input);
    }

    public void Fire3Input(InputAction.CallbackContext input)
    {
        playerCombat.GetLaser(input);
    }

    public void DodgeInput(InputAction.CallbackContext input)
    {
        playerMovement.DodgeButton(input);
    }
}