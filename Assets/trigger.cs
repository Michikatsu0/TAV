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
      
    if(cambio==true)
    {
        foreach (GameObject find in finds)
        {
            if(find.GetComponent<ControllerHumanoids>().changetrigger==true)
            {
         find.GetComponent<ControllerHumanoids>().end=true;
         find.GetComponent<ControllerHumanoids>().changetrigger=false;
            }
           
        } 
        cambio=false;
    }
    }
}
