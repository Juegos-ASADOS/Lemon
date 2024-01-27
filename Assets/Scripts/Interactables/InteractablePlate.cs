using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlate : InteractableObject
{
    private GameObject objectContained;

    public GameObject getFood()
    {
        return objectContained;
    }
    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject != null && transform.parent != null)
        {
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.COMIDA)
            {
                if (pickedObject.GetComponent<FruitCharacteristics>() == null || pickedObject.GetComponent<FruitCharacteristics>().IsCut())
                {

                pickedObject.transform.position = transform.GetChild(0).position;
                pickedObject.transform.parent = transform;
                objectContained = pickedObject;
                PlayerInstance.instance.RemoveHandObject();
                }
            }
        }
    }
}
