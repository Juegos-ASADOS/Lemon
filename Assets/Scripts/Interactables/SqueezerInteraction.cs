using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public enum JuiceType { EMPTY, LEMON, ORANGE, GRAPEFRUIT, CAKE }
public class SqueezerInteraction : InteractableObject
{
    [SerializeField]
    private Image fillBar;
    [SerializeField]
    private Canvas bar;

    //[SerializeField]
    //private ParticleSystem finishParticles;

    private JuiceType juice = JuiceType.EMPTY;

    private Transform originalPos;
    Material mat;
    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        originalPos = transform.parent;
    }
    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject != null)
        {
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                if (pickedObject.GetComponent<FruitCharacteristics>().IsCut())
                {
                    PlayerInstance.instance.RemoveHandObject();
                    juice = pickedObject.GetComponent<FruitCharacteristics>().GetTypeFruit();
                    Destroy(pickedObject);
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
                    GameObject liquid = transform.GetChild(1).gameObject;
                    liquid.SetActive(true);
                    liquid.GetComponent<Renderer>().material.color = color;
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
        GameObject liquid = transform.GetChild(1).gameObject;
        liquid.SetActive(false);
        transform.parent = originalPos;
        transform.position = originalPos.GetChild(0).position;
        transform.rotation = originalPos.GetChild(0).rotation;
    }
}
