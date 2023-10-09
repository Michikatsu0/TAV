using UnityEngine;

[RequireComponent(typeof(MechanicsController))]
public class AnimatorPlayerController : MonoBehaviour
{
    private enum PlayerState
    {
        Movement,
        IdleBreaker,
        Jump,
        IsFalling
    }

    [HideInInspector] public Animator animator;
    private PlayerState currentState;
    private MechanicsController mechanics;
    private PlayerIKMechanicsResponse iKMechanics;
    private int HCMoveZ = Animator.StringToHash("MoveZ");
    private int HCMoveX = Animator.StringToHash("MoveX");

    private int HCMovement = Animator.StringToHash("IsMoving");
    private int HCIdleBreaker = Animator.StringToHash("IsIdleBreaking");
    private int HCJump = Animator.StringToHash("IsJumping");
    private int HCIsGrounded = Animator.StringToHash("IsFalling");
    private int HCIsAiming = Animator.StringToHash("IsAiming");

    public bool isAiming;
    int clics = 0;
    private void Start()
    {
        animator = GetComponent<Animator>();
        mechanics = GetComponent<MechanicsController>();
        iKMechanics = GetComponent<PlayerIKMechanicsResponse>();
        SetState(PlayerState.Movement);
    }

    private void Update()
    {
        if (mechanics.IsAiming())
        {
            isAiming = true;
            clics++;
            if (clics >= 2)
            {
                isAiming = false;
                clics = 0;
            }
        }

        if (isAiming)
        {
            animator.SetBool(HCIsAiming, true);
            iKMechanics.TriggerWeapon(mechanics.IsFiring());
        }
        else
        {
            animator.SetBool(HCIsAiming, false);
            iKMechanics.TriggerWeapon(false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat(HCMoveX, mechanics.moveDirection.x);
            animator.SetFloat(HCMoveZ, mechanics.moveDirection.z);
            if (mechanics.moveDirection.x != 0 && mechanics.moveDirection.z == 0)
            {
                animator.SetFloat(HCMoveX, mechanics.moveDirection.x / 2);
                animator.SetFloat(HCMoveZ, mechanics.moveDirection.z / 2);
            }
        }
        else
        {
            animator.SetFloat(HCMoveX, mechanics.moveDirection.x / 2);
            animator.SetFloat(HCMoveZ, mechanics.moveDirection.z / 2);
        }
        
        PlayerState newState = DeterminateState();
        if (newState != currentState)
            SetState(newState);
    }

    private void SetState(PlayerState newState)
    {
        switch (currentState)
        {
            case PlayerState.Movement:
                animator.SetBool(HCMovement, false);
                break;
            case PlayerState.IdleBreaker:
                animator.SetBool(HCIdleBreaker,false);
                break;
            case PlayerState.Jump:
                animator.SetBool(HCJump, false);
                break;
            case PlayerState.IsFalling:
                animator.SetBool(HCIsGrounded, false);
                break;
        }
        switch (newState)
        {
            case PlayerState.Movement:
                animator.SetBool(HCMovement, true);
                break;
            case PlayerState.IdleBreaker:
                animator.SetBool(HCIdleBreaker, true);
                break;
            case PlayerState.Jump:
                animator.SetBool(HCJump, true);
                break;
            case PlayerState.IsFalling:
                animator.SetBool(HCIsGrounded, true);
                break;
        }
        currentState = newState;
    }

    private PlayerState DeterminateState()
    {
        if (mechanics.IsIdleBreaking())
            return PlayerState.IdleBreaker;
        else if (mechanics.IsJumping())
        {
            mechanics.Gravity();
            return PlayerState.Jump;
        }
        else if (!mechanics.IsFalling())
        {
            mechanics.Gravity();
            return PlayerState.IsFalling;
        }
        else
            return PlayerState.Movement;
    }
    Vector3 center;
    private void OnDrawGizmosSelected()
    {
        var mechanicsController = GetComponent<MechanicsController>();
        center.y = -mechanicsController.maxDistance;
        Gizmos.color = Color.red;
        var characterController = GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(transform.position + center, characterController.radius);
    }
}
