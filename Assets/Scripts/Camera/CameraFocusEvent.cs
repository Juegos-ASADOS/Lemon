using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "CameraEvent/CameraFocus")]
public class CameraFocusEvent : ScriptableObject
{
    [Min(0)]
    public float EnterTransitionTime;
    [Min(0)]
    public float LeaveTransitionTime;
    [Min(0.01f)]
    public float Zoom;
    [Range(0,1)]
    public float Weight = 1;
    public VolumeProfile PostProcessingProfile;
}
