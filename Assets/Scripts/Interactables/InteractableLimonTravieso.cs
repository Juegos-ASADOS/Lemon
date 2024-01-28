using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLimonTravieso : InteractableObject
{
    private Vector3 initialPos;
    private void Start()
    {
        initialPos = transform.position;
    }
    public void BackToStart()
    { 
        transform.parent = null;
        transform.position = initialPos;
    }
}
