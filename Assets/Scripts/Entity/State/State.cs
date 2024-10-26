using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Movement movement { get; private set; }
    protected CharacterEventHandler eventHandler { get; private set; }
    protected StateMachine machine { get; private set; }
    protected Animator animator { get; private set; }

    public string tag { get; protected set; }

    public void InitState(StateMachine machine, Movement movement, CharacterEventHandler eventHandler, Animator animator)
    {
        this.movement = movement;
        this.eventHandler = eventHandler;
        this.machine = machine;
        this.animator = animator;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
