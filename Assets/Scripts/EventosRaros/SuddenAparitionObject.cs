using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenAparitionObject : MonoBehaviour
{

    private void OnEnable()
    {
        SwitchObjects();
    }

    private void OnBecameInvisible()
    {
        SwitchObjects();
    }

    private void SwitchObjects()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(!child.activeSelf);
        }
    }
}
