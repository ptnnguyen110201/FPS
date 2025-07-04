using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputProvider : MonoBehaviour, InputSystem_Actions.IPlayerActions, IInputProvider
{
    private InputSystem_Actions inputActions;
    
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsSprinting { get; private set; }
    public bool IsAiming { get; private set; }
    public bool isAttacking { get; private set; }

    public event Action OnJumpPressed;
    public event Action OnReloadPressed;
    public event Action OnWeaponChanged;
    public event Action ESCPressed;
    private void Awake()
    {
        this.inputActions = new InputSystem_Actions();
        this.inputActions.Player.SetCallbacks(this);
    }

    private void OnEnable() => this.inputActions.Player.Enable();
    private void OnDisable() => this.inputActions.Player.Disable();

    public void OnMove(InputAction.CallbackContext context)
        => this.MoveInput = context.ReadValue<Vector2>();

    public void OnLook(InputAction.CallbackContext context)
        => this.LookInput = context.ReadValue<Vector2>();

    public void OnSprint(InputAction.CallbackContext context)
        => this.IsSprinting = context.ReadValueAsButton();

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            this.OnJumpPressed?.Invoke();
    }
    public void OnAttack(InputAction.CallbackContext context)
        => this.isAttacking = context.ReadValueAsButton();

    public void OnAim(InputAction.CallbackContext context)
        => this.IsAiming = context.ReadValueAsButton();



    public void OnChanged(InputAction.CallbackContext context)
    {
        if (context.performed)
            this.OnWeaponChanged?.Invoke();

    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
            this.OnReloadPressed?.Invoke();
    }

    public void OnESC(InputAction.CallbackContext context)
    {
        if (context.performed)
            this.ESCPressed?.Invoke();
    }
}
