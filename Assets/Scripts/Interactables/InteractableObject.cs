using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectType { FRUTA, BOLLO, VASO, CUCHILLO, EXPRIMIDOR, CAJA }

public class InteractableObject : MonoBehaviour
{
    public ObjectType objType;

    private void OnMouseDown()
    {
        // Player.click(type)
    }

    public virtual void Interact(GameObject pickedObject) { }
}
