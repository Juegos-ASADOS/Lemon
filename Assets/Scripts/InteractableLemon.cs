using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLemon : InteractableObject
{
    protected override void Interact() {
        Debug.Log("B");
    }
}
