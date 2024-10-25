using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public CharacterEventHandler eventHandler;

    private void Awake()
    {
        eventHandler = GetComponent<CharacterEventHandler>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        eventHandler.CallMoveEvent(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        eventHandler.CallLookEvent(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
            eventHandler.CallJumpEvent();
    }
}
