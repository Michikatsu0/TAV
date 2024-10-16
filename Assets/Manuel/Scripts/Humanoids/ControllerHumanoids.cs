using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class ControllerHumanoids : MonoBehaviour
{
   
  private NavMeshAgent navAgent;
private Animator animator;
private GameObject child;
 public float damageAmountEnemy = 10;
 public float damageAmountturret = 10;
 public bool changetrigger=false;
    private float wanderDistance = 3;
private  GameObject visuals;
    public Types characterBehavior;
public bool trigercon=false;

    [HideInInspector]public bool end=true;
private bool change=false;

public bool triggerd1;

   private void Start()
    {


      

        if (characterBehavior != null)
        {
            LoadEnemy(characterBehavior);
        }
            
 if (characterBehavior != null)
        {
            // Inicializa el comportamiento del personaje
            characterBehavior.Inicialize(gameObject);
        }
       
        child=gameObject.transform.GetChild(1).gameObject;
        animator=child.GetComponent<Animator>();
        
    }

    private void LoadEnemy(Types _characterBehavior)
    {
        
        foreach (Transform child in this.transform)
        {
            if (Application.isEditor)
                DestroyImmediate(child.gameObject);
            else
                Destroy(child.gameObject);
        }

        //load current enemy visuals
        visuals = Instantiate(_characterBehavior.characterModelPrefab);
        visuals.transform.SetParent(this.transform);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = Quaternion.identity;
    
   
    }

 
      


           
            

    

    private void Update()
    {
       
     
        if (characterBehavior != null)
        {
          
            characterBehavior.ExecuteBehavior(gameObject, end);
         
        }
  
       
}
private void OnTriggerEnter(Collider col)
{
    if(change==false)
    {
    if(gameObject.CompareTag("Player"))
    {
    if(col.gameObject.CompareTag("HumanoidEnemy"))
    {
        characterBehavior.currentHealth-=damageAmountEnemy;
    characterBehavior.TakeDamage(gameObject);
  
    animator.SetBool("run",false);
    animator.SetBool("walk",false);
    animator.SetTrigger("Punch");
  
   Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        
        if (rb != null)
        {
           
            Vector3 pushDirection = transform.position-col.transform.position  ;
            pushDirection = pushDirection.normalized; 

           
            rb.AddForce(pushDirection * 7f, ForceMode.Impulse);
        }
    }
    if(col.gameObject.CompareTag("bullet"))
    {
        Destroy(col.gameObject);
 characterBehavior.currentHealth-=damageAmountEnemy;
    characterBehavior.TakeDamage(gameObject);
  
    animator.SetBool("run",false);
    animator.SetBool("walk",false);
    animator.SetTrigger("Punch");
    }
    }
    change=true;
}
}
public void OnTriggerExit(Collider col)
{
    change=false;
}
}
