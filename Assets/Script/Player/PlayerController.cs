using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;
    public float useStamina;

    [Header("Ladder Climbing")]
    public float climbSpeed = 3f; // 사다리 등반 속도 추가
    private bool isClimbing = false;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (isClimbing) // 사다리 상태일 때는 Climb() 실행
            Climb();

        Move();
    }

    public void StartClimbing()
    {
        isClimbing = true;
        rigidbody.useGravity = false; // 중력 제거
        rigidbody.velocity = Vector3.zero;
    }

    //  사다리에서 벗어나면 원래 상태로 복귀
    public void StopClimbing()
    {
        isClimbing = false;
        rigidbody.useGravity = true; // 중력 복구
    }

    //  사다리 등반 로직 (W/S 키를 이용한 위아래 이동)
    private void Climb()
    {
        float verticalMove = curMovementInput.y * climbSpeed;
        rigidbody.velocity = new Vector3(0, verticalMove, 0); //  X, Z 이동 없이 Y축으로만 움직임
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            if (CharacterManager.Instance.Player.condition.UseStamina(useStamina))
            {
                rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            }
                
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    public void ForceJump(float jumpPower)
    {
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
        rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    public IEnumerator SpeedBoost(float amount, float duration)
    {
        moveSpeed += amount; // 이동 속도 증가
        yield return new WaitForSeconds(duration); // 일정 시간 대기
        moveSpeed -= amount; // 원래 속도로 복구
    }

    public IEnumerator JumpBoost(float amount, float duration)
    {
        jumpPower += amount; // 점프력 증가
        yield return new WaitForSeconds(duration); // 20초 대기
        jumpPower -= amount; // 원래 점프력으로 복구
    }
}
