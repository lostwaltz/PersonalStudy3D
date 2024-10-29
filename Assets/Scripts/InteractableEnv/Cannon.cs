using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour, IInteractable
{
    public Canvas canvas;

    public Transform trowProjectilTransform;

    private void Awake()
    {
    }

    public void OnHitRay(Vector3 hitPoint, float hitDistance)
    {
        canvas.enabled = true;
        canvas.transform.position = transform.position + (transform.up * 3f);
        canvas.transform.LookAt(Camera.main.transform);

        canvas.GetComponentInChildren<TMP_Text>().text = "EŰ�� ���� ���ư���";
    }

    private void Update()
    {
        canvas.enabled = false;
    }

    public void Oninteract()
    {
        StateMachine stateMachine = CharacterManager.Instance.Player.GetComponent<StateMachine>();
        stateMachine.TransitionTo(stateMachine.states["Interactive"]);

        CharacterManager.Instance.Player.transform.position = trowProjectilTransform.position;
        CharacterManager.Instance.Player.GetComponent<Rigidbody>().AddForce(trowProjectilTransform.up * 20f, ForceMode.Impulse);
    }
}
