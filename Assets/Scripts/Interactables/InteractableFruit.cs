using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLemon : InteractableObject
{
    public override void Interact(GameObject pickedObject) {
        Debug.Log("B");
    }
}
