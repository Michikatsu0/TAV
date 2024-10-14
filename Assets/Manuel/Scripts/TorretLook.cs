using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TorretLook : MonoBehaviour
{
    public MultiAimConstraint multiAimConstraint;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
          WeightedTransformArray sources = multiAimConstraint.data.sourceObjects;
        sources.SetTransform(0, player.transform); // El índice 0 porque solo tienes un source object
        
        // Asignar la lista actualizada
        multiAimConstraint.data.sourceObjects = sources;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
