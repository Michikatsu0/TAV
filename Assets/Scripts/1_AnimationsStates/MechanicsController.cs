using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MechanicsController : MonoBehaviour
{
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Vector3 moveDirection, gravityDirection;
    [SerializeField] private LayerMask isGrounded;
    [SerializeField] public float smoothTime = 0.05f, runSpeed, walkSpeed,centerDistance, jumpForce, groundGravity, airGravity; 
    private float time, delay = 5f, currentSpeed, rotationSpeed, verticalVelocity = 0;
    Vector3 centerCharacter;
    [HideInInspector] public bool canJump;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.z = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveDirection.x, 0f, moveDirection.z).normalized;
        
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        if (direction.magnitude >= 0.1f)
        {
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 directionMove = camForward * moveDirection.z + camRight * moveDirection.x;

            float targetAngle = Mathf.Atan2(directionMove.x, directionMove.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, smoothTime);
        
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            if (IsMoving() && !Input.GetKey(KeyCode.LeftShift))
                characterController.Move(directionMove * (currentSpeed * Time.deltaTime));
            else if (IsMoving() && Input.GetKey(KeyCode.LeftShift))
                characterController.Move(directionMove.normalized * (currentSpeed * Time.deltaTime));
        }

        Gravity();
    }

    public bool IsIdle()
    {
        return moveDirection is { x: 0.0f, z: 0.0f };
    }
    public bool IsMoving()
    {
        return moveDirection.x != 0 || moveDirection.z != 0;
    }

    public void Gravity()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -groundGravity;

            if (canJump && Input.GetButton("Jump"))
            {
                canJump = false;
                verticalVelocity = jumpForce;
            }
            else if (!Input.GetButton("Jump"))
            {
                canJump = true;
            }
        }
        else 
        {
            verticalVelocity -= airGravity * Time.deltaTime; 
        }

        gravityDirection.y = verticalVelocity;
        characterController.Move(gravityDirection * Time.deltaTime);
    }

    public bool IsJumping()
    {
        return Input.GetKey(KeyCode.Space) && IsGrounded();
    }

    public bool IsGrounded()
    {
        return Physics.SphereCast(transform.position, characterController.radius, Vector3.down, out RaycastHit hit, centerDistance, isGrounded);
    }

    public bool IsIdleBreaking()
    {
        if (IsIdle())
        {
            time += Time.deltaTime;
            if (time >= delay)
            {
                time = -delay;
                return true;
            }
            else
                return false;
        }
        else
        {
            time = 0;
            return false;
        }
    }

    public bool IsAiming()
    {
        return Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftShift);
    }

    public bool IsFiring()
    {
        return Input.GetMouseButton(0);
    }

    Vector3 center;

    private void OnDrawGizmosSelected()
    {
        CharacterController characterController = GetComponent<CharacterController>();
        center.y = -centerDistance;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + center, characterController.radius);
    }
}
