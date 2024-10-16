using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHealth : MonoBehaviour
{
 
    public Slider slider;
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

    public void ChangeMaxvalue(float max)
    {
        slider.maxValue=max;
    }
    public void ChangeValue(float max)
    {
        slider.value=max;
    }
}
