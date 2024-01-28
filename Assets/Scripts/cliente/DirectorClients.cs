using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorClients : MonoBehaviour
{
    public static event Action ClientEnter = delegate { };
    public static event Action ClientExit = delegate { };
    public static event Action ClientAppear = delegate { };
    public static event Action ClientDisappear = delegate { };


    //Ejemplo de como queremos enviar los eventos al cliente y de como los va a recibir el

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ClientEnter();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ClientExit();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ClientAppear();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ClientDisappear();
        }
#endif
    }
}
