using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerInputController : MonoBehaviour
{
    private CharacterEventContainer eventHandler;

    public Action OnOpenInventoryEvent { get; set; }
    public Action OnInteractiveEvent { get; set; }

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
        {
            eventHandler.OnJumpEvent?.Invoke();
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            eventHandler.OnRunEvent?.Invoke(true);
        else if (context.phase == InputActionPhase.Canceled)
            eventHandler.OnRunEvent?.Invoke(false);
    }
    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            OnOpenInventoryEvent?.Invoke();
    }
    public void OnInteractive(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            OnInteractiveEvent?.Invoke();
    }
}
