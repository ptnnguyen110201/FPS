using System;
using UnityEngine;

public interface IInputProvider 
{
    Vector2 MoveInput { get; }
    Vector2 LookInput { get; }
    bool isAttacking { get; }
    bool IsSprinting { get; }
    bool IsAiming { get; }

    event Action OnJumpPressed;
    event Action OnReloadPressed;
    event Action OnWeaponChanged;
    event Action ESCPressed;
}
