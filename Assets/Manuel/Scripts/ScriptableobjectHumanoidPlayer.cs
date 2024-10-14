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
  public float runSpeed=20f;
  private float currentspeed;
  private GameObject child;
    private Animator animator;


 public GameObject sliderPrefab;
       private GameObject sliderInstance;
    // Referencia al canvas donde se colocará el slider
    private GameObject canvas;
private Slider sliderComponent;

  

private SliderHealth sliderHealth;
public Vector3 offset= new Vector3(0,2f,0);
    public override void Inicialize(GameObject obj)
    {
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
        // Movimiento con teclas WASD
       
    sliderHealth.ChangeValue(currentHealth);
     Vector3 screenPos = Camera.main.WorldToScreenPoint(obj.transform.position+offset);

        // Actualizar la posición del slider en función de la posición del objeto
       
              sliderInstance.transform.position = screenPos;
           
        
        
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
  

