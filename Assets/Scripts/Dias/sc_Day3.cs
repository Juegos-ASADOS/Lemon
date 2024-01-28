using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sc_Day3 : MonoBehaviour
{

    [SerializeField] private Cliente client;
    public GameObject client_1 = null;
    public GameObject client_2 = null;
    public GameObject gorro;

    //vamos a dise�ar los dias mediante eventos, llevando la cuenta de estos, por ejemplo, cuando un cliente ha salido, eso solo lo podra hacer una unica vez
    int contador = 2;
    private void Awake()
    {
        Register.RegisterOpen += openShop;
        Cliente.ClientExit += nextClient;
    }

    void nextClient()
    {
        contador--;

        if (contador == 1)
        {
            StartCoroutine(EventClientThree());
        }
        if (contador == 0)
        {
            //final del dia
            GameManager.Instance.finishDay();
        }
    }

    void openShop()
    {
        StartCoroutine(EventClientOne()) ;
    }

    private IEnumerator EventClientOne()
    {
        yield return new WaitForSeconds(5);
        client.nombre = "C1";
        client.importance = true;
        client.exitWay = Cliente.ExitType.moving;
        client.setEnter();
        client_1?.SetActive(true);
    }

    private IEnumerator EventClientThree()
    {
        yield return new WaitForSeconds(0);
        client.nombre = "C2";
        client.importance = true;
        client.exitWay = Cliente.ExitType.moving;
        client_1?.SetActive(false);
        client_2?.SetActive(true);

        client.setEnter();
        PlayerInstance.instance.GetCameraComponent().rotateToCounter();
        Transform t = GameObject.FindGameObjectWithTag("Punto").transform;
        Instantiate(gorro, t).GetComponent<LemonCap>().SetClient(client);
    }
}
