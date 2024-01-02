using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ScriptableObject/InputReader")]
public class InputReader : ScriptableObject, Controls.IPlayerActions
{
    public event Action onReloadEvent;
    public event Action onShootEvent;
    public Vector2 movementDirection;

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        onReloadEvent?.Invoke();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        onShootEvent?.Invoke();
    }
}
