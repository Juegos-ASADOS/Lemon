using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayInteraction : InteractableObject
{
    private GameObject objectContained;

    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject != null)
        {            
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.VASO||
                pickedObject.GetComponent<InteractableObject>().objType == ObjectType.PLATO)
            {
                pickedObject.transform.position = transform.GetChild(0).position;
                pickedObject.transform.parent = transform;
                objectContained = pickedObject;
                PlayerInstance.instance.RemoveHandObject();
            }
        }
    }
}
