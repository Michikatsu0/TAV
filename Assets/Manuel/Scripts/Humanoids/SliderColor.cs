using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class SliderColor: MonoBehaviour
{
    public Slider slider; 
     [HideInInspector] public bool triggerColorChange = true; 
      [HideInInspector] public bool triggerColorChangeStop = true; 
      [HideInInspector] public bool changeStop = true; 
  [HideInInspector] public Color startColor = Color.green; 
  [HideInInspector] public Color endColor = Color.red; 
    public float duration = 10f; 
    [HideInInspector] public Transform objectposition;
   [HideInInspector] private Transform playerTransform;
[HideInInspector] public Image fillImage;
private float elapsedTime = 0f;
public bool change=true;
public bool end;
public bool end2;
private float tempTime;

public bool changeTimer=true;
public bool changeTimer2=true;
    void Start()
    {
       triggerColorChange = true; 
       triggerColorChangeStop = true; 
       
        if (slider != null)
        { fillImage= slider.fillRect.GetComponent<Image>();
            fillImage.GetComponent<Image>().color = startColor;
           
        }
        slider.maxValue=duration;
        slider.value= 0;
    }

void OnEnable()
{

    triggerColorChange = true;
    triggerColorChangeStop = true;
    changeStop = false;
    change=true;
    end=false;
}
    void Update()
    {
      
        if(ChangeColor()!=null)
        {
 if(Input.GetKeyDown(KeyCode.Y))
      {
        StartCoroutine(ChangeColor());
      }

        }


      
    
    }

   
 public IEnumerator ChangeColorback()
    {
      
    float elapsedTime = 0f;
     Debug.LogFormat("Funciona1");
        
end=true;
      
        while (elapsedTime <  duration)
        {
         
            tempTime=elapsedTime;
            slider.value=elapsedTime;
             fillImage.color = Color.Lerp( startColor,endColor, elapsedTime/duration );
             
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        
         end=false;
        triggerColorChange=false;
           
           
}
public IEnumerator ChangeColorBackcon()
    {
      
    

     
        while (tempTime <  duration)
        {
         
            
            slider.value=tempTime;
             fillImage.color = Color.Lerp(startColor ,endColor, tempTime/duration );
             
            tempTime += Time.deltaTime;
            yield return null; 
        }
        
        
        triggerColorChange=false;
           
           
}

       
    
    private IEnumerator ChangeColor()
    {
        end2=true;
        Debug.LogFormat("Funciona2");
        
        float elapsedTime = duration;
      float changetimer=0;
        

     
        while (elapsedTime >  0)
        {
            tempTime=elapsedTime;
            slider.value=elapsedTime;
            
             fillImage.color = Color.Lerp( endColor, startColor, changetimer/duration);
           
            elapsedTime -= Time.deltaTime;
            changetimer+=Time.deltaTime;
            yield return null; 
        }
        end2=false;
        changeStop=true;
        triggerColorChangeStop=false;
        
    }
     private IEnumerator ChangeColorcon()
    {   
        Debug.LogFormat("Funciona3");
        
       
      float changetimer=duration-tempTime;
        

     
        while (tempTime >  0)
        {
            
            slider.value=tempTime;
            
             fillImage.color = Color.Lerp( endColor, startColor, changetimer/duration);
           
            tempTime -= Time.deltaTime;
            changetimer+=Time.deltaTime;
            
            yield return null; 
        }
       
        triggerColorChangeStop=false;
        
    }

    public IEnumerator ExitTime(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Aqui");
       changeTimer=false;
    }
    public IEnumerator ExitTime2(float time)
    {
        yield return new WaitForSeconds(time);
       changeTimer2=false;
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
       public void getChangeColorbackcon()
       {
        StartCoroutine(ChangeColorBackcon());
       }
        public void getChangeColorbackStop()
       {
       StopAllCoroutines();
             
       }
         public void getChangeColorStop()
       {
       StopAllCoroutines();
             
       }
    public void getExitTime(float con)
    {
     StartCoroutine(ExitTime(con));
    }
     public void getExitTime2(float con)
    {
     StartCoroutine(ExitTime2(con));
    }
 
 
}