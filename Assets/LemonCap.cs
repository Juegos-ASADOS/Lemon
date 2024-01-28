using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonCap : MonoBehaviour
{
    Cliente client;

    private void OnMouseDown()
    {
        PlayerInstance.instance.GetCameraComponent().unLockCamera();
        client.Contentillo();
        Destroy(gameObject);
    }

    public void SetClient(Cliente c)
    {
        client = c;
    }
}
