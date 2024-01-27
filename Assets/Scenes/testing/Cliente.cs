using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliente : MonoBehaviour
{
    //Eventos a los que se les puede añadir informacion
    public static event Action ClientEnter = delegate { };
    public static event Action ClientExit = delegate { };
    public enum Intention { ENTER, EXIT, APPEAR, DISAPPEAR, STAY, OTHER }

    [SerializeField] Vector3 counterPos;
    [SerializeField] Vector3 OutOfSightPos;

    [SerializeField] float MoveSpeed;
    [SerializeField] float aceptableDistance = 0.5f;

    //provisional cambiar segun situacion
    public Intention intention = Intention.ENTER;

    private Vector3 destino;
    public bool moving = false;
    public bool teleport = false;

    private void Awake()
    {
        //provisional
        destino = counterPos;
        moving = true;
        //teleport = true;
        //

        InteractableTimbre.SendOrder += CheckOrder;
        InteractableTimbre.CounterOutOfSight += enterTeleport;

    }


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
                }
                else if(intention == Intention.EXIT || intention == Intention.DISAPPEAR)
                {
                    //esto se puede llamar desde un evento controlado
                    exitScene();
                }
                intention = Intention.STAY;
            }
        }
    }


    private void CheckOrder(items i)
    {

        moving = true;
        Debug.Log("Pedido recibidio y procesado");
        //eventos de conseguir o fallar pedido

    }
    void OnBecameVisible()
    {
        //cuando lo estemos mirando
        onSight();
    }
    void onSight()
    {
        //codigo para cuando miremos al cliente,

        //Ejemplo, lanzar texto
    }
    void OnBecameInvisible()
    {
        //cuando dejamos de mirar al cliente
        onOutOffSight();
    }
    void onOutOffSight()
    {
        teleportToDest();
    }

    public void enterTeleport()
    {
        Debug.Log("se teletrasporta");

        if (intention == Intention.APPEAR)
            teleportToDest();
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
        ClientEnter(); //evento de cliente entrado
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
    }

    void dissappearInScene()
    {
        //Telport to OutOfSight
        this.GetComponent<Transform>().position = OutOfSightPos;
    }
}
