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

    private bool hover = false;
    private bool canHold = false;
    private bool completed = false;

    void OnMouseOver()
    {
        if (tag == gameObject.tag)
        {
            hover = true;
        }
    }
    void OnMouseExit()
    {
        if (tag == gameObject.tag)
        {
            hover = false;
        }
    }
    void Update()
    {
        if (hover && canHold && Input.GetMouseButton(0) && holdTime >= 0.0f)
        {
            holdTime -= Time.deltaTime;
        }
        if (holdTime <= 0.0f &&!completed)
        {
            Debug.Log(tag + " Completed!");
            completed = true;
            ///TODO: acción de clic completada
        }
    }
    public void ChangeHold(bool canHold_)
    {
        canHold = canHold_;
    }
    public bool GetCompleted() { return completed; }
}
