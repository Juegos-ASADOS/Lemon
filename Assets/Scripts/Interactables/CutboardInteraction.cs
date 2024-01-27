using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutboardInteraction : InteractableObject
{
    private GameObject objectContained;

    [SerializeField]
    private float holdTime = 1.0f;

    private bool hover = false;
    private bool canHold = false;
    private bool completed = false;

    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject != null)
        {
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.CUCHILLO &&
                objectContained != null && objectContained.GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                canHold = true;
            }
            else if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                pickedObject.transform.position = gameObject.transform.position;
                pickedObject.transform.parent = gameObject.transform;
                objectContained = pickedObject;
                PlayerInstance.instance.RemoveHandObject();
            }
        }
    }

    void OnMouseOver() { hover = true; }
    void OnMouseExit() { hover = false; }

    void Update()
    {
        if (hover && canHold && Input.GetMouseButton(0) && holdTime >= 0.0f)
        {
            holdTime -= Time.deltaTime;
        }
        if (holdTime <= 0.0f && !completed)
        {
            Debug.Log(tag + " Completed!");
            completed = true;
            ///TODO: acción de clic completada
        }
    }
}
