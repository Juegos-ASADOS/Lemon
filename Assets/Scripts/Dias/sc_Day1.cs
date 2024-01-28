using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sc_Day1 : MonoBehaviour
{

    [SerializeField] private Cliente client;
    [SerializeField] private GameObject dialogos;

    public GameObject client_1 = null;
    public GameObject client_2 = null;
    public GameObject client_3 = null;

    //vamos a diseñar los dias mediante eventos, llevando la cuenta de estos, por ejemplo, cuando un cliente ha salido, eso solo lo podra hacer una unica vez
    int contador = 3;
    private void Awake()
    {
        Register.RegisterOpen += openShop;
        Cliente.ClientExit += nextClient;
    }

    private void Start()
    {
        dialogos.GetComponent<DialogueSystem>().startTutorial();
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
        client.nombre = "C1";
        client.importance = true;
        client.exitWay = Cliente.ExitType.moving;

        client_1?.SetActive(true);
        client.setEnter();
    }

   

    private IEnumerator EventClientTWO()
    {
        yield return new WaitForSeconds(5);
        client.nombre = "C2";
        client.importance = true;
        client.exitWay = Cliente.ExitType.moving;

        client_1?.SetActive(false);
        client_2?.SetActive(true);
        client.setEnter();
    }

    private IEnumerator EventClientThree()
    {
        yield return new WaitForSeconds(5);
        client.nombre = "C3";
        client.importance = true;
        client.exitWay = Cliente.ExitType.moving;
        client_2?.SetActive(false);
        client_3?.SetActive(true);
        client.setAppear();
    }
}
