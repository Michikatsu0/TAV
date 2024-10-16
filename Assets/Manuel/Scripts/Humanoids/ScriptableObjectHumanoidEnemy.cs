using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;  
using System.Linq;
[CreateAssetMenu(fileName = "HumanoidEnemy", menuName = "ScriptableObjects/HumanoidEnemy", order = 2)]
public class ScriptableObjectHumanoidEnemy : Types
{
    private bool chasing;
    private bool chase;
 public Transform[] patrolPoints = new  Transform[4];
  [HideInInspector] public bool triggerColorchange = true; 
    private int currentPointIndex = 0;
    [HideInInspector] public bool triggerColorchangeStop = false;
    public float moveSpeed = 4f;
    public float attackspeed=5f;
    public float chaseDistance = 5f;
    public float attackRange = 10f;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;
    public float endAttackTime;
    private float endcurrentAttackTime;
    private Transform startposition;
    float rotationSpeed = 10.0f;
    private NavMeshAgent agent;
private bool stop=false;
    public LayerMask playerLayer;  
    private GameObject child;
    private Animator animator;
 
    private GameObject player;
    private Transform playerTransform;
    private Transform objtransform;
    private bool conchange;
private SliderColor sliderColor;
public Vector3 offset= new Vector3(0,2.5f,0);
   
    public GameObject sliderPrefab;
       private GameObject sliderInstance;
  
    private GameObject canvas;
    
private Slider sliderComponent;
private CanvasGroup sliderCanvasGroup;
private float temp;
private bool changeStartcourtine=true;
private bool changestarStoptcourtine=true;
private bool changeStoptcourtine=true;
private bool changeStoptcourtinestart=true;
private bool changeStartcontcourtine=true;
private bool changeStocontcourtine=true;
private bool changebackconcourtine=true;
private GameObject trigger2;

    public override void Inicialize(GameObject obj)
    {
        triggerColorchange = true; 
         patrolPoints[0] = GameObject.Find("Waypoint1").transform;
        patrolPoints[1] = GameObject.Find("Waypoint2").transform;
        patrolPoints[2] = GameObject.Find("Waypoint3").transform;
        patrolPoints[3] = GameObject.Find("Waypoint4").transform;
        currentHealth = maxhealth;
        child = obj.transform.GetChild(1).gameObject;
        objtransform = obj.transform;
        endcurrentAttackTime = endAttackTime;
        startposition = obj.transform;
        player = GameObject.FindGameObjectWithTag("Player");
        agent=obj.GetComponent<NavMeshAgent>();
        animator = child.GetComponent<Animator>();
  trigger2=GameObject.Find("Trigger(no borrar)");
 
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
              agent.SetDestination(patrolPoints[currentPointIndex].position);
        }
        if (sliderInstance != null)
    {
        // Añadir o buscar el CanvasGroup
        sliderCanvasGroup = sliderInstance.GetComponent<CanvasGroup>();
        if (sliderCanvasGroup == null)
        {
            sliderCanvasGroup = sliderInstance.AddComponent<CanvasGroup>();
        }
    }
    }
