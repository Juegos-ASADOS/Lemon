using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableTimbre : InteractableObject
{
    private bool InSight;

    // public event EventHandler<items> SendOrder;
    public static event Action<items> SendOrder = delegate { };
    public static event Action CounterOutOfSight = delegate { };
    public override void Interact(GameObject pickedObject) {
        Debug.Log("Timbrado");
        //lanzar evento de pedido listo
        items i;

        //_sendOrder?.Invoke();
        SendOrder(i);
    }


    private void Update()
    {
        
        if (InSight != I_Can_See())
        {
            InSight = !InSight;
            if (!InSight)
            {
                Debug.Log("fuera de vista");
                CounterOutOfSight();
            }
        }
    }
    private bool I_Can_See()
    {

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider>().bounds))
            return true;
        else
            return false;
    }
}



public struct items
{

}
