using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeInteraction : InteractableObject
{
    private Transform originalPos;

    private void Start()
    {
        originalPos = transform.parent;
    }

    public void CutEnd()
    {
        PlayerInstance.instance.RemoveHandObject();
        transform.parent = originalPos;
        transform.position = originalPos.GetChild(0).position;
    }
}
