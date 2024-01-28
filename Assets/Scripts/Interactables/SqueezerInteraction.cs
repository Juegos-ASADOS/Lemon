using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum JuiceType { EMPTY, LEMON, ORANGE, GRAPEFRUIT, CAKE }
public class SqueezerInteraction : InteractableObject
{
    [SerializeField]
    private float holdTime = 1.0f;

    //[SerializeField]
    //private ParticleSystem finishParticles;

    private float restingTime = 0.0f;
    private GameObject fruit;
    private JuiceType juice = JuiceType.EMPTY;

    private Transform originalPos;
    Material mat;
    private bool hover = false;
    private bool canHold = false;
    private void Start()
    {
        restingTime = holdTime;
        mat = GetComponent<Renderer>().material;
        originalPos = transform.parent;
    }
    void Update()
    {
        if (hover && canHold && Input.GetMouseButton(0) && restingTime >= 0.0f)
        {
            restingTime -= Time.deltaTime;
        }

        if (restingTime <= 0.0f)
        {
           // gameObject.GetComponent<Renderer>().material = fruit.GetComponent<Renderer>().material;
            PlayerInstance.instance.RemoveHandObject();
            Destroy(fruit);
            fruit = null;
            //finishParticles.transform.position = gameObject.transform.position;
            //finishParticles.Play();
            restingTime = holdTime;
        }
    }
    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject != null)
        {
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                fruit = pickedObject;
                if (fruit.GetComponent<FruitCharacteristics>().IsCut())
                {
                    juice = fruit.GetComponent<FruitCharacteristics>().GetTypeFruit();
                    canHold = true;
                }
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
        PlayerInstance.instance.RemoveHandObject();
        transform.parent = originalPos;
        transform.position = originalPos.GetChild(0).position;
    }

    void OnMouseOver() { hover = true; }
    void OnMouseExit() { hover = false; }
}
