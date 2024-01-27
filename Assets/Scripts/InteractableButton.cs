using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : InteractableObject
{
    protected override void Interact() {
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
