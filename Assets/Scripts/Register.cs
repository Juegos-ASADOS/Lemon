using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : MonoBehaviour
{
    [SerializeField]
    uint code;

    string actCode = "";
    // Start is called before the first frame update
    void Start()
    {
        
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
            //SONIDO inicioCaja
            OpenDrawer(0);
            Debug.Log("Abrir");
        }
        else
        {
            //SONIDO errorCodigo
            CloseDrawer(0);
            Debug.Log("Error");
            actCode = "";
        }
    }

    public void OpenDrawer(int drawer)
    {
        transform.GetChild(drawer + 2).GetComponent<Animator>().SetTrigger("Open");
    }
    public void CloseDrawer(int drawer)
    {
        transform.GetChild(drawer + 2).GetComponent<Animator>().SetTrigger("Close");
    }
}
