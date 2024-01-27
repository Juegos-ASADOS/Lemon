using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerInteractable : InteractableObject
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject.GetComponent<InteractableObject>().objType != ObjectType.DINERO) return;
        if (pickedObject != null)
            PlayerInstance.instance.RemoveHandObject();
        pickedObject.transform.parent = transform.GetChild(0);
        pickedObject.transform.position = Vector3.zero;
        pickedObject.transform.GetComponent<Rigidbody>().isKinematic = false;
    }
}
