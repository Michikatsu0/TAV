using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MechanicsController : MonoBehaviour
{
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Vector3 moveDirection, gravityDirection;
    [SerializeField] private LayerMask isGrounded;
    [SerializeField] public float centerDistance, groundGravity, airGravity; 
    private float time, delay = 5f;
    Vector3 centerCharacter;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.z = Input.GetAxis("Vertical");
    }

    public bool IsIdle()
    {
        return moveDirection.x == 0 && moveDirection.z == 0;
    }
    public bool IsMoving()
    {
        time = 0;
        return moveDirection.x != 0 || moveDirection.z != 0;
    }

    public void Gravity()
    {
        if (IsFalling())
            gravityDirection.y = -airGravity;
        else
            gravityDirection.y = -groundGravity;
        
        //characterController.Move(gravityDirection * Time.deltaTime);
    }

    public bool IsJumping()
    {
        time = 0;
        return Input.GetKey(KeyCode.Space) && !IsFalling();
    }

    public bool IsFalling()
    {
        time = 0;

        centerCharacter = transform.position + new Vector3(0, characterController.height / 2, 0);

        if (Physics.SphereCast(centerCharacter, characterController.radius, Vector3.down, out RaycastHit hit, centerDistance, isGrounded))
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
        var characterController = GetComponent<CharacterController>();
        center = transform.position - new Vector3(0, centerDistance - (characterController.height / 2), 0);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, characterController.radius);
    }
}
