using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientCounterPosition : MonoBehaviour
{
    private bool InSight;

    public static event Action CounterOutOfSight = delegate { };

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
