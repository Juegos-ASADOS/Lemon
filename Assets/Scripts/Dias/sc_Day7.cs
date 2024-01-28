using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sc_Day7 : MonoBehaviour
{
    float timer = 0.0f;
    bool up = false;

    private void Start()
    {
        PlayerInstance.instance.GetCameraComponent().lockCamera();
    }

    private void Update()
    {
        if(timer < 3.0f)
            timer += Time.deltaTime;
        else
        {
            if (!up)
            {
                timer = 0.0f;
                PlayerInstance.instance.GetCameraComponent().rotateUp();
                up = true;
            }
            else
            {
                GameManager.Instance.endDay();
            }
        }
    }
}
