using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Type { LIMON, CRUASAN, MANZANA,TIMBRE }

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    Type type;

    private void OnMouseDown()
    {
        // Player.click(type)
        Interact();
    }

    protected virtual void Interact() { }
}
