using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutboardInteraction : InteractableObject
{
    private GameObject objectContained;

    void Update()
    {
       
    }
    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject != null)
        {
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.CUCHILLO && 
                objectContained != null && objectContained.GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                GetComponent<HoldToComplete>().ChangeHold(true);
            }
            else if(pickedObject.GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                pickedObject.transform.position = gameObject.transform.position;
                pickedObject.transform.parent = gameObject.transform;
                objectContained = pickedObject;
                PlayerInstance.instance.RemoveHandObject();
            }
        }
    }
}
