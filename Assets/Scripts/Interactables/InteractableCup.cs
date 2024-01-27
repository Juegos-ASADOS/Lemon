using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCup : InteractableObject
{
    private JuiceType juice;

    public JuiceType GetJuice() { return juice; }
    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject != null && transform.parent != null)
        {
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.EXPRIMIDOR)
            {
                juice = pickedObject.GetComponent<SqueezerInteraction>().GetJuice();
                gameObject.GetComponent<Renderer>().material = pickedObject.GetComponent<Renderer>().material;
                pickedObject.GetComponent<SqueezerInteraction>().RemoveJuice();
                Debug.Log(juice.ToString());
            }
        }
    }
}
