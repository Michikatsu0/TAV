using UnityEngine;

[RequireComponent(typeof(MechanicsController))]
public class AnimatorPlayerController : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        Movement,
        IdleBreaker,
        Jump
    }

    [HideInInspector] public Animator animator;
    private PlayerState currentState;
    private MechanicsController mechanics;
    private PlayerIKMechanicsResponse iKMechanics;
    private int HCMoveZ = Animator.StringToHash("MoveZ");
    private int HCMoveX = Animator.StringToHash("MoveX");

    private int HCIdle = Animator.StringToHash("IsIdle");
    private int HCMovement = Animator.StringToHash("IsMoving");
    private int HCIdleBreaker = Animator.StringToHash("IsIdleBreaking");
    private int HCJump = Animator.StringToHash("IsJumping");
    private int HCIsFalling= Animator.StringToHash("IsFalling");
    private int HCIsAiming = Animator.StringToHash("IsAiming");

    public bool isAiming;
    int clicks = 0;
    private void Start()
    {
        animator = GetComponent<Animator>();
        mechanics = GetComponent<MechanicsController>();
        iKMechanics = GetComponent<PlayerIKMechanicsResponse>();
        SetState(PlayerState.Movement);
    }

    private void Update()
    {
        mechanics.IsFalling();

        animator.SetBool(HCIsFalling, mechanics.IsFalling());

        if (mechanics.IsAiming())
        {
            isAiming = true;
            clicks++;
            if (clicks >= 2)
            {
                isAiming = false;
                clicks = 0;
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
            animator.SetBool(HCIsAiming, false);
            iKMechanics.TriggerWeapon(false);
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
            case PlayerState.Idle:
                animator.SetBool(HCIdle, false);
                break;
            case PlayerState.Movement:
                animator.SetBool(HCMovement, false);
                break;
            case PlayerState.IdleBreaker:
                animator.SetBool(HCIdleBreaker,false);
                break;
            case PlayerState.Jump:
                animator.SetBool(HCJump, false);
                break;
        }
        switch (newState)
        {
            case PlayerState.Idle:
                animator.SetBool(HCIdle, true);
                break;
            case PlayerState.Movement:
                animator.SetBool(HCMovement, true);
                break;
            case PlayerState.IdleBreaker:
                animator.SetBool(HCIdleBreaker, true);
                break;
            case PlayerState.Jump:
                animator.SetBool(HCJump, true);
                break;
        }
        currentState = newState;
    }

    private PlayerState DeterminateState()
    {
        if (mechanics.IsIdleBreaking())
            return PlayerState.IdleBreaker;
        else if (mechanics.IsJumping())
            return PlayerState.Jump;
        else if (mechanics.IsMoving())
            return PlayerState.Movement;
        else
            return PlayerState.Idle;
    }

   
}
