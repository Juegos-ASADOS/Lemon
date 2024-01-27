using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : InteractableObject
{
    public override void Interact(GameObject pickedObject) {
        if(pickedObject != null)
        {
            //SONIDO interactuar pero no funciona
            return;
        }
        GetComponent<Animator>().SetTrigger("Pressed");
        if(this.name == "Enter")
        {
            //SONIDO boton
            this.transform.parent.GetComponentInParent<Register>().Enter();
        }
        else
        {
            //SONIDO boton
            this.transform.parent.GetComponentInParent<Register>().Number(this.name);

        }
    }
}
