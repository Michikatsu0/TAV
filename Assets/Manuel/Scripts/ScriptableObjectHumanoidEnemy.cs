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
public Vector3 offset= new Vector3(0,5f,0);
    // Referencia al prefab del slider
    public GameObject sliderPrefab;
       private GameObject sliderInstance;
    // Referencia al canvas donde se colocará el slider
    private GameObject canvas;
    private trigger trigger;
private Slider sliderComponent;
private CanvasGroup sliderCanvasGroup;

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
}
    public override void ExecuteBehavior(GameObject obj, bool con)

    {
       
       Vector3 screenPos = Camera.main.WorldToScreenPoint(obj.transform.position+offset);

        // Actualizar la posición del slider en función de la posición del objeto
        if(sliderInstance!=null)
        {  sliderInstance.transform.position = screenPos;

        // Cambiar visibilidad a través de la transparencia (alpha)
        if (screenPos.z > 0)
        {
            sliderCanvasGroup.alpha = 1f;  // Totalmente visible
        }
        else
        {
            sliderCanvasGroup.alpha = 0f;  // Totalmente invisible
        }
    
        
        }
       
        sliderInstance.GetComponent<RectTransform>().transform.localScale= new Vector3(0.5f,0.5f,0.5f);
        

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
                             Debug.Log("2");
 rb.velocity = Vector3.zero; // Detiene el movimiento
rb.angularVelocity = Vector3.zero;
 animator.SetBool("Chase", false);
                 animator.SetBool("Run", false);
                  rb.velocity = Vector3.zero; // Detiene el movimiento
rb.angularVelocity = Vector3.zero;
                 
                
                            sliderColor.getChangeColorStopStart();
                           }
                           else
                           {
                      Debug.Log("3");
                           
                             
                              trigger.cambio=true;
                            triggerColorchangeStop=false;
                            sliderColor.triggerColorChange=true;
                           }
                            
                        }
            }
              else
            {
                

                triggerColorchangeStop=true;
                if(sliderColor.triggerColorChange==true)
                            {
                           Debug.Log("4");     
             animator.SetBool("Chase", false);
                 animator.SetBool("Run", false);
                 agent.isStopped=true;
                 rb.velocity = Vector3.zero; // Detiene el movimiento
rb.angularVelocity = Vector3.zero;
                 sliderColor.getChangeColorStar();
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
                obj.transform.Translate(direction * moveSpeed *2 * Time.deltaTime);
                            }
            
            }
            
          
        
    }
    else{
          Debug.Log("6");
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

