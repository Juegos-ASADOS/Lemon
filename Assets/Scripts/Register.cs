using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Register : MonoBehaviour
{

    public static event Action RegisterOpen = delegate { };

    [SerializeField]
    uint code;

    public CameraWaypoint waypoint;
    FMOD_Manager fmodManager;

    string actCode = "";

    public TextMeshPro txt;
    // Start is called before the first frame update
    void Start()
    {
        fmodManager = FMOD_Manager.instance;
    }

    // Update is called once per frame
    private void Awake()
    {
        GameManager.EndOfDay += OpenDrawer;
    }
    public void Number(string n)
    {
        actCode += n;
        if (actCode.Length>4) actCode = n;

        txt.text = actCode;
    }

    public void Enter()
    {
        if(actCode.Length > 0  && code == int.Parse(actCode))
        {
            fmodManager.SetGlobalParameterByName("CashRegister", "Init");
            //SONIDO inicioCaja
            RegisterOpen();
        }
        else
        {
            fmodManager.SetGlobalParameterByName("CashRegister", "CodeError");
            fmodManager.PlaySingleInstanceEmitterControllerGroup("Abrir");
            //SONIDO errorCodigo
            actCode = "";
            txt.text = "0000";
        }
    }


   
    public void OpenDrawer(int drawer)
    {
        Camera.main.GetComponent<MovingCamera>().ForcedRotateTo(waypoint);
        //Camera.main.GetComponent<MovingCamera>().lockCamera();
        fmodManager.SetGlobalParameterByName("CashRegister", "Open");
        fmodManager.PlaySingleInstanceEmitterControllerGroup("Abrir");
        transform.GetChild(drawer + 2).GetComponent<Animator>().SetTrigger("Open");
        if (transform.GetChild(drawer + 2).GetComponent<DrawerInteractable>() != null)
            transform.GetChild(drawer + 2).GetComponent<DrawerInteractable>().enabled = true;
    }
    public void CloseDrawer(int drawer)
    {
        transform.GetChild(drawer + 2).GetComponent<Animator>().SetTrigger("Close");
        fmodManager.SetGlobalParameterByName("CashRegister", "Close");
        fmodManager.PlaySingleInstanceEmitterControllerGroup("Abrir");
        if (transform.GetChild(drawer + 2).GetComponent<DrawerInteractable>() != null)
            transform.GetChild(drawer + 2).GetComponent<DrawerInteractable>().enabled = false;
        GameManager.Instance.endDay();
    }
}
