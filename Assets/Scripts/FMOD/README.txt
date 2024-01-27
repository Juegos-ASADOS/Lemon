using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField]
    public StudioEventEmitter actionTest;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        fmodManager = FMOD_Manager.instance;
        if (!fmodManager) Debug.Log("fmodManager is null");
    }

    // Cambio de escenas por la putisima cara
    public void CambiarEscena(string nuevaEscena)
    {
        // Verifica si la escena con el nombre especificado existe
        if (SceneManager.GetSceneByName(nuevaEscena) != null)
        {
            // Cambia a la nueva escena
            SceneManager.LoadScene(nuevaEscena);
        }
        else
        {
            Debug.LogError("Error: La escena '" + nuevaEscena + "' no fue encontrada.");
        }
    }

    //FMOD
    FMOD_Manager fmodManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Presionaste la tecla 1");
            fmodManager.PlaySingleInstanceEmitterControllerGroup("2DAction");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Presionaste la tecla 2");
            fmodManager.PlaySingleInstanceEmitterControllerGroup("3DAction");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Presionaste la tecla 3");
            fmodManager.PlaySingleInstanceEmitterControllerGroup("2DActionTimeline");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Presionaste la tecla 4");
            fmodManager.PlaySingleInstanceEmitterControllerGroup("3DActionTimeline");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Presionaste la tecla Q");
            fmodManager.StopSingleInstanceEmitterControllerGroup("2DAction");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Presionaste la tecla W");
            fmodManager.StopSingleInstanceEmitterControllerGroup("3DAction");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Presionaste la tecla E");
            fmodManager.StopSingleInstanceEmitterControllerGroup("2DActionTimeline");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Presionaste la tecla R");
            fmodManager.StopSingleInstanceEmitterControllerGroup("3DActionTimeline");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Presionaste la tecla Z");
            fmodManager.ToggleSingleInstanceEmitterControllerGroup("2DAction");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Presionaste la tecla X");
            fmodManager.ToggleSingleInstanceEmitterControllerGroup("3DAction");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Presionaste la tecla C");
            fmodManager.ToggleSingleInstanceEmitterControllerGroup("2DActionTimeline");
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("Presionaste la tecla V");
            fmodManager.ToggleSingleInstanceEmitterControllerGroup("3DActionTimeline");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Presionaste la tecla B");
            fmodManager.SetGlobalParameterByName("Global States", 0);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("Presionaste la tecla N");
            fmodManager.SetGlobalParameterByName("Global States", 1);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Presionaste la tecla M");
            fmodManager.SetGlobalParameterByName("Global States", 2);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Destroy(fmodManager);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            actionTest.Play();
        }


    }
}
