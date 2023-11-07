using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

public class AIEnemyRobotHumanoid : MonoBehaviour
{
    [SerializeField] private HumanoidAttack humanoidAttack;
    [SerializeField] private float attackDistance, angularSpeed, damage;
    private Transform playerPosition;
    private Animator animator;
    private NavMeshAgent agent;
    private HealthEnemyResponse healthResponse;

    private float currentDistance;
    private int HCSpeed = Animator.StringToHash("Speed");
    private int HCAttack = Animator.StringToHash("Attack");
    private int HCHit = Animator.StringToHash("Hit");

    
    void Start()
    {
        playerPosition = GameObject.Find("PlayerArmature").transform;
        healthResponse = GetComponent<HealthEnemyResponse>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        humanoidAttack.attackDistance = attackDistance;
        humanoidAttack.angularSpeed = angularSpeed;
        humanoidAttack.damage = damage;
    }

    void FixedUpdate()
    {
        if (healthResponse.deathScript)
        {
            agent.isStopped = true;
            return;
        }
        else
            agent.SetDestination(playerPosition.position);

        animator.SetBool(HCHit, healthResponse.onHit);
        animator.SetFloat(HCSpeed, agent.velocity.magnitude);

        currentDistance = Vector3.Distance(playerPosition.position, agent.transform.position);

        if (!humanoidAttack.stopAttack && currentDistance < humanoidAttack.attackDistance)
        {
            animator.SetBool(HCAttack, true);
            agent.isStopped = true;
            LookTarget();
        }
        else
        {
            animator.SetBool(HCAttack, false);
            agent.isStopped = false;
        }
    }

    private void Attack(AnimationEvent animationEvent)
    {
        humanoidAttack.hitBox.enabled = animationEvent.intParameter == 1 ? true : false;
    }

    private void LookTarget()
    {
        humanoidAttack.attackDirection = playerPosition.position - agent.transform.position;
        humanoidAttack.attackDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(humanoidAttack.attackDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * humanoidAttack.angularSpeed);
    }
}
