using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    void Start()
    {
        
    }

    // Update is called once per frame
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
