using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Unity.VisualScripting;


[CreateAssetMenu(fileName = "HumanoidPlayer", menuName = "ScriptableObjects/HumanoidPlayer", order = 1)]
public class ScriptableobjectHumanoidPlayer : Types
{
  public float moveSpeed = 10f;
    float rotationSpeed = 10.0f;
  public float runSpeed=20f;
  private float currentspeed;
  private GameObject child;
    private Animator animator;

private GameObject pivote;
 public GameObject sliderPrefab;
       private GameObject sliderInstance;

    private GameObject canvas;
private Slider sliderComponent;

  

private SliderHealth sliderHealth;
public Vector3 offset= new Vector3(0,2f,0);
    public override void Inicialize(GameObject obj)
    {
        pivote=GameObject.Find("Pivote");
        currentHealth=maxhealth;
         child=obj.transform.GetChild(1).gameObject;
        animator=child.GetComponent<Animator>();
         canvas = GameObject.Find("Canvas");
        if (sliderPrefab != null && canvas != null)
        {
        
             sliderInstance = Instantiate(sliderPrefab, canvas.transform);
            
         
            sliderInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);  
            
           
            sliderComponent = sliderInstance.GetComponent<Slider>();
                 
          sliderHealth=sliderComponent.GetComponent<SliderHealth>();
                  sliderHealth.ChangeMaxvalue(maxhealth);
       
            if (sliderComponent != null)
            {
                sliderComponent.value = 0.5f;  
            }
             
        }
        
        
    }

    public override void ExecuteBehavior(GameObject obj, bool con)

    {
        if(pivote!=null)
        {
       

         GameObject[] finds = GameObject.FindGameObjectsWithTag("Pivote");
           foreach (GameObject find in finds)
        {
             
           find.transform.position=new Vector3(obj.transform.position.x,obj.transform.position.y+1.2f,obj.transform.position.z);
        }
        }
 
      Vector3 worldPosition = obj.transform.position + offset;
sliderHealth.ChangeValue(currentHealth);

if (sliderInstance != null)
{
    
    sliderInstance.transform.position = worldPosition;

    
      Vector3 directionToPlayer = (Camera.main.transform.position - obj.transform.position).normalized;
          
    sliderInstance.GetComponent<RectTransform>().localScale = new Vector3(0.02f, 0.01f, 0.03f);  
   
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
                 sliderInstance.transform.rotation = Quaternion.Slerp( sliderInstance.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);


}
           
        
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if(Input.GetKey(KeyCode.LeftShift))
        {
        currentspeed=runSpeed;
        
        }
        else{
              
                 currentspeed=moveSpeed;
               
        }
        
      move= new Vector3(horizontal, 0, vertical) * currentspeed * Time.deltaTime;
   obj.transform.Translate(move);
     
        
    
       if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || 
         Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.RightArrow) || 
         Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || 
         Input.GetKey(KeyCode.DownArrow)) && !Input.GetKey(KeyCode.LeftShift))
    {
      
        
       
        if (animator != null)
        {
            animator.SetBool("run", false);
            animator.SetBool("walk", true);
        }
        
    }
    else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || 
         Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.RightArrow) || 
         Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || 
         Input.GetKey(KeyCode.DownArrow)) && Input.GetKey(KeyCode.LeftShift))
    {
        if (animator != null)
        {
animator.SetBool("run", true);
            animator.SetBool("runstop", false);
 }
    }
  
    else if(!Input.anyKey)
    {
      
        if (animator != null)
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
            animator.SetBool("runstop", true);
        }
    }
}
    
}


  

