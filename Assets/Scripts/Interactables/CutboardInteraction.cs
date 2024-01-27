using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutboardInteraction : InteractableObject
{
    [SerializeField]
    private GameObject objectContained;

    void Update()
    {
        if (objectContained != null)
        {
            if (objectContained.GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                GetComponent<HoldToComplete>().changeHold(true);
            }
        }
    }
    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject != null)
        {
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.CUCHILLO)
            {
                GetComponent<HoldToComplete>().changeHold(true);
            }
            else if(pickedObject.GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                pickedObject.transform.position = this.gameObject.transform.position;
                objectContained = pickedObject;
                PlayerInstance.instance.RemoveHandObject();
            }
        }
    }
}
