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
                if (juice != JuiceType.EMPTY)
                {
                    Color color;
                    switch (juice)
                    {
                        case JuiceType.LEMON:
                            color = Color.yellow;
                            break;
                        case JuiceType.ORANGE:
                            color = new Color(0.98f, 0.62f, 0.32f, 1.0f);
                            break;
                        case JuiceType.GRAPEFRUIT:
                            color = Color.red; break;
                        default:
                            color = Color.white;
                            break;
                    }
                    GameObject liquid = transform.GetChild(0).gameObject;
                    liquid.SetActive(true);
                    liquid.GetComponent<Renderer>().material.color = color;
                    pickedObject.GetComponent<SqueezerInteraction>().RemoveJuice();
                    //Debug.Log(juice.ToString());
                }
                //gameObject.GetComponent<Renderer>().material = pickedObject.GetComponent<Renderer>().material;
            }
        }
    }
}
