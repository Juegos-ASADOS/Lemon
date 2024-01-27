using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FMOD_EventEmitter_Controller : MonoBehaviour
{
    //  Fmod Events can create multiple instances to play simultaneously times

    [Header("StudioEventEmitter")]
    [SerializeField] private StudioEventEmitter eventEmitter_;

    //  Controls wether audio is playing
    private bool paused = false;

    //  Groups that share variables with inside FMOD Proyect
    [Header("EventEmitterGroups")]
    [SerializeField] public string[] groups;

    private void Start()
    {
        if (eventEmitter_ == null)
            Debug.Log("StudioEventEmitter missing");

    }
    private void OnDestroy()
    {
        eventEmitter_.Stop();
    }

    //  Play Single Event Emitter Instance once
    public void PlaySingleInstance()
    {
        eventEmitter_.Play();
    }

    //  Stop Single Event Emitter Instance loosing all references and parameter values
    public void StopSingleInstance()
    {
        eventEmitter_.Stop();
    }

    //  Toggle Single Event Emitter Instance to play and stop without loosing all references and parameter values
    public void ToggleSingleInstance()
    {
        if (eventEmitter_.IsPlaying() && !paused)
        {
            eventEmitter_.EventInstance.setPaused(true);
            paused = true;
        }
        else
        {
            eventEmitter_.EventInstance.setPaused(false);
            paused = false;
        }
    }


    ////  Changes Global Parameter Value by Name
    public void SetParameterByName(string name, float value)
    {
        eventEmitter_.EventInstance.setParameterByName(name, value);
    }
    ////  Labeled values cannot be change by string value, instead we have to use (0, 1, 2...)
    public void SetParameterByNameWithLabel(string name, string label)
    {
        eventEmitter_.EventInstance.setParameterByNameWithLabel(name, label);
    }

    ////  THIS METHOD IS NOW INSIDE FMOD_Manager.cs
    //public void setGlobalParameterByName(string name, float value)
    //{
    //    if (globalVariableNames.Contains(name))
    //        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(name, value);
    //    else
    //        Debug.Log("Missing Global Variable name");
    //}
}