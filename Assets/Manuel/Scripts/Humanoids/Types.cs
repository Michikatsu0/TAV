using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Types : ScriptableObject
{
 // Salud máxima
public float maxhealth=100f;

[HideInInspector]public float currentHealth;

    // Nuevo: Referencia al modelo 3D o Prefab del personaje
    public GameObject characterModelPrefab;  


    // Guardar la referencia al objeto instanciado
    protected GameObject instantiatedModel;
   [HideInInspector]  public Vector3 move;

    // Método para inicializar el personaje
     
 [HideInInspector] public bool endAttack=true;
    public abstract void ExecuteBehavior(GameObject obj, bool con); 
public abstract void Inicialize(GameObject obj);
    // Método para recibir daño
    public virtual void TakeDamage( GameObject obj)
    {  
       
    
        if (currentHealth <= 0)
        {
            
        obj.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Método para morir
  
}
