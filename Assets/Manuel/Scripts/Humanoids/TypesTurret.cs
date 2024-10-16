using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TypesTurret : ScriptableObject
{
    // Start is called before the first frame update
   public GameObject prefab;

   public bool triggerdend;
    // Start is called before the first frame update
       public abstract void ExecuteBehavior(GameObject obj); 
public abstract void Inicialize(GameObject obj);
}
