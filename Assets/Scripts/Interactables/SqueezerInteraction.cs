using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueezerInteraction : InteractableObject
{
    [SerializeField]
    private GameObject objectContained;

    void Update()
    {
        if (objectContained != null)
        {

        }
    }
    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject != null)
        {
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                GetComponent<HoldToComplete>().changeHold(true);
            }
        }
    }
}
