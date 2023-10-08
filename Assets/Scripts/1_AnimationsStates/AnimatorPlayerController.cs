using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.MeshOperations;
[RequireComponent(typeof(MechanicsController))]
public class AnimatorPlayerController : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        Movement,
        IdleBreaker,
        Jump,
        IsFalling
    }

    [HideInInspector] public Animator animator;
    private PlayerState currentState;
    private MechanicsController mechanics;

    private string HCMoveZ = "MoveZ";
    private string HCMoveX = "MoveX";

    private string HCIdle = "IsIdle";
    private string HCMovement = "IsMoving";
    private string HCIdleBreaker = "IsIdleBreaking";
    private string HCJump = "IsJumping";
    private string HCIsGrounded = "IsFalling";


    private void Start()
    {
        animator = GetComponent<Animator>();
        mechanics = GetComponent<MechanicsController>();
        SetState(PlayerState.Movement);
    }

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat(HCMoveX, mechanics.moveDirection.x);
            animator.SetFloat(HCMoveZ, mechanics.moveDirection.y);
            if (mechanics.moveDirection.x != 0 && mechanics.moveDirection.y == 0)
            {
                animator.SetFloat(HCMoveX, mechanics.moveDirection.x / 2);
                animator.SetFloat(HCMoveZ, mechanics.moveDirection.y / 2);
            }
        }
        else
        {
            animator.SetFloat(HCMoveX, mechanics.moveDirection.x / 2);
            animator.SetFloat(HCMoveZ, mechanics.moveDirection.y / 2);
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
            case PlayerState.IsFalling:
                animator.SetBool(HCIsGrounded, false);
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
        else if (mechanics.IsMoving())
            return PlayerState.Movement;
        else
            return PlayerState.Idle;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var characterController = GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(transform.position, characterController.radius);
    }
}
