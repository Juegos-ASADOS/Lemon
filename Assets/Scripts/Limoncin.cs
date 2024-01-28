using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Limoncin : MonoBehaviour
{
    public Animator animator;
    public static event Action<string> LimoncinEvent = delegate { };

    private void Start()
    {
        //Esto Tendr� que ser o cuando se coja o cuando se intente cortar o lo que sea
        LimoncinEvent("Limoncin");
    }
    public void HablaLimoncin(int a)
    {

        LimoncinEvent("Limoncin"+a.ToString());
    }
    public void StartAnimation()
    {
        animator.SetBool("talk",true);
    }
    public void EndAnimation()
    {
        animator.SetBool("talk",false);

    }
}