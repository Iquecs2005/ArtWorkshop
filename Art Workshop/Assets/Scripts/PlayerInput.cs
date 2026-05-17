using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector2> OnMoveInput;
    [SerializeField] private UnityEvent<Vector2> OnShootInput;
    [SerializeField] private UnityEvent OnShootCanceledInput;

    private bool shootState = false;

    public void OnMove(InputAction.CallbackContext moveInput)
    {
        //Pega o input do player e passa pro script principal
        Vector2 moveVector = moveInput.ReadValue<Vector2>();
        OnMoveInput.Invoke(moveVector);
    }

    public void OnShoot(InputAction.CallbackContext shootInput)
    {
        Vector2 shootVector = shootInput.ReadValue<Vector2>();
        float xComponent = shootVector.x;
        float yComponent = shootVector.y;

        if (Mathf.Abs(xComponent) > 0.5f || Mathf.Abs(yComponent) > 0.5f)
        {
            shootState = true;
            OnShootInput.Invoke(shootVector);
        }
        else if (shootState)
        {
            shootState = false;
            OnShootCanceledInput.Invoke();
        }
    }

    //public void OnAssimilation(InputAction.CallbackContext assimilateInput)
    //{
    //    playerMovementScript.Assimilate();
    //}

    //public void OnItemChange(InputAction.CallbackContext itemInput)
    //{
    //    if (itemInput.started)
    //    {
    //        playerMovementScript.ChangeItemSelected();
    //    }
    //}

    //public void UseItem(InputAction.CallbackContext itemInput)
    //{
    //    if (itemInput.started)
    //    {
    //        playerMovementScript.UseItem();
    //    }
    //}

    //public void OnPause(InputAction.CallbackContext pause)
    //{
    //    if (pause.started)
    //    {
    //        playerMovementScript.TogglePause();
    //    }
    //}
}
