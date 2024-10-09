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
    private float wanderDistance = 3;
private  GameObject visuals;
    public Types characterBehavior;

   private void Start()
    {
        if (navAgent == null)
            navAgent = this.GetComponent<NavMeshAgent>();

        if (characterBehavior != null)
            LoadEnemy(characterBehavior);
            

        if (characterBehavior != null)
        {
            // Inicializa el comportamiento del personaje
            characterBehavior.Inicialize(gameObject);
        }
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
        
animator=visuals.GetComponent<Animator>();
        if (characterBehavior != null)
        {
            // Ejecuta el comportamiento del personaje en cada frame
            characterBehavior.ExecuteBehavior(gameObject);
        }
   
        if (characterBehavior.move.magnitude > 0 && characterBehavior.move.magnitude<=5f)
        {

            // Activar el trigger de movimiento en el Animator
            if (animator != null)
            {
                animator.SetBool("Walk",true);
                animator.SetBool("Run",false);
            }
        }
        else if(characterBehavior.move.magnitude==0)
        {
            // Desactivar el trigger si no se está moviendo (opcional)
            if (animator != null)
            {
                animator.SetBool("Walk",false);
                animator.SetBool("RunStop", true);
        }

        // Aquí no hay ataque, solo movimiento
    }
    else if(characterBehavior.move.magnitude>5f)
    {
          animator.SetBool("Run",true);
          animator.SetBool("RunStop",false);
    }
    }

    // Método para recibir daño
    public void TakeDamage(float damage)
    {
        if (characterBehavior != null)
        {
            characterBehavior.TakeDamage(gameObject, damage);
        }
    }
}
