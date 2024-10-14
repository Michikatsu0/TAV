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
    // Referencia al canvas donde se colocará el slider
    private GameObject canvas;
private Slider sliderComponent;

  

private SliderHealth sliderHealth;
public Vector3 offset= new Vector3(0,2f,0);
    public override void Inicialize(GameObject obj)
    {
        pivote=GameObject.Find("Pivote");
        currentHealth=maxhealth;
         child=obj.transform.GetChild(0).gameObject;
        animator=child.GetComponent<Animator>();
         canvas = GameObject.Find("Canvas");
        if (sliderPrefab != null && canvas != null)
        {
            // Instanciar el slider como hijo del Canvas
             sliderInstance = Instantiate(sliderPrefab, canvas.transform);
            
            // Configurar la posición del slider (puedes ajustar esto según tus necesidades)
            sliderInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);  // Por ejemplo, centrado en el Canvas
            
            // Opcional: Acceder y modificar componentes del slider si es necesario
            sliderComponent = sliderInstance.GetComponent<Slider>();
                 
          sliderHealth=sliderComponent.GetComponent<SliderHealth>();
                  sliderHealth.ChangeMaxvalue(maxhealth);
       
            if (sliderComponent != null)
            {
                sliderComponent.value = 0.5f;  // Establecer el valor inicial del slider, por ejemplo
            }
             
        }
        
        
    }

    public override void ExecuteBehavior(GameObject obj, bool con)

    {
        pivote.transform.position=new Vector3(obj.transform.position.x,obj.transform.position.y+1.2f,obj.transform.position.z);
        // Movimiento con teclas WASD
      Vector3 worldPosition = obj.transform.position + offset;
sliderHealth.ChangeValue(currentHealth);
// Asegúrate de que el Slider exista
if (sliderInstance != null)
{
    // Establecer la posición del Slider en World Space
    sliderInstance.transform.position = worldPosition;

    // Ajustar el tamaño (escala) del Slider
      Vector3 directionToPlayer = (Camera.main.transform.position - obj.transform.position).normalized;
          
    sliderInstance.GetComponent<RectTransform>().localScale = new Vector3(0.02f, 0.01f, 0.03f);  // Ajusta estos valores para controlar el tamaño

    // Si necesitas controlar la visibilidad, puedes usar la distancia del objeto con la cámara
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
                 sliderInstance.transform.rotation = Quaternion.Slerp( sliderInstance.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);


    // Cambiar la visibilidad del slider según la distancia o según otras condiciones
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
     
        
        // Activar la animación de caminar
       if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || 
         Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.RightArrow) || 
         Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || 
         Input.GetKey(KeyCode.DownArrow)) && !Input.GetKey(KeyCode.LeftShift))
    {
        // Activar la animación de caminar
        
       
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
    // Comprobar si se está presionando cualquier tecla de movimiento con Shift (para correr)
   
        // Activar la animación de correr
       
    
    // Si no se está presionando ninguna tecla de movimiento
    else if(!Input.anyKey)
    {
        // Detener todas las animaciones de movimiento
        if (animator != null)
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
            animator.SetBool("runstop", true);
        }
    }
}
    
}

    // Método para recibir daño
  

