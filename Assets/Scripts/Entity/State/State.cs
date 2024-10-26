using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public Movement movement { get; set; }
    public CharacterEventHandler eventHandler { get; set; }
    public StateMachine machine { get; set; }
    public Animator animator { get; set; }

    public string tag { get; protected set; }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
