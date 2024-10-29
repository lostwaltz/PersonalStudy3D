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
    public bool OpenUI { get; set; }

    private void Awake()
    {
        OpenUI = false;

        Cursor.lockState = CursorLockMode.Locked;

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
        states.Add(new ClimbStatePlayer(GetComponent<Rigidbody>()));
        states.Add(new InteractionStatePlayer());

        stateMachine.Initialize(states);
    }
}
