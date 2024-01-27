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
        if(code == int.Parse(actCode))
        {
            //SONIDO inicioCaja
            Debug.Log("Abrir");
        }
        else
        {
            //SONIDO errorCodigo
            Debug.Log("Error");
            actCode = "";
        }
    }
}
