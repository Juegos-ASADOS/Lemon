using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCup : InteractableObject
{
    private JuiceType juice;

    public JuiceType GetJuice() { return juice; }
    public override void Interact(GameObject pickedObject)
    {
        if(pickedObject != null)
        {
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                juice = pickedObject.GetComponent<SqueezerInteraction>().GetJuice();

            }
        }
    }
}
