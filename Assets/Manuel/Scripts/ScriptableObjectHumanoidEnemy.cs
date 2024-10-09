using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HumanoidEnemy", menuName = "ScriptableObjects/HumanoidEnemy", order = 2)]
public class ScriptableObjectHumanoidEnemy : Types
{
   public float moveSpeed = 3f;
    public float chaseDistance = 10f;
    public float attackRange = 2f;
    public float damageAmount = 10f;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;
    public float endAttackTime;
    private float endcurrentAttackTime;

    public bool endAttack;
        public LayerMask playerLayer;  
private GameObject child;
       private Animator animator;

    private Transform playerTransform;
    private Transform objtransform;
    public override void Inicialize(GameObject obj)
    {
         child=obj.transform.GetChild(0).gameObject;
        objtransform=obj.transform;
        endcurrentAttackTime=endAttackTime;
    }

    public override void ExecuteBehavior(GameObject obj)
    {
       
        animator=child.GetComponent<Animator>();
        
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }

        if(endAttack==false)
        {
        endcurrentAttackTime=-Time.deltaTime;
        if(endAttackTime<=0)
        {
            endAttack=true;
            endcurrentAttackTime=endAttackTime;
        }
        if (playerTransform != null)
        {

            float distanceToPlayer = Vector3.Distance(obj.transform.position, playerTransform.position);

             // Moverse hacia el jugador

                Vector3 direction = (playerTransform.position - obj.transform.position).normalized;
                animator.SetBool("Chase",true);
                obj.transform.Translate(direction * moveSpeed * Time.deltaTime);
              

                // Verificar si está en rango para atacar
                if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
                {
                    AttackPlayer();
                    nextAttackTime = Time.time + attackCooldown; 
                }
            }
        }
    }
    

    private void AttackPlayer()
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(objtransform.position, attackRange, playerLayer);
        foreach (var hitCollider in hitColliders)
        {
            GameObject player = hitCollider.gameObject;
            
            // Supongamos que el jugador tiene un `CharacterBehavior` asignado directamente
            Types playerBehavior = player.GetComponent<Types>();

            if (playerBehavior != null)
            {
                // Inflige daño al jugador
                playerBehavior.TakeDamage(player, damageAmount);
                Debug.Log("El enemigo ha atacado al jugador.");
            }
        
}
    }
}
