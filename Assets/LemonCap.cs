using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonCap : MonoBehaviour
{
    private void Start()
    {
        PlayerInstance.instance.GetCameraComponent().lockCamera(); 
    }

    private void OnMouseDown()
    {
        DialogueSystem.EndDialogueEvent += PlayerInstance.instance.GetCameraComponent().unLockCamera;
        Destroy(gameObject);
    }
}
