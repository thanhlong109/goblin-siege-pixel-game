using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls controls;

    public float valueX;
    public bool isJump;


    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Player.Movement.performed += StartMove;
        controls.Player.Movement.canceled += StopMove;

        controls.Player.Jump.performed += StartJump;
        controls.Player.Jump.canceled += StopJump;

        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Movement.performed -= StartMove;
        controls.Player.Movement.canceled -= StopMove;

        controls.Player.Jump.performed -= StartJump;
        controls.Player.Jump.canceled -= StopJump;

        controls?.Player.Disable();
    }

    private void StartMove(InputAction.CallbackContext ctx)
    {
        valueX = ctx.ReadValue<float>();
    }

    private void StopMove(InputAction.CallbackContext ctx)
    {
        valueX = 0f;
    }

    private void StartJump(InputAction.CallbackContext ctx)
    {
        isJump = true;
    }

    private void StopJump(InputAction.CallbackContext ctx)
    {
        isJump = false;
    }

}
