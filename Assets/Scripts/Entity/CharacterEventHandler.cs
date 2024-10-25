using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterEventHandler : MonoBehaviour
{
    private event Action<Vector2> OnMoveEvent;
    private event Action<Vector2> OnLookEvent;
    private event Action OnJumpEvent;

    public void SubscribeMoveEvent(Action<Vector2> action)
    {
        OnMoveEvent += action;
    }

    public void SubscribeLookEvent(Action<Vector2> action)
    {
        OnLookEvent += action;
    }
    public void SubscribeJumpEvent(Action action)
    {
        OnJumpEvent += action;
    }


    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector3 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    public void CallJumpEvent()
    {
        OnJumpEvent?.Invoke();
    }
}
