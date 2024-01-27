using System.Collections;
using System.Collections.Generic;
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
        Gizmos.DrawRay(transform.position, transform.forward);
    }
#endif
}
