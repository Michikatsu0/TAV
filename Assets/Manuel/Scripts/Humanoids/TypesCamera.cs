using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TypesCamera : ScriptableObject
{
    public GameObject prefab;
    // Start is called before the first frame update
       public abstract void ExecuteBehavior(GameObject obj); 
public abstract void Inicialize(GameObject obj);
}
