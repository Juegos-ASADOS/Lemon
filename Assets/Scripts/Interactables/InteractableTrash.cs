using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTrash : InteractableObject
{
    public override void Interact(GameObject pickedObject)
    {
        if(pickedObject != null)
        {
            if(pickedObject.GetComponent<InteractableCup>() != null)
                FMOD_Manager.instance.PlaySingleInstanceEmitterControllerGroup("Glass");
            PlayerInstance.instance.DumpObject();
        }
    }
}
