using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TorretLook : MonoBehaviour
{
    public MultiAimConstraint multiAimConstraint;
    private GameObject player;
  
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
          WeightedTransformArray sources = multiAimConstraint.data.sourceObjects;
        sources.SetTransform(0, player.transform); 
        
       
        multiAimConstraint.data.sourceObjects = sources;
    }

   
    void Update()
    {
        
    }
}
