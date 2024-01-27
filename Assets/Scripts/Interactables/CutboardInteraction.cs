using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutboardInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject objectContained;


    void Start()
    {

    }

    void Update()
    {
        if (objectContained != null)
        {
            if (objectContained.GetComponent<InteractableObject>().objType == ObjectType.FRUTA)
            {
                GetComponent<HoldToComplete>().changeHold(true);
            }
        }
    }
}
