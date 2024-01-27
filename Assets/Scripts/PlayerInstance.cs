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
        pickTransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickObject(GameObject obj)
    {
        InteractableObject interact = obj.GetComponent<InteractableObject>();
        if (interact == null)
            return;

        ObjectType t = interact.objType;
        if (pickedObject == null)
        {
            if (t == ObjectType.CUCHILLO || t == ObjectType.EXPRIMIDOR || (obj.transform.parent != null && t == ObjectType.FRUTA))
                pickedObject = obj;
            else if(t == ObjectType.FRUTA || t == ObjectType.BOLLO || t == ObjectType.VASO)
                pickedObject = Instantiate(obj);             
            else
                interact.Interact(null);

            if (pickedObject != null)
            {
                pickedObject.transform.position = pickTransform.position;
                pickedObject.transform.SetParent(transform);
            }
        }
        else
        {
            interact.Interact(pickedObject);
        }
    }

    public void DumpObject()
    {
        ObjectType t = pickedObject.GetComponent<InteractableObject>().objType;

        if (t == ObjectType.FRUTA || t == ObjectType.BOLLO || t == ObjectType.VASO)
            Destroy(pickedObject);
    }
    public void RemoveHandObject()
    {
        pickedObject = null;
    }
}