void OnEnable()
{
  
    triggerColorchange = true;
    triggerColorchangeStop = false;
    chasing=false;
    chase=true;
  changeStartcourtine=true;
 changestarStoptcourtine=true;
changeStoptcourtine=true;
changeStartcontcourtine=true;
 changeStoptcourtinestart=true;
 stop=false;
 changebackconcourtine=true;
 changeStocontcourtine=true;
 changeStocontcourtine=true;
    
}
    public override void ExecuteBehavior(GameObject obj, bool con)

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
        

        if(con==false)
        {
        Rigidbody rb = obj.GetComponent<Rigidbody>();

                 agent.isStopped=true;
         

            
            
                float distanceToPlayer =  Vector3.Distance(obj.transform.position, player.transform.position);
              
                    if(distanceToPlayer> chaseDistance)
                    {
                        if(sliderColor.end==true)
                        {
                        if( changebackconcourtine)
                    {
sliderColor.getChangeColorStop();
changebackconcourtine=false;
 stop=true;
                    }
                   
                        }
                        if(stop==false)
              {
                
                        if(triggerColorchangeStop==false)
                        {
                        
Debug.Log("!");
               
                Vector3 directionToPlayer = (player.transform.position - obj.transform.position).normalized;
          
            
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

           
                obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

              
                Vector3 direction = (player.transform.position - obj.transform.position).normalized;
                animator.SetBool("Chase", true);
                obj.transform.Translate(direction * moveSpeed * Time.deltaTime);
                        }
                        else{
                           if(sliderColor.triggerColorChangeStop==true)
                           {
                            changeStartcourtine=true;
                            changeStoptcourtine=true;
                            changeStartcontcourtine=true;
                          
                            Debug.Log("2");
                             sliderColor.triggerColorChange=true;
                              Vector3 directionToPlayer = (player.transform.position - obj.transform.position).normalized;
                          
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

             
                obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            
                Vector3 direction = (player.transform.position - obj.transform.position).normalized;
               animator.SetBool("Run", true);
                obj.transform.Translate(direction * attackspeed * Time.deltaTime);
             
                  
                           if(changestarStoptcourtine)
                           {
                            sliderColor.getChangeColorStar();
                            changestarStoptcourtine=false;
                           }
                           }
                           else
                           {
                      Debug.Log("3");
                           
                             chasing=false;
                          
                          trigger2.GetComponent<Pivotetrigger>().change=true;
                          trigger2.GetComponent<Pivotetrigger>().cambio=true;
                            triggerColorchangeStop=false;
                           
                           }
                            
                        }
            }
            else{
 if(sliderColor.triggerColorChangeStop==true)
                           {
                            changeStartcourtine=true;
                            changeStoptcourtine=true;
                            changeStartcontcourtine=true;
                          
                            Debug.Log("4");
                             sliderColor.triggerColorChange=true;
             animator.SetBool("Run", false);
              animator.SetBool("Chase", false);
                  Vector3 directionToPlayer = (player.transform.position - obj.transform.position).normalized;
          
                // Crear la rotación basada en la dirección (solo rotar en el eje Y)
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

                // Aplicar la rotación suavemente (opcional)
                obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                   
                           if(changeStocontcourtine)
                           {
                            sliderColor.getChangeColorStarcon();
                           changeStocontcourtine=false;
                           }
                           }
                           else
                           {
                      Debug.Log("5");
                           
                             chasing=false;
                         trigger2.GetComponent<Pivotetrigger>().change=true;
                          trigger2.GetComponent<Pivotetrigger>().cambio=true;

                            triggerColorchangeStop=false;
                            
                           
                           }
                            
                        }
            
            
                    }

              else
            {
                
                if(sliderColor.end2==true)
                {
                     if(changeStoptcourtine)
                    {
sliderColor.getChangeColorbackStop();
changeStoptcourtine=false;
 chasing=true;
                    }
                   
                }
             
                if(chasing==false)
                {
                   
                       
                
                if(sliderColor.triggerColorChange==true)
                            {
                              changestarStoptcourtine=true;
             changestarStoptcourtine=true;
                   changeStocontcourtine=true;
                     sliderColor.triggerColorChangeStop=true;
                    
                           Debug.Log("6");     
             animator.SetBool("Chase", false);
                 animator.SetBool("Run", false);
                 agent.isStopped=true;
             
if(changeStartcourtine)
{
                 sliderColor.getChangeColorStopStart();
                 stop=false;
                 changeStartcourtine=false;
}


                            }
                            else{
triggerColorchangeStop=true;
         Debug.Log("7");
                 animator.SetBool("Run", true);
                  Vector3 directionToPlayer = (player.transform.position - obj.transform.position).normalized;
          
                // Crear la rotación basada en la dirección (solo rotar en el eje Y)
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

                // Aplicar la rotación suavemente (opcional)
                obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                 Vector3 direction = (player.transform.position - obj.transform.position).normalized;
                obj.transform.Translate(direction * attackspeed * Time.deltaTime);
                            }
            
            }
            else{
                
                      
                
                if(sliderColor.triggerColorChange==true)
                {
                     triggerColorchangeStop=true;
                   changestarStoptcourtine=true;
                    changeStocontcourtine=true;
                     sliderColor.triggerColorChangeStop=true;
                           Debug.Log("8"); 
                        
               Vector3 directionToPlayer = (player.transform.position - obj.transform.position).normalized;
          
                // Crear la rotación basada en la dirección (solo rotar en el eje Y)
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

                // Aplicar la rotación suavemente (opcional)
                obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                 Vector3 direction = (player.transform.position - obj.transform.position).normalized;
                obj.transform.Translate(direction * attackspeed  * Time.deltaTime);
                 animator.SetBool("Run", true);
                 agent.isStopped=true;
               if(changeStartcontcourtine)
               {
 sliderColor.getChangeColorbackcon();
 changeStartcontcourtine=false;
               }
            }
            else{
 Debug.Log("9");  
   triggerColorchangeStop=true;
                 animator.SetBool("Run", true);
                  Vector3 directionToPlayer = (player.transform.position - obj.transform.position).normalized;
          
                // Crear la rotación basada en la dirección (solo rotar en el eje Y)
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

                // Aplicar la rotación suavemente (opcional)
                obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                 Vector3 direction = (player.transform.position - obj.transform.position).normalized;
                obj.transform.Translate(direction * attackspeed *2 * Time.deltaTime);
            }
            }
            }
          
        
    }
    else{
          Debug.Log("10");
Patrol();

    }
    } 
    

  private void Patrol()
    {
         agent.isStopped=false;
     animator.SetBool("Chase", true);
     

    if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
    {
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length; 
        agent.isStopped = false;
        agent.SetDestination(patrolPoints[currentPointIndex].position); 
    }

 
    if (agent.velocity.sqrMagnitude > 0)
    {
        animator.SetBool("Chase", true);
    }
    else
    {
        animator.SetBool("Chase", false);
    }

    }

    }

