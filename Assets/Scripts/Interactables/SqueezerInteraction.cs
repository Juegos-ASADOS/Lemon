using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum JuiceType { EMPTY, LEMON, ORANGE, GRAPEFRUIT }
public class SqueezerInteraction : InteractableObject
{
    private GameObject objectContained;
    private JuiceType juice = JuiceType.EMPTY;
    void Update()
    {
        if (objectContained != null)
        {

        }
    }
    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject != null)
        {
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                switch (pickedObject.name)
                {
                    case "Orange":
                        juice = JuiceType.ORANGE;
                        break;
                    case "Grapefruit":
                        juice = JuiceType.GRAPEFRUIT;
                        break;
                    case "Lemon":
                        juice = JuiceType.LEMON;
                        break;
                        default: break;
                }
                GetComponent<HoldToComplete>().ChangeHold(true);
            }
        }
    }
    public JuiceType GetJuice()
    {
        return juice;
    }
    public void RemoveJuice()
    {
        juice = JuiceType.EMPTY;
    }
}
