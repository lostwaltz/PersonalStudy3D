using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector2 curMovementInput;

    [Header("Look")]
    [SerializeField] private Vector2 curMouseDeltaInput;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float curRotAxisX;
    [SerializeField] private float curRotAxisY;
    [SerializeField] private Transform headTransform;



    private CharacterEventHandler eventHandler;
    private Rigidbody movementRigidbody;

    private void Awake()
    {
        movementRigidbody = GetComponent<Rigidbody>();
        eventHandler = GetComponent<CharacterEventHandler>();
    }

    private void Start()
    {
        eventHandler.SubscribeMoveEvent(CallbackSetMoveDirection);
        eventHandler.SubscribeLookEvent(CallbackSetMouseDelta);
        eventHandler.SubscribeJumpEvent(CallbackJump);
    }

    void Update()
    {
        ApplyMovement();
        ApplyLook();
    }

    public void CallbackSetMoveDirection(Vector2 direction)
    {
        curMovementInput = direction;
    }
    public void CallbackSetMouseDelta(Vector2 mouseDelta)
    {
        curMouseDeltaInput = mouseDelta;
    }

    public void ApplyMovement()
    {
        Vector3 direction = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        direction = direction * 5f;
        direction.y = movementRigidbody.velocity.y;

        //TODO 매직넘버 수정
        movementRigidbody.velocity = direction;
    }
    public void ApplyLook()
    {
        //TODO 매직넘버 수정
        curRotAxisX += curMouseDeltaInput.y * lookSensitivity;
        curRotAxisX = Mathf.Clamp(curRotAxisX, -55, 75f);

        curRotAxisY += curMouseDeltaInput.x * lookSensitivity;
        curRotAxisY = curRotAxisY % 360f;

        transform.eulerAngles = new Vector3(0, curRotAxisY, 0);
        headTransform.localEulerAngles = new Vector3(-curRotAxisX, 0, 0);
    }

    public void CallbackJump()
    {
        //TODO 매직넘버 수정
        movementRigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
}
