using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType { FRUTA, COMIDA, VASO,PLATO, CUCHILLO, EXPRIMIDOR, CAJA, TABLA, PAPELERA, BASE, TIMBRE, DINERO, CAJONDINERO, BANDEJA }


public class InteractableObject : MonoBehaviour
{
    public ObjectType objType;
    public Transform destMovement;

    private void OnMouseDown()
    {
        PlayerInstance.instance.ClickObject(gameObject);
    }

    public virtual void Interact(GameObject pickedObject) { }

    private void FixedUpdate()
    {
        if (destMovement != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, destMovement.position, 30f * Time.deltaTime);
            if (Vector3.Distance(transform.position, destMovement.position) < 0.5)
            {
                destMovement = null;
            }
        }
    }

}
