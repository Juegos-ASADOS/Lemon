using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMOD_UI_Bus_Volume : MonoBehaviour
{

    [Header("FMOD Mixer Bus Path ID")]
    private readonly string[] busOptions = { "bus:/", "bus:/SFX", "bus:/Music" };   //  Todos los paths de los buses del proyecto de FMOD
    [SerializeField] private int busOptionIndex;                        //  ID del path asociado a la posicion del string de arriba
    FMOD.Studio.Bus bus;


    private void Start()
    {
        if (busOptionIndex != -1)
            bus = FMODUnity.RuntimeManager.GetBus(busOptions[busOptionIndex]);
        else
            Debug.Log("Missing Bus Path");
    }

    public void setBusVolume(float sliderValue)
    {
        // Establecer el volumen del bus
        bus.setVolume(sliderValue);
    }
}
