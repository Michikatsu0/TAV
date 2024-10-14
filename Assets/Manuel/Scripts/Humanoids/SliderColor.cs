using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class SliderColor: MonoBehaviour
{
    public Slider slider; // Referencia al Slider // Objeto al que el Slider mira
     [HideInInspector] public bool triggerColorChange = true; 
      [HideInInspector] public bool triggerColorChangeStop = true; // Condición pública para activar el cambio de color
  [HideInInspector] public Color startColor = Color.green; // Color inicial
  [HideInInspector] public Color endColor = Color.red; // Color final
    public float duration = 5f; // Duración del cambio de color
    [HideInInspector] public Transform objectposition;
   [HideInInspector] private Transform playerTransform;
[HideInInspector] public Image fillImage;

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
}
    void Update()
    {
        // Si hay un objeto asignado para la posición, mover el Slider a esa posición
        
        
    

        // Comienza el cambio de color si la condición se cumple
       
    }

   
 private IEnumerator ChangeColorback()
    {
        
    float elapsedTime = 0f;
      
        

        // Cambia el color de verde a rojo
        while (elapsedTime <  duration)
        {
            slider.value=elapsedTime;
             fillImage.color = Color.Lerp( endColor,startColor, elapsedTime/duration );
             if(elapsedTime==duration-1)
             {
                 triggerColorChangeStop=false;
             }
            elapsedTime += Time.deltaTime;
            yield return null; // Espera el siguiente frame
        }
           
        triggerColorChangeStop=false;


       
    }
    private IEnumerator ChangeColor()
    {
        Debug.LogFormat("Funciona");
        
        float elapsedTime = duration;
      float changetimer=0;
        

        // Cambia el color de verde a rojo
        while (elapsedTime >  0)
        {
            slider.value=elapsedTime;
            
             fillImage.color = Color.Lerp( startColor, endColor, changetimer/duration);
           
            elapsedTime -= Time.deltaTime;
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
    
    
}