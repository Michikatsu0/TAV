using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;  // Cambié a UnityEngine.UI ya que el sistema de UI clásico de Unity usa esto
using System.Linq;
[CreateAssetMenu(fileName = "HumanoidEnemy", menuName = "ScriptableObjects/HumanoidEnemy", order = 2)]
public class ScriptableObjectHumanoidEnemy : Types
{
    private bool chasing;
    private bool chase;
 public Transform[] patrolPoints = new  Transform[4]; // Array de puntos de patrullaje
  [HideInInspector] public bool triggerColorchange = true; 
    private int currentPointIndex = 0;
    [HideInInspector] public bool triggerColorchangeStop = false;
    public float moveSpeed = 4f;
    public float attackspeed=7f;
    public float chaseDistance = 10f;
    public float attackRange = 10f;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;
    public float endAttackTime;
    private float endcurrentAttackTime;
    private Transform startposition;
    float rotationSpeed = 5.0f;
    private NavMeshAgent agent;

    public LayerMask playerLayer;  
    private GameObject child;
    private Animator animator;
 
    private GameObject player;
    private Transform playerTransform;
    private Transform objtransform;
    private bool conchange;
private SliderColor sliderColor;
public Vector3 offset= new Vector3(0,2.5f,0);
    // Referencia al prefab del slider
    public GameObject sliderPrefab;
       private GameObject sliderInstance;
    // Referencia al canvas donde se colocará el slider
    private GameObject canvas;
    private trigger trigger;
private Slider sliderComponent;
private CanvasGroup sliderCanvasGroup;
private float temp;
private bool changeStartcourtine=true;
private bool changestarStoptcourtine=true;
private bool changeStoptcourtine=true;
private bool changeStartcontcourtine=true;

