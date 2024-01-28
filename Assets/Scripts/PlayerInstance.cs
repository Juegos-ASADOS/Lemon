using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance instance;

    GameObject pickedObject;

    // La posici�n de los objetos al cogerlos
    Transform pickTransform;

    [SerializeField] float MoveSpeed = 30f;

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

    private void FixedUpdate()
    {
        if (pickedObject != null)
        {
            pickedObject.transform.position = Vector3.MoveTowards(pickedObject.transform.position, pickTransform.position, MoveSpeed * Time.deltaTime);
        }

    }

    public void ClickObject(GameObject obj)
    {
        InteractableObject interact = obj.GetComponent<InteractableObject>();
        if (interact == null)
            return;

        ObjectType t = interact.objType;
        if (pickedObject == null)
        {
            if (t == ObjectType.FRUTA || t == ObjectType.COMIDA || t == ObjectType.VASO || t == ObjectType.PLATO)
            {
                if(t == ObjectType.FRUTA && obj.transform.parent != null && obj.transform.parent.GetComponent<CutboardInteraction>() != null)
                    obj.transform.parent.GetComponent<BoxCollider>().enabled = true;
                if (obj.transform.parent != null && !obj.transform.parent.CompareTag("Cesta"))
                    pickedObject = obj;
                else
                {
                    pickedObject = Instantiate(obj);
                    pickedObject.transform.localScale = obj.transform.lossyScale;
                }
            }
            else if (t == ObjectType.CUCHILLO || t == ObjectType.EXPRIMIDOR || t == ObjectType.DINERO)
            {
                if(t == ObjectType.CUCHILLO)
                    GameObject.FindGameObjectWithTag("Cutboard").gameObject.transform.GetComponent<BoxCollider>().enabled = true;
                pickedObject = obj;
            }
            else
                interact.Interact(null);
            
            if (pickedObject != null)
            {
                FMOD_Manager.instance.PlaySingleInstanceEmitterControllerGroup("Grab");
                pickedObject.GetComponent<InteractableObject>().destMovement = pickTransform;
                pickedObject.transform.parent = transform;
                pickedObject.GetComponent<InteractableObject>().PickUp();
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

        if (t == ObjectType.FRUTA || t == ObjectType.COMIDA || t == ObjectType.VASO || t==ObjectType.PLATO)
            Destroy(pickedObject);
    }
    public void RemoveHandObject()
    {
        pickedObject = null;
    }
}
