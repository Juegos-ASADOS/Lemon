using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldToComplete : MonoBehaviour
{
    [SerializeField]
    private float holdTime = 1.0f;

    [SerializeField]
    private String tag;

    private bool canHold = false;
    void Start()
    {

    }
    void OnMouseOver()
    {
        if (tag == gameObject.tag)
        {
            canHold = true;
            Debug.Log("Can hold");
        }
    }
    void OnMouseExit()
    {
        if (tag == gameObject.tag)
        {
            canHold = false;
            Debug.Log("Can't hold");
        }
    }
    void Update()
    {
        if (canHold && Input.GetMouseButton(0) && holdTime >= 0.0f)
        {
            holdTime -= Time.deltaTime;
        }
        if (holdTime <= 0.0f)
        {
            Debug.Log(tag+" Completed!");
            ///TODO: acción de clic completada
        }
    }
}
