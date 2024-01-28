using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterInteractable : InteractableObject
{
    public CameraWaypoint camPos;
    private MovingCamera mc;
    private Collider col;
    float wait = 0.75f;
    float timer = 0;
    private void Start()
    {
        mc = Camera.main.transform.GetComponent<MovingCamera>();
        col = transform.GetComponent<Collider>();
    }
    public override void Interact(GameObject pickedObject)
    {
        StartCoroutine(mc.ForcedRotateTo(camPos));
        col.enabled = false;
        timer = wait;
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }
        if(!col.enabled && mc.getCameraMoving())
            col.enabled = true;
    }
}
