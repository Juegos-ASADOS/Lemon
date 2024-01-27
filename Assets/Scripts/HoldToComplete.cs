using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldToComplete : MonoBehaviour
{
    [SerializeField]
    private float holdTime = 1.0f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && holdTime >= 0.0f)
        {
            holdTime -= Time.deltaTime;
            Debug.Log("Holding...");
        }
        if (holdTime <= 0.0f)
        {
            Debug.Log("Completed!");
        }
    }
}
