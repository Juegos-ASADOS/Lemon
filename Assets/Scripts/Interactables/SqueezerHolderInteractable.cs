using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueezerHolderInteractable : InteractableObject
{
    public override void Interact(GameObject pickedObject)
    {
        /*if(pickedObject == null)
            PlayerInstance.instance.ClickObject(transform.GetChild(1).gameObject);
        else*/ 
        if (pickedObject != null && pickedObject.GetComponent<InteractableObject>().objType == ObjectType.EXPRIMIDOR)
        {
            PlayerInstance.instance.RemoveHandObject();
            pickedObject.transform.parent = transform;
            pickedObject.transform.position = transform.GetChild(0).position;
        }
            
    }
}
