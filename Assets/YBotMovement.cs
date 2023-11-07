using Unity.VisualScripting;
using UnityEngine;

public class YBotMovement : MonoBehaviour
{
    public CharacterController characterController;
    public Transform player;
    public Collider triggerCollider;
    public float followSpeed = 3f;
    public float attackDistance = 0.7f;
    public float runSpeed = 6f;
    public float walkSpeed = 3f;
    [HideInInspector] public Vector3 gravityDirection;

    public float groundGravity, airGravity ;

    private float verticalVelocity = 0;
    [SerializeField]
    private Animator animator;

    private bool isFollowingPlayer = false;

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator reference not set in the inspector!");
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (isFollowingPlayer)
        {
            // Calculate the direction towards the player
            Vector3 direction = player.position - transform.position;
            direction.y = 0f; // Ensure the character stays at the same level as the ground

            // Move the character towards the player without changing its rotation
            characterController.Move(direction.normalized * followSpeed * Time.deltaTime);

            // Check the distance between character and player
            

            // Set Attack state if the distance is less than attackDistance
            if (distance < attackDistance)
            {
                // Set Attack state here
                // Your attack logic goes here
                Debug.Log("Attacking!");
                animator.SetBool("Attack", true);

                // Stop following the player
                isFollowingPlayer = false;
            }
        }

        if (distance > attackDistance)
        {
            // Set Attack state here
            // Your attack logic goes here
            Debug.Log("Attacking!");

            // Stop following the player
            isFollowingPlayer = true;
            animator.SetBool("Attack", false);
        }

        // Calculate movement speed in X and Z directions of the CharacterController
        float moveSpeedX = Mathf.Abs(characterController.velocity.x) / followSpeed;
        float moveSpeedZ = Mathf.Abs(characterController.velocity.z) / followSpeed;

        // Set Animator parameters (normalized speed values)
        animator.SetFloat("MoveX", moveSpeedX);
        animator.SetFloat("MoveZ", moveSpeedZ);
        Gravity();
    }

    void FixedUpdate()
    {
        if (isFollowingPlayer)
        {
            // Calculate direction to the player
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0f; // Ensure the character stays at the same level as the ground

            // Only rotate towards the player if the player is moving
            if (directionToPlayer.magnitude > 0.1f)
            {
                // Set character's rotation to face the player
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == triggerCollider)
        {
            // Start following the player when it enters the trigger collider
            isFollowingPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other == triggerCollider)
        {
            // Stop following the player when it exits the trigger collider
            isFollowingPlayer = false;
        }
    }

    public void Gravity()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -groundGravity;
        }
        else
        {
            verticalVelocity -= airGravity * Time.deltaTime;
        }

        gravityDirection.y = verticalVelocity;
        characterController.Move(gravityDirection * Time.deltaTime);
    }
}