using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sc_Day1 : MonoBehaviour
{

    [SerializeField] private Cliente client;

    //vamos a dise�ar los dias mediante eventos, llevando la cuenta de estos, por ejemplo, cuando un cliente ha salido, eso solo lo podra hacer una unica vez
    int contador = 3;
    private void Awake()
    {
        Register.RegisterOpen += openShop;
        Cliente.ClientExit += nextClient;
    }

    void nextClient()
    {
        contador--;

        if (contador == 2)
        {
            StartCoroutine(EventClientTWO());
        }
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
        client.nombre = "Dani";
        client.importance = true;
        client.exitWay = Cliente.ExitType.moving;
        client.setEnter();
    }

    private IEnumerator EventClientTWO()
    {
        yield return new WaitForSeconds(5);
        client.nombre = "Jose";
        client.importance = true;
        client.exitWay = Cliente.ExitType.moving;

        client.setEnter();
    }

    private IEnumerator EventClientThree()
    {
        yield return new WaitForSeconds(5);
        client.nombre = "Lemonian";
        client.importance = true;
        client.exitWay = Cliente.ExitType.moving;

        client.setAppear();
    }
}