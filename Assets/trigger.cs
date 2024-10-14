using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{

public bool cambio=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          GameObject[] finds = GameObject.FindGameObjectsWithTag("HumanoidEnemy");
        if (Input.GetKeyDown(KeyCode.O))
    {
        // Encuentra todos los objetos con el tag "HumanoidEnemy"
      
        
        foreach (GameObject find in finds)
        {
           find.GetComponent<ControllerHumanoids>().end=false;
        }
    }
    if(cambio==true)
    {
        foreach (GameObject find in finds)
        {
           find.GetComponent<ControllerHumanoids>().end=true;
        } 
    }
    }
}
