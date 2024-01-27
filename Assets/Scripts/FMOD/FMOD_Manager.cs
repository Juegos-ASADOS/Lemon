using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

public class FMOD_Manager : MonoBehaviour
{
    public static FMOD_Manager instance { get; private set; }

    //HashMap that contains pair keys <String, List<FMOD_EventEmitter_Controller>> with all groups and EmiterControllers
    public Hashtable eventEmitterMap = new Hashtable();

    [Header("All FMOD_EventEmitterController in app")]
    [SerializeField] FMOD_EventEmitter_Controller[] controllers;

    [Header("All Global Variables from FMOD Proyect")]
    public List<string> globalVariableNames;

  
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
    }

    private void Start()
    {
        InitializateMap();
    }

    private void OnDestroy()
    {
        eventEmitterMap = null;
    }

    //  HASHMAP CONTROLLER  //

    //  Register EmiterController to Hashmap
    public void RegisterEventGroup(FMOD_EventEmitter_Controller eventEmiterController, string group)
    {
        if (!eventEmitterMap.ContainsKey(group))                                                                        //If group hasn't been asigned before
        {
            List<FMOD_EventEmitter_Controller> eventEmittersControllers = new List<FMOD_EventEmitter_Controller>();     //Create array of EmittersControllers in group
            eventEmittersControllers.Add(eventEmiterController);                                                        //Add this EmiterController to array
            eventEmitterMap.Add(group, eventEmittersControllers);                                                       //Add group to map
        }
        else                                                                                                            //If group has been asigned before
        {
            ((List<FMOD_EventEmitter_Controller>)eventEmitterMap[group]).Add(eventEmiterController);                    //Add this EmiterController to array
        }
    }

    // Initializate Hashmap
    void InitializateMap()
    {
        foreach (FMOD_EventEmitter_Controller controller in controllers)
        {
            foreach (string group in controller.groups)
            {
                RegisterEventGroup(controller, group);
            }
        }
    }

    //  PLAYER EVENTS CONTROLLER  //

    //  Play Group of Single Events Emitters Instance once
    public void PlaySingleInstanceEmitterControllerGroup(string groupName)
    {
        if (eventEmitterMap.Contains(groupName))
        {
            foreach (FMOD_EventEmitter_Controller emitterController in ((List<FMOD_EventEmitter_Controller>)eventEmitterMap[groupName]))
            {
                emitterController.PlaySingleInstance();
            }
        }
        else
        {
            Debug.Log("Missing Group Name");
        }
    }

    //  Stop Group Single Events Emitters Instance loosing all references and parameter values
    public void StopSingleInstanceEmitterControllerGroup(string groupName)
    {
        if (eventEmitterMap.Contains(groupName))
        {
            foreach (FMOD_EventEmitter_Controller emitterController in ((List<FMOD_EventEmitter_Controller>)eventEmitterMap[groupName]))
            {
                emitterController.StopSingleInstance();
            }
        }
        else
        {
            Debug.Log("Missing Group Name");
        }
    }

    //  Toggle Group of Single Events Emitters Instance to play and stop without loosing all references and parameter values
    public void ToggleSingleInstanceEmitterControllerGroup(string groupName)
    {
        if (eventEmitterMap.Contains(groupName))
        {
            foreach (FMOD_EventEmitter_Controller emitterController in ((List<FMOD_EventEmitter_Controller>)eventEmitterMap[groupName]))
            {
                emitterController.ToggleSingleInstance();
            }
        }
        else
        {
            Debug.Log("Missing Group Name");
        }
    }


    //  GLOBAL VARIABLE CONTROLLER  //

    //  Changes Global Parameter Value by Name
    //  Labeled values cannot be change by string value, instead we have to use (0, 1, 2...)
    public void SetGlobalParameterByName(string name, float value)
    {
        if (globalVariableNames.Contains(name))
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(name, value);
        else
            Debug.Log("Missing Global Variable name");
    }

}
