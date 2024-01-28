using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawerInteractable : InteractableObject
{
    // Start is called before the first frame update
    public TextMeshPro txt;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject.GetComponent<InteractableObject>().objType != ObjectType.DINERO) return;
        if (pickedObject != null)
            PlayerInstance.instance.RemoveHandObject();
        GameManager.Instance.money += 1;
        txt.text = GameManager.Instance.money + "€";
        pickedObject.transform.parent = transform.GetChild(0);
        pickedObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        pickedObject.transform.localPosition = Vector3.zero;
    }
}
