using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CharacterEventContainer : MonoBehaviour
{
    private event Action<Vector2> onMoveEvent;
    private event Action<Vector2> onLookEvent;
    private event Action<bool> onRunEvent;
    private event Action onJumpEvent;
    private event Action onHitEvent;
    private event Action<State> onStateChangedEvent;

    private event Action<float> onDamage;
    private event Action<float> onHeal;
    private event Action onDeath;
    private event Action oninvinciblilityEnd;

    // For Behaviour
    public Action<Vector2> OnMoveEvent { get { return onMoveEvent; } set { onMoveEvent += value; } }
    public Action<Vector2> OnLookEvent { get { return onLookEvent; } set { onLookEvent += value; } }
    public Action<bool> OnRunEvent { get { return onRunEvent; } set { onRunEvent += value; } }
    public Action OnJumpEvent { get { return onJumpEvent; } set { onJumpEvent += value; } }
    public Action OnHitEvent { get { return onHitEvent; } set { onHitEvent += value; } }
    public Action<State> OnStateChangedEvent {  get { return onStateChangedEvent; } set { onStateChangedEvent += value; } }


    // For Healht
    public Action<float> OnDamage { get { return onDamage; } set { onDamage += value; } }
    public Action<float> OnHeal { get { return onHeal; } set { onHeal += value; } }
    public Action OnDeath { get { return onDeath; } set { onDeath += value; } }
    public Action OninvinciblilityEnd { get { return oninvinciblilityEnd; } set { oninvinciblilityEnd += value; } }
}
