using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public struct NamedCameraEvents
{
    public string Name;
    public CameraFocusEvent CEvent;
}

[RequireComponent(typeof(Volume))]
public class CameraEvent : MonoBehaviour
{
    public NamedCameraEvents[] events;

    private Camera cam;
    private Volume cameraVolume;
    private bool eventInProcess = false;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cameraVolume = GetComponent<Volume>();
    }

    public bool CallEvent(string name)
    {
        if (eventInProcess) return false;

        CameraFocusEvent CEvent;

        if(TryGetEventValue(name, out CEvent))
        {
            StartCoroutine(ProcessEvent(CEvent));
            return true;
        }

        Debug.Log("Did not find event: " + name);
        return false;
    }

    public bool RemoveEvent()
    {
        if (!eventInProcess) return false;
        eventInProcess = false;
        return true;
    }

    private IEnumerator ProcessEvent(CameraFocusEvent CEvent)
    {
        eventInProcess = true;

        float camFov = cam.fieldOfView;
        cameraVolume.weight = 0;
        cameraVolume.profile = CEvent.PostProcessingProfile;

        float elapsedTime = 0;
        while(elapsedTime < CEvent.EnterTransitionTime)
        {
            cameraVolume.weight = Mathf.Lerp(0, CEvent.Weight, elapsedTime / CEvent.EnterTransitionTime);
            cam.fieldOfView = Mathf.Lerp(camFov, camFov*CEvent.Zoom, elapsedTime / CEvent.EnterTransitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraVolume.weight = CEvent.Weight;
        cam.fieldOfView = camFov * CEvent.Zoom;
        
        while(eventInProcess) 
            yield return null;

        camFov = cam.fieldOfView;
        elapsedTime = 0;
        while (elapsedTime < CEvent.LeaveTransitionTime)
        {
            cameraVolume.weight = Mathf.Lerp(CEvent.Weight, 0, elapsedTime / CEvent.LeaveTransitionTime);
            cam.fieldOfView = Mathf.Lerp(camFov, camFov / CEvent.Zoom, elapsedTime / CEvent.LeaveTransitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraVolume.weight = 0;
        cam.fieldOfView = camFov / CEvent.Zoom;
        cameraVolume.profile = null;
    }

    private bool TryGetEventValue(string name, out CameraFocusEvent CamEvent)
    {
        foreach (var NamedCEvent in events)
        {
            if (NamedCEvent.Name == name)
            {
                CamEvent = NamedCEvent.CEvent;
                return true;
            }
        }
        CamEvent = null;
        return false;
    }
}
