using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Limoncin : MonoBehaviour
{
    public static event Action<string> LimoncinEvent = delegate { };

    private void Start()
    {
        //Esto Tendrá que ser o cuando se coja o cuando se intente cortar o lo que sea
        LimoncinEvent("Limoncin");
    }
}