using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Player : MonoBehaviour
{
    public PlayerInputController controller {  get; private set; }

    private void Awake()
    {
        CharacterManager.Instance.Player = this;

        controller = GetComponent<PlayerInputController>();

        InitPlayerComponent();
        InitPlayerState();
    }

    private void Start()
    {
        UIManager.Instance.ShowPopupUI<UI_VitalDisplay>("UI_VitalDisplay");

    }
    private void InitPlayerComponent()
    {
        gameObject.GetOrAddComponent<VitalController>();
    }
    private void InitPlayerState()
    {
        StateMachine stateMachine = gameObject.AddComponent<StateMachine>();

        List<State> states = new List<State>();
        states.Add(new IdleStatePlayer());
        states.Add(new WalkStatePlayer());
        states.Add(new RunStatePlayer());
        states.Add(new JumpStatePlayer());

        stateMachine.Initialize(states);
    }
}
