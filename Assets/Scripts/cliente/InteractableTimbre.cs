using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableTimbre : InteractableObject
{

    // public event EventHandler<items> SendOrder;
    public static event Action SendOrder = delegate { };
    public override void Interact(GameObject pickedObject) {
        Debug.Log("Timbrado");
        FMOD_Manager.instance.PlaySingleInstanceEmitterControllerGroup("RingBell");
        //lanzar evento de pedido listo

        SendOrder();
    }
}



