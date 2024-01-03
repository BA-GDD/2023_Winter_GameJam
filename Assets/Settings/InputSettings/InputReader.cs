using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/Setting/InputReader")]
public class InputReader : ScriptableObject, Controls.IPlayerActions
{
    public event Action onDashEvent;
    public event Action onShootEvent;
    public Vector2 movementDirection;
    public bool isReload;
    public bool isSkillOccur;
    public bool isSkillPrepare;
    private Controls _controls;

    public AudioClip reloadClip;

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
        if (context.performed)
        {
            onDashEvent?.Invoke();
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SoundManager.Instance.Play(reloadClip, 100, 1, 2, true, "ReloadSE");
            isReload = true;
        }
        else if (context.canceled)
        {
            SoundManager.Instance.Stop("ReloadSE");
            isReload = false;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onShootEvent?.Invoke();
        }
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSkillPrepare = true;
        }
        else if (context.canceled)
        {
            isSkillOccur = true;
        }
    }
}
