using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliente : MonoBehaviour
{
    //Eventos a los que se les puede añadir informacion
    public static event Action<bool, string> ClientEnter = delegate { };
    public static event Action ClientExit = delegate { };
    public static event Action ClientReaddy = delegate { };
    public static event Action<bool, string> ClientSatisfiedEvent = delegate { };

    public static event Action<bool, string> ClientSeen = delegate { };
    public enum Intention { ENTER, EXIT, APPEAR, DISAPPEAR, STAY, READY }
    public enum ExitType { moving, teleport }

    [SerializeField] Vector3 counterPos;
    [SerializeField] Vector3 OutOfSightPos;

    [SerializeField] float MoveSpeed;
    [SerializeField] float aceptableDistance = 0.5f;

    [SerializeField] string nombre;
    [SerializeField] bool importance = false;
    [SerializeField] ExitType exitWay;

    //provisional cambiar segun situacion
    public Intention intention;//= Intention.ENTER;

    private Vector3 destino;
    private bool seen;

    public bool moving = false;
    public bool teleport = false;

    private void Awake()
    {
        //provisional
        destino = counterPos;
        //moving = true;
        //teleport = true;
        //

        TrayInteraction.GiveOrder += CheckOrder;
        ClientCounterPosition.CounterOutOfSight += enterTeleport;

        DirectorClients.ClientEnter += setEnter;
        DirectorClients.ClientExit += setExit;
        DirectorClients.ClientAppear += setAppear;
        DirectorClients.ClientDisappear += setDisAppear;

        DialogueSystem.EndDespedidaEvent += setExitWay;

    }

    void setExitWay()
    {

        Debug.Log("se ejecuta el exitway");
        if (exitWay == ExitType.moving)
        {
            setExit();
        }
        else
        {
            setDisAppear();
        }
    }

    //Debug
    void setEnter()
    {
        teleport = false;
        moving = true;
        destino = counterPos;
        intention = Intention.ENTER;
    }
    void setExit()
    {
        teleport = false;
        moving = true;
        destino = OutOfSightPos;
        intention = Intention.EXIT;

    }
    void setAppear()
    {
        moving = false;
        teleport = true;
        destino = counterPos;
        intention = Intention.APPEAR;

    }
    void setDisAppear()
    {
        moving = false;
        teleport = true;
        destino = OutOfSightPos;
        intention = Intention.DISAPPEAR;

    }
    //
    private void FixedUpdate()
    {
        if (moving)
        {
                transform.position = Vector3.MoveTowards(transform.position, destino, MoveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, destino) <= aceptableDistance)
            {
                moving = false;
                if (intention == Intention.ENTER || intention == Intention.APPEAR)
                {
                    //esto se puede llamar desde un evento controlado
                    enterScene();
                    intention = Intention.READY;
                    if (seen) {
                        onSight();
                    }
                }
                else if(intention == Intention.EXIT || intention == Intention.DISAPPEAR)
                {
                    //esto se puede llamar desde un evento controlado
                    exitScene();
                    intention = Intention.STAY;
                }
            }
        }
    }


    private void CheckOrder(comandas com)
    {

        //moving = true;
        Debug.Log("Pedido recibidio y procesado");
        //eventos de conseguir o fallar pedido

        if (com == comandas.Empty_Cup)
        {
            //vaso vacio
            return;
        }
        if (com == comandas.Empty_Plate)
        {
            //plato vacio
            return;
        }
        if (com == comandas.No_Tray)
        {
            //bandeja vacia
            return;
        }

        if (com == ComandasClientes.Instance.GetCommandByName(nombre))
        {
            //success

            ClientSatisfiedEvent(importance, nombre);

            return;
        }
        


    }
    void OnBecameVisible()
    {
        //cuando lo estemos mirando
        onSight();
        seen = true;
    }
    void onSight()
    {
     
        if (intention == Intention.READY)
        {
            ClientEnter(importance, nombre); //evento de cliente entrado
            intention = Intention.STAY;
        }
    }
    void OnBecameInvisible()
    {
        //cuando dejamos de mirar al cliente
        seen = false;
        onOutOffSight();
    }
    void onOutOffSight()
    {
        if (intention == Intention.DISAPPEAR)
        {
            dissappearInScene();
            ClientExit();
        }
    }

    public void enterTeleport()
    {
        //Debug.Log("se teletrasporta");

        if (intention == Intention.APPEAR)
        {
            appearInScene();
            ClientEnter(importance, nombre);
            intention = Intention.STAY;
        }
    }
    public void teleportToDest()
    {
        if (teleport)
        {
            transform.position = destino;
        }
    }

    void enterScene()
    {
        //Go to CounterPos
        ClientReaddy();
        destino = OutOfSightPos;
    }

    void exitScene()
    {
        //Go to OutOfSight
        ClientExit(); //evento de cliente ha salido
    }

    void appearInScene()
    {
        //Teleport to CounterPos
        this.GetComponent<Transform>().position = counterPos;
        teleport = false;
    }

    void dissappearInScene()
    {
        //Telport to OutOfSight
        this.GetComponent<Transform>().position = OutOfSightPos;
        teleport = false;
    }
}
