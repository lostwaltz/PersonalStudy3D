using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CharacterEventHandler : MonoBehaviour
{
    private event Action<Vector2> OnMoveEvent;
    private event Action<Vector2> OnLookEvent;
    private event Action<bool> OnRunEvent;
    private event Action OnJumpEvent;
    private event Action OnHitEvent;

    private event Action<State> stateChanged;

    public void SubscribeMoveEvent(Action<Vector2> action, bool isUnSub)
    {
        if (isUnSub)
        {
            OnMoveEvent -= action;
            return;
        }

        OnMoveEvent += action;
    }
    public void SubscribeLookEvent(Action<Vector2> action, bool isUnSub)
    {
        if (isUnSub)
        {
            OnLookEvent -= action;
            return;
        }
        OnLookEvent += action;
    }
    public void SubscribeJumpEvent(Action action, bool isUnSub)
    {
        if (isUnSub)
        {
            OnJumpEvent -= action;
            return;
        }
        OnJumpEvent += action;
    }
    public void SubscribeStateEvent(Action<State> action, bool isUnSub)
    {
        if (isUnSub)
        {
            stateChanged -= action;
            return;
        }
        stateChanged += action;
    }
    public void SubscribeRunEvent(Action<bool> action, bool isUnSub)
    {
        if (isUnSub)
        {
            OnRunEvent -= action;
            return;
        }
        OnRunEvent += action;
    }
    public void SubscribeOnHitEvent(Action action, bool isUnSub)
    {
        if (isUnSub)
        {
            OnHitEvent -= action;
            return;
        }
        OnHitEvent += action;
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
    public void CallStateChange(State state)
    {
        stateChanged?.Invoke(state);
    }
    public void CallRunEvent(bool isRun)
    {
        OnRunEvent?.Invoke(isRun);
    }
    public void CallHitEvent()
    {
        OnHitEvent?.Invoke();
    }

}
