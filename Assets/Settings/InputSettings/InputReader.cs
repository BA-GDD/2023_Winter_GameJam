using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/Setting/InputReader")]
public class InputReader : ScriptableObject, Controls.IPlayerActions
{
    public event Action onDashEvent;
    public event Action onReloadEvent;
    public event Action onShootEvent;
    public event Action onSkillEvent;
    public Vector2 movementDirection;
    private Controls _controls;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();

            _controls.Player.SetCallbacks(this);
        }

        _controls.Player.Enable();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        onDashEvent?.Invoke();
    }

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

    public void OnSkill(InputAction.CallbackContext context)
    {
        onSkillEvent?.Invoke();
    }
}
