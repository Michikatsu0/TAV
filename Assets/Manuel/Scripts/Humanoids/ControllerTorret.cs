using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTorret : MonoBehaviour
{
  
     public TypesTurret characterBehavior;
     private  GameObject visuals;
    [HideInInspector] public bool con=false;
 
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

    private void LoadEnemy(TypesTurret _characterBehavior)
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
         
            characterBehavior.ExecuteBehavior(gameObject);
         
        }
  
       
}
}
