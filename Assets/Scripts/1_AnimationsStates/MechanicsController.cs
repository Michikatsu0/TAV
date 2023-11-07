using UnityEngine;

public class MechanicsController : MonoBehaviour
{
    private AnimatorPlayerController playerAnim;
    private PlayerHealthResponse healthResponse;
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Vector3 moveDirection, movementDirection, gravityDirection;
    [SerializeField] private Transform cam;
    [SerializeField] private LayerMask isGrounded;
    [SerializeField] public float animLayerSmooth1,smoothTime = 0.05f, runSpeed, walkSpeed, centerDistance, jumpForce, groundGravity, airGravity, crouchHeight, standHeight, crouchSpeed, centerCrouch; 
    [HideInInspector] public bool canJump, isAiming;
    private float time, delay = 5f, currentSpeed, angle ,rotationSpeed, verticalVelocity = 0;
    [HideInInspector] public float animLayer1;
    Vector3 centerCharacter;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnim = GetComponent<AnimatorPlayerController>();
        healthResponse = GetComponent<PlayerHealthResponse>();    
    }

    private void FixedUpdate()
    {
        if (healthResponse.deathScript) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0f, vertical);

        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = runSpeed;
        else
        {
            currentSpeed = walkSpeed;
            if (IsCrouching())
                currentSpeed = crouchSpeed;
        }
        

        if (playerAnim.animator.GetBool(playerAnim.HCIsAiming))
        {
            angle = RotationAim();
            movementDirection = transform.forward * vertical + transform.right * horizontal;
        }
        else
        {
            if (moveDirection.magnitude >= 0.1f)
                angle = RotationMove(moveDirection);
        }

        characterController.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        
        if (moveDirection.magnitude >= 0.1f)
            characterController.Move(movementDirection * (currentSpeed * Time.deltaTime));
        
        Gravity(); 
        Crouch();
    }
    
    public float RotationMove(Vector3 direction)
    {
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        movementDirection = camForward * direction.z + camRight * direction.x;
        
        float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;
        return Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, smoothTime);
    }
    
    public float RotationAim()
    {
        float targetAngle = cam.eulerAngles.y;
        return Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, smoothTime);
    }
    
    public bool IsIdle()
    {
        return moveDirection is { x: 0.0f, z: 0.0f };
    }
    
    public bool IsMoving()
    {
        return moveDirection.x != 0 || moveDirection.z != 0;
    }

    public void Crouch()
    {
        if (IsCrouching())
        {
            characterController.height = crouchHeight;
            centerCharacter.y = centerCrouch;
            characterController.center = centerCharacter;
        }
        else
        {
            characterController.height = standHeight;
            characterController.center = Vector3.zero;
        }
    }

    public bool IsCrouching()
    {
        return Input.GetKey(KeyCode.C) && !IsAiming() && !Input.GetKey(KeyCode.LeftShift);
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
        if (IsGrounded())
        {
            if (Input.GetKey(KeyCode.Space))
                return true;
            else
                return false;
        }
        else
            return false;
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
