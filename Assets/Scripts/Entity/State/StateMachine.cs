using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    public Dictionary<string, State> states = new Dictionary<string, State>();

    private CharacterEventHandler eventHandler;

    private void Awake()
    {
        eventHandler = gameObject.GetComponent<CharacterEventHandler>();
    }

    public void Initialize(List<State> Initstates)
    {
        CurrentState = Initstates[0];

        for (int i = 0; i < Initstates.Count; i++)
        {
            Initstates[i].InitState(this, gameObject.GetComponent<Movement>(), eventHandler, gameObject.GetComponent<Animator>());

            string key = Initstates[i].tag;

            states[key] = Initstates[i];
        }

        Initstates[0].Enter();
        eventHandler.CallStateChange(Initstates[0]);
    }

    public void TransitionTo(State nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();

        eventHandler.CallStateChange(nextState);
    }


    public void Execute()
    {
        if (CurrentState != null)
        {
            CurrentState.Execute();
        }
    }
}
