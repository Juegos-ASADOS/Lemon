using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType { FRUTA, BOLLO, VASO, CUCHILLO, EXPRIMIDOR, CAJA, TABLA, PAPELERA, BASE, TIMBRE, DINERO, CAJONDINERO, BANDEJA }


public class InteractableObject : MonoBehaviour
{
    public ObjectType objType;

    private void OnMouseDown()
    {
        PlayerInstance.instance.ClickObject(gameObject);
    }

    public virtual void Interact(GameObject pickedObject) { }
}
