using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutboardInteraction : InteractableObject
{
    [SerializeField]
    private float holdTime = 1.0f;
    private float restingTime = 0.0f;

    [SerializeField]
    private ParticleSystem cutParticles;

    private GameObject knife;

    private bool hover = false;
    private bool canHold = false;

    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject != null)
        {
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.CUCHILLO &&
                transform.childCount == 2  && transform.GetChild(1).GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                canHold = true;
                knife = pickedObject;
            }
            else if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                pickedObject.transform.position = transform.GetChild(0).position;
                pickedObject.transform.parent = transform;
                PlayerInstance.instance.RemoveHandObject();
            }
        }
    }

    void OnMouseOver() { hover = true; }
    void OnMouseExit() { hover = false; }

    void Update()
    {
        if (hover && canHold && Input.GetMouseButton(0) && restingTime >= 0.0f)
        {
            restingTime -= Time.deltaTime;
        }
        if (restingTime <= 0.0f)
        {
            if (transform.childCount == 2)
            {
                transform.GetChild(1).GetComponent<FruitCharacteristics>().cutFruit();
                canHold = false;
            }
            cutParticles.transform.position = transform.GetChild(0).position;
            cutParticles.Play();
            restingTime = holdTime;
            if (knife != null)
            {
                knife.GetComponent<KnifeInteraction>().CutEnd();
                knife = null;
            }
        }
    }
}
