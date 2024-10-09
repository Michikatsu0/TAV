using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[CreateAssetMenu(fileName = "HumanoidPlayer", menuName = "ScriptableObjects/HumanoidPlayer", order = 1)]
public class ScriptableobjectHumanoidPlayer : Types
{
  public float moveSpeed = 100f;
  public float runSpeed=200f;
  private float currentspeed;
  private GameObject child;
    private Animator animator;

    public override void Inicialize(GameObject obj)
    {
        
    }

    public override void ExecuteBehavior(GameObject obj)
    {
        // Movimiento con teclas WASD
      
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
      child=obj.transform.GetChild(0).gameObject;
        animator=child.GetComponent<Animator>();
        
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
    else
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
  

