using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Camera1", menuName = "ScriptableObjects/Camera1", order = 3)]
public class ScriptableCAmera1 : TypesCamera
{
    private bool chase=false;
    private bool stop=false;
      public GameObject sliderPrefab;
       private GameObject sliderInstance;
 
    private GameObject canvas;
    public float chaseDistance=7;
    private Slider sliderComponent;
    
private CanvasGroup sliderCanvasGroup;
private SliderColor sliderColor;
public Vector3 offset= new Vector3(0,1f,0);

    float rotationSpeed = 10.0f;
private GameObject player;

private bool start;
private bool startcon;

private bool back;
private bool backcon;
private bool starstop;
private bool backstop;
private bool trigger;


    public override void Inicialize(GameObject obj)
    {
player=GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.Find("Canvas");  

     
        if (sliderPrefab != null && canvas != null)
        {
        
             sliderInstance = Instantiate(sliderPrefab, canvas.transform);
            
        
            sliderInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);  
            
          
            sliderComponent = sliderInstance.GetComponent<Slider>();
                 sliderColor=sliderComponent.GetComponent<SliderColor>();

          
                  
       
            if (sliderComponent != null)
            {
                sliderComponent.value = 0.5f; 
            }
      
        }
        if (sliderInstance != null)
    {
      
        sliderCanvasGroup = sliderInstance.GetComponent<CanvasGroup>();
        if (sliderCanvasGroup == null)
        {
            sliderCanvasGroup = sliderInstance.AddComponent<CanvasGroup>();
        }
    }
    }
    void OnEnable()
{
     start=true;
  startcon=true;

 back=true;
 backcon=true;
  starstop=true;
  backstop=true;
chase=false;
    stop=false;
    trigger=false;

}
        public override void ExecuteBehavior(GameObject obj)
    {
         Vector3 worldPosition = obj.transform.position + offset;


if (sliderInstance != null)
{
 
    sliderInstance.transform.position = worldPosition;

  
    sliderInstance.GetComponent<RectTransform>().localScale = new Vector3(0.02f, 0.01f, 0.1f);  

   
    float distanceToCamera = Vector3.Distance(Camera.main.transform.position, worldPosition);

    if (distanceToCamera > 0)  
    {
        sliderCanvasGroup.alpha = 1f; 
    }
    else
    {
        sliderCanvasGroup.alpha = 0f; 
    }
}
if (sliderInstance != null)
{
   
  

      Vector3 directionToCameera= (Camera.main.transform.position - obj.transform.position).normalized;
          

    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToCameera.x, 0, directionToCameera.z));
                 sliderInstance.transform.rotation = Quaternion.Slerp( sliderInstance.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);


 
}
 float distanceToPlayer =  Vector3.Distance(obj.transform.position, player.transform.position);
               
               if(distanceToPlayer> chaseDistance)
               {
                if(trigger==true)
                { sliderColor.triggerColorChange=true;
                back=true;
                    backstop=true;
                    backcon=true;
                if(sliderColor.end2)
                {
                    if(starstop==true)
                    {
                     sliderColor.getChangeColorStop();
                    stop=true;
                    starstop=false;
                    }
                }
                if(stop==false)
                {
                    
                  
                    Debug.Log("1");
                    if(start==true)
                    {
                    sliderColor.getChangeColorStar();
                    start=false;
                   }
                
    
               
              
    
    
}
else{
    if(startcon)
    {
       sliderColor.getChangeColorStarcon();
       startcon=false;
    }
}
                }
               }
else 
    {
       
                    starstop=true;
                    start=true;
                    startcon=true;
                    trigger=true;
                    
     if(sliderColor.end2)
                {
                    
                    if(backstop)
                    {
                    sliderColor.getChangeColorStop();
                      
                    chase=true;
                    backstop=false;
                    }
                }
                if(chase==false)
                {
                    if(sliderColor.triggerColorChange)
                   {
                    sliderColor.triggerColorChange=true;
                    starstop=true;
                    start=true;
                    startcon=true;
                    if(back)
                    {
                         sliderColor.getChangeColorbackcon();
                   
                    back=false;
                    
                }
                }
                 else{
                  Debug.Log("2");
                   
          GameObject pivote=GameObject.Find("Trigger(no borrar)");
        pivote.GetComponent<Pivotetrigger>().change=true;
        pivote.GetComponent<Pivotetrigger>().cambio=false;
        pivote.GetComponent<Pivotetrigger>().cambio2=true;
                             
               }
                }
                else{
                 Debug.Log("4");   
                  if(sliderColor.triggerColorChange)
                  {
if(backcon)
{
sliderColor.getChangeColorbackcon();
backcon=false;
}
                }
                else{
                  Debug.Log("2");
                    if(startcon==true)
                    {
                        sliderColor.getChangeColorStarcon();
                    
                    startcon=false;
                    }
                   
                   else{
       GameObject pivote=GameObject.Find("Trigger(no borrar)");
        pivote.GetComponent<Pivotetrigger>().change=true;
        pivote.GetComponent<Pivotetrigger>().cambio=false;
        pivote.GetComponent<Pivotetrigger>().cambio2=true;
         
    }
               }
    }
    }
}
}