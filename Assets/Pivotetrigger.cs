using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pivotetrigger : MonoBehaviour
{
    public bool cambio=false;
    public bool cambio2=false;
    public bool change;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(change==true)
        {
            
        Debug.Log("hola");
         GameObject[] finds = GameObject.FindGameObjectsWithTag("HumanoidEnemy");
           GameObject[] finds2 = GameObject.FindGameObjectsWithTag("turret");
        if(cambio==true)
    {
        foreach (GameObject find in finds)
        {
             Debug.Log("hola2");
            
                Debug.Log(find.GetComponent<ControllerHumanoids>().end);
         find.GetComponent<ControllerHumanoids>().end=true;
        }
    }
        else{
            foreach (GameObject find in finds)
        {
             Debug.Log("hola2");
            
                Debug.Log(find.GetComponent<ControllerHumanoids>().end);
         find.GetComponent<ControllerHumanoids>().end=false;
         
        }
        }
        if(cambio2==true)
        {
        
       
                foreach (GameObject find in finds2)
        {
             Debug.Log("hola2");
            
                
         find.GetComponent<ControllerTorret>().con=true;
         
        }
        }
    
            else{

             foreach (GameObject find in finds2)
        {
             Debug.Log("hola2");
            
              
         find.GetComponent<ControllerTorret>().con=false;
         
        }
            }
            
               
         change=false;
       
    }
    }
}
        


