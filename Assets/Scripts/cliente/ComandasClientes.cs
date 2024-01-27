using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComandasClientes : MonoBehaviour
{
    [SerializeField]
    private List<characterCommand> personajes;

    [System.Serializable]
    struct characterCommand
    {
        public string name;
        public comandas comanda;
    }


    public static ComandasClientes Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public comandas GetCommandByName(string clientName)
    {
        // Get characterEvent
        bool found = false;
        short i = 0;
        while (!found && i < personajes.Count)
        {
            if (personajes[i].name == clientName)
                break;
            i++;
        }
        if (!found)
        {

        }
        //return personajes[i].comanda;
            return comandas.Error; //wtf why? ajaja el nombre estaba mal
    }
}
