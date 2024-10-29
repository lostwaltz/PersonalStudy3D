using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool isRun { get; private set; }
    public bool isWalk { get; private set; }
    public bool isJumpTrigered { get; private set; }
    private int jumpCount;
    private int maxJumpCount = 1;

    private Vector2 curMovementInput;

    [Header("Look")]
    [SerializeField] private Vector2 curMouseDeltaInput;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float curRotAxisX;
    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform CameraTransform;

    private CharacterEventContainer eventHandler;
    private Rigidbody movementRigidbody;
    private VitalController vitalController;

    private CharacterStatsHandler statsHandler;

    float ExtraSpeed;

    private void Awake()
    {
        jumpCount = maxJumpCount;
        isJumpTrigered = false;
        movementRigidbody = GetComponent<Rigidbody>();
        eventHandler = GetComponent<CharacterEventContainer>();
        vitalController = GetComponent<VitalController>();
        statsHandler = GetComponent<CharacterStatsHandler>();

        CameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        eventHandler.OnMoveEvent    += CallbackSetMoveDirection;
        eventHandler.OnLookEvent    += CallbackSetMouseDelta;
        eventHandler.OnJumpEvent    += CallbackJump;
        eventHandler.OnRunEvent     += CallbackRun;

        vitalController.OnVitalValueChange += (float Health, float Stamina) => { if (0f >= Stamina) isRun = false; };
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
        if (true == GetComponent<RayCheck>().isGround())
            jumpCount = maxJumpCount;

        if ((false == GetComponent<RayCheck>().isGround() && false == GetComponent<RayCheck>().isLadderOnFront()) && jumpCount <= 0)
           return;

        jumpCount--;

        isJumpTrigered = true;
    }

    public void CallbackRun(bool isRun)
    {
        if (1f >= CharacterManager.Instance.Player.GetComponent<VitalController>().CurStamina)
            isRun = false;

        this.isRun = isRun;
    }


    public void ApplyMovement()
    {
        Vector3 direction = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        float speed = isRun ? statsHandler.CurrentStat.speed * 2f : statsHandler.CurrentStat.speed;

        direction = direction * (speed + ExtraSpeed);
        direction.y = movementRigidbody.velocity.y;

        movementRigidbody.velocity = direction;
    }
    public void ApplyCliming()
    {
        Vector3 direction = transform.up * curMovementInput.y + transform.right * curMovementInput.x;
        float speed = isRun ? statsHandler.CurrentStat.speed * 2f : statsHandler.CurrentStat.speed;

        direction = direction * (speed + ExtraSpeed);

        movementRigidbody.velocity = direction;
    }


    public void ApplyLook()
    {
        if (true == CharacterManager.Instance.Player.OpenUI)
            return;

        //TODO change magic number / magic number -> value
        curRotAxisX += curMouseDeltaInput.y * lookSensitivity;
        curRotAxisX = Mathf.Clamp(curRotAxisX, -55, 75f);

        headTransform.localEulerAngles = new Vector3(-curRotAxisX, 0, 0);

        CameraTransform.position = transform.position + (Vector3.up * 1.2f);
        CameraTransform.localEulerAngles = new Vector3(-curRotAxisX, 0, 0);
        CameraTransform.position = CameraTransform.position + (- CameraTransform.forward * 1.5f);

        transform.eulerAngles += new Vector3(0, curMouseDeltaInput.x * lookSensitivity, 0);
    }
    public void ApplyJump()
    {
        if (false == isJumpTrigered)
            return;

        //TODO : change magic number / magic number -> value
        Vector3 vec = movementRigidbody.velocity;
        vec.y = 0;

        movementRigidbody.velocity = vec;
        movementRigidbody.AddForce(transform.up * 5f, ForceMode.Impulse);

        isJumpTrigered = false;
    }

    public void StartSpeedBoost()
    {
        StartCoroutine(SpeedBoost());
    }
    public void StartDobleJump()
    {
        StartCoroutine(DobleJump());
    }

    IEnumerator SpeedBoost()
    {
        ExtraSpeed = 2f;
        yield return new WaitForSeconds(5f);
        ExtraSpeed = 0f;
    }
    IEnumerator DobleJump()
    {
        maxJumpCount = 2;

        yield return new WaitForSeconds(5f);

        maxJumpCount = 1;
    }
}

