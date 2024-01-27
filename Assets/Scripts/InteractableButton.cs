using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : InteractableObject
{
    public override void Interact(GameObject pickedObject) {
        if(this.name == "Enter")
        {
            this.transform.parent.GetComponentInParent<Register>().Enter();
        }
        else
        {
            this.transform.parent.GetComponentInParent<Register>().Number(this.name);

        }
    }
}
