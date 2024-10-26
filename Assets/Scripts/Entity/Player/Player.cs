using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    public Movement movement { get; private set; }
    public CharacterEventHandler eventHandler { get; private set; }

    private void Awake()
    {
        CharacterManager.Instance.Player = this;

        movement = GetComponent<Movement>();
        eventHandler = GetComponent<CharacterEventHandler>();

        InitPlayerState();

        //Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        stateMachine.Execute();
    }

    private void InitPlayerState()
    {
        stateMachine = gameObject.AddComponent<StateMachine>();

        List<State> states = new List<State>();
        states.Add(new IdleStatePlayer());
        states.Add(new WalkStatePlayer());
        states.Add(new RunStatePlayer());
        states.Add(new JumpStatePlayer());

        stateMachine.Initialize(states);
    }
}
