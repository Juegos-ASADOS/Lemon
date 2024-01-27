using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : MonoBehaviour
{
    [SerializeField]
    uint code;

    FMOD_Manager fmodManager;

    string actCode = "";
    // Start is called before the first frame update
    void Start()
    {
        fmodManager = FMOD_Manager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Number(string n)
    {
        actCode += n;
        if (actCode.Length>4) actCode = n;

        Debug.Log(actCode);
    }

    public void Enter()
    {
        if(actCode.Length > 0  && code == int.Parse(actCode))
        {
            fmodManager.SetGlobalParameterByName("CashRegister", 0);
            //SONIDO inicioCaja
            OpenDrawer(0);
            Debug.Log("Abrir");
            fmodManager.PlaySingleInstanceEmitterControllerGroup("CashRegister");

        }
        else
        {
            fmodManager.SetGlobalParameterByName("CashRegister", 1);
            //SONIDO errorCodigo
            Debug.Log("Error");
            actCode = "";
        }
    }

    public void OpenDrawer(int drawer)
    {
        transform.GetChild(drawer + 2).GetComponent<Animator>().SetTrigger("Open");
        if (transform.GetChild(drawer + 2).GetComponent<DrawerInteractable>() != null)
            transform.GetChild(drawer + 2).GetComponent<DrawerInteractable>().enabled = true;
    }
    public void CloseDrawer(int drawer)
    {
        transform.GetChild(drawer + 2).GetComponent<Animator>().SetTrigger("Close");
        if (transform.GetChild(drawer + 2).GetComponent<DrawerInteractable>() != null)
            transform.GetChild(drawer + 2).GetComponent<DrawerInteractable>().enabled = false;
    }
}
