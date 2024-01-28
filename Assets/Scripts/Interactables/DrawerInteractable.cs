using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawerInteractable : InteractableObject
{
    // Start is called before the first frame update
    public TextMeshPro txt;
    public MoneySpawn coins;
    int counter = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject == null || pickedObject.GetComponent<InteractableObject>().objType != ObjectType.DINERO) return;
        PlayerInstance.instance.RemoveHandObject();
        GameManager.Instance.money += 1;
        counter++;
        txt.text = GameManager.Instance.money + "€";
        pickedObject.transform.parent = transform.GetChild(0);
        pickedObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        pickedObject.transform.localPosition = Vector3.zero;
        if(counter == coins.coins)
        {
            counter = 0;
            transform.parent.GetComponent<Register>().CloseDrawer(0);
        }
    }

    public void ActivateCoins()
    {
        coins.Activate();
    }
}
