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
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.VASO)
            {
                pickedObject.transform.position = gameObject.transform.position;
                pickedObject.transform.parent = gameObject.transform;
                objectContained = pickedObject;
                PlayerInstance.instance.RemoveHandObject();
            }
        }
    }
}
