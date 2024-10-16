using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Types : ScriptableObject
{
 // Salud máxima
public float maxhealth=100f;

[HideInInspector]public float currentHealth;
[HideInInspector]public bool triggerend=false;

    public GameObject characterModelPrefab;  


 
    protected GameObject instantiatedModel;
   [HideInInspector]  public Vector3 move;

   
     
 [HideInInspector] public bool endAttack=true;
    public abstract void ExecuteBehavior(GameObject obj, bool con); 
public abstract void Inicialize(GameObject obj);

    public virtual void TakeDamage( GameObject obj)
    {  
       
    
        if (currentHealth <= 0)
        {
            
        obj.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

 
  
}
