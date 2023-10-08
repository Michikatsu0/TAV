using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceMaterial : MonoBehaviour
{
    // Start is called before the first frame update

    public bool setRealTime, setColorFor;

    [SerializeField] private float seconds = 1f;

    public bool setColor, emission;
    [SerializeField] private Color color;
    
    private MeshRenderer m_Renderer;
    private Material material;
    private float time = 0;
    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        material = m_Renderer.material;
        material.color = color;
        SetInstanceMaterial();
    }

    void Update()
    {
        if (setRealTime)
        {
            setColorFor = false;
            setColorFor = false;
            SetInstanceMaterial();
        }
        else if (setColorFor)
        {
            setRealTime = false;
            setColor = false;

            time += Time.deltaTime;
            if (time <= seconds)
                SetInstanceMaterial();
            else
            {
                time = 0;
                setColorFor = false;
            }
        }
        else if (setColor)
        {
            setColor = false;
            setRealTime = false;
            setColorFor = false;
            SetInstanceMaterial();
        }
    }

    public void ChangeColor(Color color, bool emission)
    {
        this.color = color;
        setColor = true;
        if (emission)
            material.EnableKeyword("_EMISSION");
        else
            material.DisableKeyword("_EMISSION");
    }

    private void SetInstanceMaterial()
    {
        material.color = color;
        material.SetColor("_EmissionColor", color);
        if (emission)
            material.EnableKeyword("_EMISSION");
        else
            material.DisableKeyword("_EMISSION");
    }

}
