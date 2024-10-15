using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class SliderColor: MonoBehaviour
{
    public Slider slider; // Referencia al Slider // Objeto al que el Slider mira
     [HideInInspector] public bool triggerColorChange = true; 
      [HideInInspector] public bool triggerColorChangeStop = true; 
      [HideInInspector] public bool changeStop = true; // Condición pública para activar el cambio de color
  [HideInInspector] public Color startColor = Color.green; // Color inicial
  [HideInInspector] public Color endColor = Color.red; // Color final
    public float duration = 10f; // Duración del cambio de color
    [HideInInspector] public Transform objectposition;
   [HideInInspector] private Transform playerTransform;
[HideInInspector] public Image fillImage;
private float elapsedTime = 0f;
public bool change=true;
public bool end;
private float tempTime;
    void Start()
    {
       triggerColorChange = true; 
       triggerColorChangeStop = true; 
        // Asegúrate de que el slider tenga el color inicial
        if (slider != null)
        { fillImage= slider.fillRect.GetComponent<Image>();
            fillImage.GetComponent<Image>().color = startColor;
           
        }
        slider.maxValue=duration;
        slider.value= slider.maxValue;
    }

void OnEnable()
{
    // Reinicializar las variables cuando el objeto es activado
    triggerColorChange = true;
    triggerColorChangeStop = true;
    changeStop = false;
    change=true;
    end=false;
}
    void Update()
    {
        // Si hay un objeto asignado para la posición, mover el Slider a esa posición
        if(ChangeColor()!=null)
        {
 if(Input.GetKeyDown(KeyCode.Y))
      {
        StartCoroutine(ChangeColor());
      }

        }


        // Comienza el cambio de color si la condición se cumple
    
    }

   
 public IEnumerator ChangeColorback()
    {
      
    float elapsedTime = 0f;
     Debug.LogFormat("Funciona1");
        
end=true;
        // Cambia el color de verde a rojo
        while (elapsedTime <  duration)
        {
         
            tempTime=elapsedTime;
            slider.value=elapsedTime;
             fillImage.color = Color.Lerp( endColor,startColor, elapsedTime/duration );
             
            elapsedTime += Time.deltaTime;
            yield return null; // Espera el siguiente frame
        }
        
         end=false;
        triggerColorChangeStop=false;
           
           
}

       
    
    private IEnumerator ChangeColor()
    {
        Debug.LogFormat("Funciona2");
        
        float elapsedTime = duration;
      float changetimer=0;
        

        // Cambia el color de verde a rojo
        while (elapsedTime >  0)
        {
            tempTime=elapsedTime;
            slider.value=elapsedTime;
            
             fillImage.color = Color.Lerp( startColor, endColor, changetimer/duration);
           
            elapsedTime -= Time.deltaTime;
            changetimer+=Time.deltaTime;
            yield return null; // Espera el siguiente frame
        }
        changeStop=true;
        triggerColorChange=false;
        
    }
     private IEnumerator ChangeColorcon()
    {   
        Debug.LogFormat("Funciona3");
        
       
      float changetimer=tempTime;
        

        // Cambia el color de verde a rojo
        while (tempTime >  0)
        {
            
            slider.value=tempTime;
            
             fillImage.color = Color.Lerp( startColor, endColor, changetimer/duration);
           
            tempTime -= Time.deltaTime;
            changetimer+=Time.deltaTime;
            yield return null; // Espera el siguiente frame
        }
       
        triggerColorChange=false;
        
    }
       public void getChangeColorStar()
       {
        StartCoroutine(ChangeColor());
       }
        public void getChangeColorStopStart()
       {
        StartCoroutine(ChangeColorback());
       }
     public void getChangeColorStarcon()
       {
        StartCoroutine(ChangeColorcon());
       }
        public void getChangeColorbackStop()
       {
       StopAllCoroutines();// Interrumpir la coroutine
             
       }
       public void getChange()
       {
      
       }
 
 
}