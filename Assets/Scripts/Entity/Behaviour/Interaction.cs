using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public interface IInteractable
{
    public void OnHitRay(Vector3 hitPoint, float hitDistance);
    public void Oninteract();
}

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    public float maxCheckDistance;
    private float lastCheckTime;

    public LayerMask layerMask;
    public IInteractable curInteractable;
    private Vector3 hitPoint;

    private Camera mainCamera;
    private PlayerInputController controller;
    // Start is called before the first frame update
    void Start()
    {
        hitPoint = Vector3.zero;
        controller = GetComponent<PlayerInputController>();
        mainCamera = Camera.main;

        controller.OnInteractiveEvent += OnInteractCallback;
    }

    // Update is called once per frame
    void Update()
    {
        curInteractable?.OnHitRay(hitPoint, 5f);

        lastCheckTime += Time.time;
        if (lastCheckTime < checkRate)
            return;
        
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out RaycastHit hit, 5f, layerMask))
        {
            hitPoint = hit.point;
            curInteractable = hit.collider.gameObject.GetComponent<IInteractable>();
            UIManager.Instance.uiContainer["UI_InfoBox"].gameObject.SetActive(true);
        }
        else
        {
            hitPoint = Vector3.zero;
            curInteractable = null;
            UIManager.Instance.uiContainer["UI_InfoBox"].gameObject.SetActive(false);
        }

        lastCheckTime = 0f;
    }

    public void OnInteractCallback()
    {
        curInteractable?.Oninteract();
        curInteractable = null;
    }
}
