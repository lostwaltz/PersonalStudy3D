using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        CharacterManager.Instance.Player = this;

        InitPlayerComponent();
        InitPlayerState();

        //Cursor.lockState = CursorLockMode.Locked;
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
