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
        transform.GetComponent<Animator>().SetTrigger("Pressed");
        if (this.name == "Enter")
        {
            //SONIDO boton
            this.transform.parent.GetComponentInParent<Register>().Enter();
        }
        else
        {
            FMOD_Manager.instance.PlaySingleInstanceEmitterControllerGroup("Button");
            //SONIDO boton
            this.transform.parent.GetComponentInParent<Register>().Number(this.name);

        }
    }
}
