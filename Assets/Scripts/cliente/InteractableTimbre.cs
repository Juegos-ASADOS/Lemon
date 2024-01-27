using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableTimbre : InteractableObject
{

    // public event EventHandler<items> SendOrder;
    public static event Action<items> SendOrder = delegate { };
    public override void Interact(GameObject pickedObject) {
        Debug.Log("Timbrado");
        //lanzar evento de pedido listo
        items i;

        //_sendOrder?.Invoke();
        SendOrder(i);
    }
}



public struct items
{

}
