using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum JuiceType { EMPTY, LEMON, ORANGE, GRAPEFRUIT }
public class SqueezerInteraction : InteractableObject
{
    [SerializeField]
    private float holdTime = 1.0f;

    private GameObject fruit;
    private JuiceType juice = JuiceType.EMPTY;

    private bool hover = false;
    private bool canHold = false;
    private bool completed = false;

    void Update()
    {
        if (hover && canHold && Input.GetMouseButton(0) && holdTime >= 0.0f)
        {
            holdTime -= Time.deltaTime;
        }
        if (holdTime <= 0.0f && !completed)
        {
            Debug.Log(tag + " Completed!");
            completed = true;
            ///TODO: acción de clic completada
           gameObject.GetComponent<Renderer>().material = fruit.GetComponent<Renderer>().material;
            PlayerInstance.instance.RemoveHandObject();
            Destroy(fruit);
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
                canHold = true;
                fruit = pickedObject;
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

    void OnMouseOver() { hover = true; }
    void OnMouseExit() { hover = false; }
}
