using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public CharacterEventContainer eventHandler;

    private void Awake()
    {
        eventHandler = GetComponent<CharacterEventContainer>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        eventHandler.OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        eventHandler.OnLookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            eventHandler.OnJumpEvent?.Invoke();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            eventHandler.OnRunEvent?.Invoke(true);
        else if (context.phase == InputActionPhase.Canceled)
            eventHandler.OnRunEvent?.Invoke(false);
    }

}
