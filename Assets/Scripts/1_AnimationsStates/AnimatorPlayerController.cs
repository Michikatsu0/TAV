using UnityEngine;

public class AnimatorPlayerController : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        Movement,
        IdleBreaker,
        Jump,
        Crouch
    }

    [HideInInspector] public Animator animator;
    private PlayerState currentState;
    private MechanicsController mechanics;
    private PlayerIKMechanicsResponse iKMechanics;
    private PlayerHealthResponse healthResponse;
    private int HCMoveZ = Animator.StringToHash("MoveZ");
    private int HCMoveX = Animator.StringToHash("MoveX");

    private int HCIdle = Animator.StringToHash("IsIdle");
    private int HCMovement = Animator.StringToHash("IsMoving");
    private int HCIdleBreaker = Animator.StringToHash("IsIdleBreaking");
    private int HCJump = Animator.StringToHash("IsJumping");
    private int HCFall= Animator.StringToHash("IsFalling");
    private int HCCrouch = Animator.StringToHash("IsCrouching");
    private int HCHit = Animator.StringToHash("Hit");
    [HideInInspector] public int HCIsZooming = Animator.StringToHash("IsZooming");
    [HideInInspector] public int HCIsAiming = Animator.StringToHash("IsAiming");
    private Animator cMAnimator;
    int clicks = 0;

    private void Start()
    {
        cMAnimator = GameObject.Find("CM_VC_Player").GetComponent<Animator>();
        animator = GetComponent<Animator>();
        healthResponse = GetComponent<PlayerHealthResponse>();
        mechanics = GetComponent<MechanicsController>();
        iKMechanics = GetComponent<PlayerIKMechanicsResponse>();
        SetState(PlayerState.Movement);
    }

    private void Update()
    {
        if (healthResponse.deathScript) return;

        mechanics.IsGrounded();

        animator.SetBool(HCFall, mechanics.IsGrounded());

        if (mechanics.IsAiming())
        {
            mechanics.isAiming = true;
            
            clicks++;
            if (clicks >= 2)
            {
                mechanics.isAiming = false;
                clicks = 0;
            }
        }
        
        animator.SetBool(HCIsAiming, mechanics.isAiming);
        cMAnimator.SetBool(HCIsZooming, mechanics.isAiming);
        animator.SetBool(HCCrouch, mechanics.IsCrouching());
        animator.SetBool(HCHit, healthResponse.onHit);

        if (mechanics.isAiming)
        {
            iKMechanics.TriggerWeapon(mechanics.IsFiring());
            mechanics.animLayer1 = Mathf.Lerp(animator.GetLayerWeight(1), 1, mechanics.animLayerSmooth1 * Time.deltaTime);
        }
        else
        {
            iKMechanics.TriggerWeapon(false);
            mechanics.animLayer1 = Mathf.Lerp(animator.GetLayerWeight(1), 0, mechanics.animLayerSmooth1 * Time.deltaTime);
        }

        animator.SetLayerWeight(1, mechanics.animLayer1);
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool(HCIsAiming, false);
            cMAnimator.SetBool(HCIsZooming, false);
            iKMechanics.TriggerWeapon(false);
            animator.SetFloat(HCMoveX, mechanics.moveDirection.x);
            animator.SetFloat(HCMoveZ, mechanics.moveDirection.z);
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
