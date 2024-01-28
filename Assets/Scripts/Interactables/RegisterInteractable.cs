using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterInteractable : InteractableObject
{
    public CameraWaypoint camPos;
    public override void Interact(GameObject pickedObject)
    {
        MovingCamera mc = Camera.main.transform.GetComponent<MovingCamera>();
        StartCoroutine(mc.ForcedRotateTo(camPos));
        transform.GetComponent<Collider>().enabled = false;
    }
}
