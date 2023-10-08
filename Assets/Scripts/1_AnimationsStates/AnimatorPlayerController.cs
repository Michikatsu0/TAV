using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.MeshOperations;
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

    private int HCMoveZ = Animator.StringToHash("MoveZ");
    private int HCMoveX = Animator.StringToHash("MoveX");

    private int HCMovement = Animator.StringToHash("IsMoving");
    private int HCIdleBreaker = Animator.StringToHash("IsIdleBreaking");
    private int HCJump = Animator.StringToHash("IsJumping");
    private int HCIsGrounded = Animator.StringToHash("IsFalling");

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var characterController = GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(transform.position, characterController.radius);
    }
}
