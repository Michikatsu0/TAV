using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamera : MonoBehaviour
{
  
      public TypesCamera characterBehavior;
     private  GameObject visuals;

   private void Start()
    {


      

        if (characterBehavior != null)
        {
            LoadEnemy(characterBehavior);
        }
            
 if (characterBehavior != null)
        {
           
            characterBehavior.Inicialize(gameObject);
        }
       
        
        
    }

    private void LoadEnemy(TypesCamera _characterBehavior)
    {
     
        foreach (Transform child in this.transform)
        {
            if (Application.isEditor)
                DestroyImmediate(child.gameObject);
            else
                Destroy(child.gameObject);
        }

       
        visuals = Instantiate(_characterBehavior.prefab);
        visuals.transform.SetParent(this.transform);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = Quaternion.identity;
    
   
    }
     private void Update()
    {
        

        if (characterBehavior != null)
        {
            // Ejecuta el comportamiento del personaje en cada frame
            characterBehavior.ExecuteBehavior(gameObject);
         
        }
  
       
}
}
