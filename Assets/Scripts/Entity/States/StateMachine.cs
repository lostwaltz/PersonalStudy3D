using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    public Dictionary<string, State> states = new Dictionary<string, State>();

    private CharacterEventContainer eventHandler;

    private void Awake()
    {
        eventHandler = gameObject.GetComponent<CharacterEventContainer>();
    }

    public void Initialize(List<State> Initstates)
    {
        CurrentState = Initstates[0];

        for (int i = 0; i < Initstates.Count; i++)
        {
            Initstates[i].InitState(this, gameObject.GetComponent<Movement>(), eventHandler, gameObject.GetComponent<Animator>(), gameObject.GetComponent<VitalController>());

            string key = Initstates[i].tag;

            states[key] = Initstates[i];
        }

        Initstates[0].Enter();
        eventHandler.OnStateChangedEvent?.Invoke(Initstates[0]);
    }

    public void TransitionTo(State nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();

        eventHandler.OnStateChangedEvent?.Invoke(nextState);
    }

    public void LateUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.Execute();
        }
    }
}
