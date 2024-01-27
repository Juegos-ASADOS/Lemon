using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum JuiceType { EMPTY, LEMON, ORANGE, GRAPEFRUIT }
public class SqueezerInteraction : InteractableObject
{
    [SerializeField]
    private float holdTime = 1.0f;
    private float restingTime = 0.0f;
    private GameObject fruit;
    private JuiceType juice = JuiceType.EMPTY;
    Material mat;
    private bool hover = false;
    private bool canHold = false;
    private bool completed = false;
    private void Start()
    {
        restingTime = holdTime;
        mat = GetComponent<Renderer>().material;
    }
    void Update()
    {
        if (hover && canHold && Input.GetMouseButton(0) && restingTime >= 0.0f)
        {
            if (completed) completed = false;
            restingTime -= Time.deltaTime;
        }
        
        if (restingTime <= 0.0f)
        {
            completed = true;            
            gameObject.GetComponent<Renderer>().material = fruit.GetComponent<Renderer>().material;
            PlayerInstance.instance.RemoveHandObject();
            Destroy(fruit);
            fruit = null;
            restingTime = holdTime;
            Debug.Log("Tiempo restante: "+restingTime);
        }
    }
    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject != null)
        {
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                fruit = pickedObject;
                Debug.Log(pickedObject.name);
                switch (pickedObject.tag)
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
        gameObject.GetComponent<Renderer>().material = mat;
    }

    void OnMouseOver() { hover = true; }
    void OnMouseExit() { hover = false; }
}
