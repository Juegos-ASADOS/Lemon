using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

[System.Serializable]
public struct WaypointInfo
{
    public Vector3 direction;
    public CameraWaypoint waypoint;
}

public class CameraWaypoint : MonoBehaviour
{
    [SerializeField] WaypointInfo[] waypoints;

    public CameraWaypoint ProcessMovement(Vector3 dir)
    {
        CameraWaypoint waypoint = null;
        TryGetWaypointValue(dir, out waypoint);
        return waypoint;
    }

    private bool TryGetWaypointValue(Vector3 dir, out CameraWaypoint waypoint)
    {
        foreach (var info in waypoints)
        {
            if (info.direction == dir)
            {
                waypoint = info.waypoint;
                return true;
            }
        }

        waypoint = this;
        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, .2f);
        Gizmos.color = Color.yellow;
        
        Handles.DrawLine(transform.position, transform.position + transform.forward, 2.5f);
    }
#endif
}
