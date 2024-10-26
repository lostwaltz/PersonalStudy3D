using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool isRun { get; private set; }
    public bool isWalk { get; private set; }
    public bool isJumpTrigered { get; private set; }

    private Vector2 curMovementInput;
    
    private LayerMask groundLayerMask;

    [Header("Look")]
    [SerializeField] private Vector2 curMouseDeltaInput;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float curRotAxisX;
    [SerializeField] private Transform headTransform;

    private CharacterEventHandler eventHandler;
    private Rigidbody movementRigidbody;

    private void Awake()
    {
        isJumpTrigered = false;
        movementRigidbody = GetComponent<Rigidbody>();
        eventHandler = GetComponent<CharacterEventHandler>();
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    private void Start()
    {
        eventHandler.SubscribeMoveEvent(CallbackSetMoveDirection, false);
        eventHandler.SubscribeLookEvent(CallbackSetMouseDelta, false);
        eventHandler.SubscribeJumpEvent(CallbackJump, false);
        eventHandler.SubscribeRunEvent(CallbackRun, false);
    }

    public void CallbackSetMoveDirection(Vector2 direaction)
    {
        isWalk = (Mathf.Abs(direaction.x) > 0f || Mathf.Abs(direaction.y) > 0f);
        curMovementInput = direaction;
    }

    public void CallbackSetMouseDelta(Vector2 mouseDelta)
    {
        curMouseDeltaInput = mouseDelta;
    }
    public void CallbackJump()
    {
        if (false == isGround())
            return;

        isJumpTrigered = true;
    }
    public void CallbackRun(bool isRun)
    {
        this.isRun = isRun;
    }


    public void ApplyMovement()
    {
        Vector3 direction = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        //TODO 매직넘버 수정
        float speed = isRun ? 5f : 2f;

        direction = direction * speed;
        direction.y = movementRigidbody.velocity.y;

        movementRigidbody.velocity = direction;
    }
    public void ApplyLook()
    {
        //TODO 매직넘버 수정
        curRotAxisX += curMouseDeltaInput.y * lookSensitivity;
        curRotAxisX = Mathf.Clamp(curRotAxisX, -55, 75f);

        headTransform.localEulerAngles = new Vector3(-curRotAxisX, 0, 0);
        transform.eulerAngles += new Vector3(0, curMouseDeltaInput.x * lookSensitivity, 0);
    }
    public void ApplyJump()
    {
        if (false == isJumpTrigered)
            return;

        //TODO : 매직 넘버 수정
        movementRigidbody.AddForce(transform.up * 5f, ForceMode.Impulse);

        isJumpTrigered = false;
    }

    public bool isGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }

        }
        return false;
    }
}
