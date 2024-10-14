using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class ControllerHumanoids : MonoBehaviour
{
    // Referencia al ScriptableObject que define el comportamiento
  private NavMeshAgent navAgent;
private Animator animator;
private GameObject child;
 public float damageAmount = 100;
    private float wanderDistance = 3;
private  GameObject visuals;
    public Types characterBehavior;

    [HideInInspector]public bool end=true;
private bool change=false;
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
       
        child=gameObject.transform.GetChild(0).gameObject;
        animator=child.GetComponent<Animator>();
        
    }

    private void LoadEnemy(Types _characterBehavior)
    {
        //remove children objects i.e. visuals
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
            // Ejecuta el comportamiento del personaje en cada frame
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
        characterBehavior.currentHealth-=10f;
    characterBehavior.TakeDamage(gameObject);
    Debug.Log(characterBehavior.currentHealth);
    animator.SetBool("run",false);
    animator.SetBool("walk",false);
    animator.SetTrigger("Punch");
  
   Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            // Calcula la dirección del empuje, que es desde este objeto hacia el objeto tocado
            Vector3 pushDirection = transform.position-col.transform.position  ;
            pushDirection = pushDirection.normalized; // Normaliza para obtener solo la dirección

            // Aplica una fuerza en esa dirección con la magnitud especificada
            rb.AddForce(pushDirection * 7f, ForceMode.Impulse);
        }
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
