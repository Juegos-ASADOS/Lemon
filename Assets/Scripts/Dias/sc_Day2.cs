using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sc_Day2 : MonoBehaviour
{

    [SerializeField] private Cliente client;

    //vamos a diseñar los dias mediante eventos, llevando la cuenta de estos, por ejemplo, cuando un cliente ha salido, eso solo lo podra hacer una unica vez
    int contador = 4;
    private void Awake()
    {
        Register.RegisterOpen += openShop;
        Cliente.ClientExit += nextClient;
    }

    void nextClient()
    {
        contador--;

        if (contador == 3)
        {
            StartCoroutine(EventClientTWO());
        }
        if (contador == 2)
        {
            StartCoroutine(EventClientThree());
        }
        if (contador == 1)
        {
            StartCoroutine(EventClientFour());
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
    }

    private IEnumerator EventClientTWO()
    {
        yield return new WaitForSeconds(5);
        client.nombre = "C2";
        client.importance = true;
        client.exitWay = Cliente.ExitType.moving;

        client.setEnter();
    }

    private IEnumerator EventClientThree()
    {
        yield return new WaitForSeconds(5);
        client.nombre = "C3";
        client.importance = true;
        client.exitWay = Cliente.ExitType.moving;

        client.setAppear();
    }

    private IEnumerator EventClientFour()
    {
        yield return new WaitForSeconds(5);
        client.nombre = "C4";
        client.importance = true;
        client.exitWay = Cliente.ExitType.moving;

        client.setAppear();
    }
}