    public override void Inicialize(GameObject obj)
    {
        triggerColorchange = true; 
         patrolPoints[0] = GameObject.Find("Waypoint1").transform;
        patrolPoints[1] = GameObject.Find("Waypoint2").transform;
        patrolPoints[2] = GameObject.Find("Waypoint3").transform;
        patrolPoints[3] = GameObject.Find("Waypoint4").transform;
        currentHealth = maxhealth;
        child = obj.transform.GetChild(0).gameObject;
        objtransform = obj.transform;
        endcurrentAttackTime = endAttackTime;
        startposition = obj.transform;
        player = GameObject.FindGameObjectWithTag("Player");
        agent=obj.GetComponent<NavMeshAgent>();
        animator = child.GetComponent<Animator>();
     trigger=GameObject.Find("Cube").GetComponent<trigger>();
        // Buscar el Canvas en la escena
        canvas = GameObject.Find("Canvas");  // Asegúrate que tu Canvas esté nombrado correctamente

        // Crear el slider a partir del prefab
        if (sliderPrefab != null && canvas != null)
        {
            // Instanciar el slider como hijo del Canvas
             sliderInstance = Instantiate(sliderPrefab, canvas.transform);
            
            // Configurar la posición del slider (puedes ajustar esto según tus necesidades)
            sliderInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);  // Por ejemplo, centrado en el Canvas
            
            // Opcional: Acceder y modificar componentes del slider si es necesario
            sliderComponent = sliderInstance.GetComponent<Slider>();
                 sliderColor=sliderComponent.GetComponent<SliderColor>();

          
                  
       
            if (sliderComponent != null)
            {
                sliderComponent.value = 0.5f;  // Establecer el valor inicial del slider, por ejemplo
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
    // Reinicializar las variables cuando el objeto es activado
    triggerColorchange = true;
    triggerColorchangeStop = false;
    chasing=false;
    chase=true;
  changeStartcourtine=true;
 changestarStoptcourtine=true;
changeStoptcourtine=true;
changeStartcontcourtine=true;
    
}
    public override void ExecuteBehavior(GameObject obj, bool con)

    {
       
      Vector3 worldPosition = obj.transform.position + offset;

// Asegúrate de que el Slider exista
if (sliderInstance != null)
{
    // Establecer la posición del Slider en World Space
    sliderInstance.transform.position = worldPosition;

    // Ajustar el tamaño (escala) del Slider
    sliderInstance.GetComponent<RectTransform>().localScale = new Vector3(0.02f, 0.01f, 0.1f);  // Ajusta estos valores para controlar el tamaño

    // Si necesitas controlar la visibilidad, puedes usar la distancia del objeto con la cámara
    float distanceToCamera = Vector3.Distance(Camera.main.transform.position, worldPosition);

    // Cambiar la visibilidad del slider según la distancia o según otras condiciones
    if (distanceToCamera > 0)  // O cualquier otra condición que necesites
    {
        sliderCanvasGroup.alpha = 1f;  // Totalmente visible
    }
    else
    {
        sliderCanvasGroup.alpha = 0f;  // Totalmente invisible
    }
}
if (sliderInstance != null)
{
    // Establecer la posición del Slider en World Space
  
    // Ajustar el tamaño (escala) del Slider
      Vector3 directionToPlayer = (player.transform.position - obj.transform.position).normalized;
          
  // Ajusta estos valores para controlar el tamaño

    // Si necesitas controlar la visibilidad, puedes usar la distancia del objeto con la cámara
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
                 sliderInstance.transform.rotation = Quaternion.Slerp( sliderInstance.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);


    // Cambiar la visibilidad del slider según la distancia o según otras condiciones
}
        

        if(con==false)
        {
        Rigidbody rb = obj.GetComponent<Rigidbody>();

                 agent.isStopped=true;
         

            
            
                float distanceToPlayer =  Vector3.Distance(obj.transform.position, player.transform.position);
              
                    if(distanceToPlayer> chaseDistance)
                    {
                        if(triggerColorchangeStop==false)
                        {
                        
Debug.Log("!");
                // Calcular la dirección hacia el jugador
                Vector3 directionToPlayer = (player.transform.position - obj.transform.position).normalized;
          
                // Crear la rotación basada en la dirección (solo rotar en el eje Y)
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

                // Aplicar la rotación suavemente (opcional)
                obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                // Moverse hacia el jugador
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
             animator.SetBool("Run", true);
                  Vector3 directionToPlayer = (player.transform.position - obj.transform.position).normalized;
          
                // Crear la rotación basada en la dirección (solo rotar en el eje Y)
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

                // Aplicar la rotación suavemente (opcional)
                obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                   obj.transform.Translate(directionToPlayer * attackspeed * Time.deltaTime);
                           if(changestarStoptcourtine)
                           {
                            sliderColor.getChangeColorStopStart();
                            changestarStoptcourtine=false;
                           }
                           }
                           else
                           {
                      Debug.Log("3");
                           
                             chasing=false;
                              trigger.cambio=true;
                          
                            triggerColorchangeStop=false;
                           
                           }
                            
                        }
            }
              else
            {
                if(sliderColor.end==true)
                {
                     if(changeStoptcourtine)
                    {
sliderColor.getChangeColorbackStop();
changeStoptcourtine=false;
                    }
                    chasing=true;
                }
                if(chasing==false)
                {
                
                triggerColorchangeStop=true;
                if(sliderColor.triggerColorChange==true)
                            {
                              changestarStoptcourtine=true;
             
                           Debug.Log("4");     
             animator.SetBool("Chase", false);
                 animator.SetBool("Run", false);
                 agent.isStopped=true;
             
if(changeStartcourtine)
{
                 sliderColor.getChangeColorStar();
                 changeStartcourtine=false;
}


                            }
                            else{

         Debug.Log("5");
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
                  triggerColorchangeStop=true;
                if(sliderColor.triggerColorChange==true)
                {
                    changestarStoptcourtine=true;
                
                           Debug.Log("6"); 
                         
               Vector3 directionToPlayer = (player.transform.position - obj.transform.position).normalized;
          
                // Crear la rotación basada en la dirección (solo rotar en el eje Y)
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

                // Aplicar la rotación suavemente (opcional)
                obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                 Vector3 direction = (player.transform.position - obj.transform.position).normalized;
                obj.transform.Translate(direction * attackspeed *2 * Time.deltaTime);
                 animator.SetBool("Run", true);
                 agent.isStopped=true;
               if(changeStartcontcourtine)
               {
 sliderColor.getChangeColorStarcon();
 changeStartcontcourtine=false;
               }
            }
            else{
 Debug.Log("7");  
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
          Debug.Log("8");
Patrol();

    }
    } 
    

  private void Patrol()
    {
         agent.isStopped=false;
     animator.SetBool("Chase", true);
         // Si ya estamos cerca del destino actual, asignamos el siguiente punto
    if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
    {
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length; // Avanzar al siguiente punto de patrullaje
        agent.isStopped = false;
        agent.SetDestination(patrolPoints[currentPointIndex].position); // Establecer el nuevo destino
    }

    // Cambiar la animación solo si el agente está en movimiento
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

