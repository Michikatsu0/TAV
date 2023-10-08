using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MechanicsController : MonoBehaviour
{
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Vector3 moveDirection;
    [SerializeField] private LayerMask layerGround;
    [SerializeField] float groundGravity, airGravity; 
    private float time, delay = 5f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");
    }

    public bool IsIdle()
    {
        return moveDirection.x == 0 && moveDirection.y == 0;
    }
    public bool IsMoving()
    {
        time = 0;
        return moveDirection.x != 0 || moveDirection.y != 0;
    }

    public bool IsGrounded() => characterController.isGrounded;

    public void Gravity()
    {
        if (IsFalling())
            moveDirection.y = groundGravity;
        else
            moveDirection.y = airGravity;
        
        characterController.Move(moveDirection * Time.deltaTime);
    }

    public bool IsJumping()
    {
        time = 0;
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool IsFalling()
    {
        time = 0;
        if (Physics.SphereCast(transform.position, characterController.radius, Vector3.down, out RaycastHit hit, layerGround))
            return false;
        else
            return true;
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
    private float animatorLayer1;

    public bool IsAiming()
    {
        return Input.GetMouseButtonDown(0) && IsIdle();
    }



}