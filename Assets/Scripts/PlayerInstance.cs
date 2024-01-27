using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance instance;

    GameObject pickedObject;

    // La posición de los objetos al cogerlos
    Transform pickTransform;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ClickObject(GameObject obj)
    {
        InteractableObject interact = obj.GetComponent<InteractableObject>();
        if (interact == null)
            return;

        ObjectType t = interact.objType;
        if (pickedObject == null)
        {
            if (t == ObjectType.FRUTA || t == ObjectType.BOLLO || t == ObjectType.VASO)
                pickedObject = Instantiate(obj);
            else if (t != ObjectType.CAJA)
                pickedObject = obj;
            else
                interact.Interact(null);

            pickedObject.transform.position = pickTransform.position;
        }
        else
        {
            interact.Interact(pickedObject);
        }
    }
}
