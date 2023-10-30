using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        IdleBreaker,
        Walk,
        Run,
        Jump,
        TurnLeft,   // Added TurnLeft state
        TurnRight   // Added TurnRight state
    }

    public Animator _characterAnimator;
    private PlayerState _currentState;

    void Start()
    {
        SetState(PlayerState.Idle);
    }

    void Update()
    {
        PlayerState newState = DeterminateState();
        if (newState != _currentState)
            SetState(newState);
    }

    private void SetState(PlayerState newState)
    {
        // Disable the current state
        DisableState(_currentState);

        // Enable the new state
        EnableState(newState);

        _currentState = newState;
    }

    private void DisableState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                _characterAnimator.SetBool("Idle", false);
                break;
            case PlayerState.IdleBreaker:
                _characterAnimator.SetBool("IdleBreaker", false);
                break;
            case PlayerState.Walk:
                _characterAnimator.SetBool("Walk", false);
                break;
            case PlayerState.Run:
                _characterAnimator.SetBool("Run", false);
                break;
            case PlayerState.Jump:
                _characterAnimator.SetBool("Jump", false);
                break;
            case PlayerState.TurnLeft: // Disable TurnLeft state
                _characterAnimator.SetBool("TurnLeft", false);
                break;
            case PlayerState.TurnRight: // Disable TurnRight state
                _characterAnimator.SetBool("TurnRight", false);
                break;
        }
    }

    private void EnableState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                _characterAnimator.SetBool("Idle", true);
                break;
            case PlayerState.IdleBreaker:
                _characterAnimator.SetBool("IdleBreaker", true);
                break;
            case PlayerState.Walk:
                _characterAnimator.SetBool("Walk", true);
                break;
            case PlayerState.Run:
                _characterAnimator.SetBool("Run", true);
                break;
            case PlayerState.Jump:
                _characterAnimator.SetBool("Jump", true);
                break;
            case PlayerState.TurnLeft: // Enable TurnLeft state
                _characterAnimator.SetBool("TurnLeft", true);
                break;
            case PlayerState.TurnRight: // Enable TurnRight state
                _characterAnimator.SetBool("TurnRight", true);
                break;
        }
    }

    private PlayerState DeterminateState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return PlayerState.Jump;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            return PlayerState.TurnLeft; // Change to TurnLeft state when A key is pressed
        }
        else if (Input.GetKey(KeyCode.D))
        {
            return PlayerState.TurnRight; // Change to TurnRight state when D key is pressed
        }
        else if (IsRunning())
        {
            return PlayerState.Run;
        }
        else if (IsWalking())
        {
            return PlayerState.Walk;
        }
        else
        {
            return PlayerState.Idle;
        }
    }

    private bool IsWalking()
    {
        return Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift);
    }

    private bool IsRunning()
    {
        return Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift);
    }
}
