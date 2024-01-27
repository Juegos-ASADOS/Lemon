using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    [SerializeField] private AnimationCurve movementCurve;

    [SerializeField] private float animationDuration = .5f;
    private bool cameraMoving = false;
    private Vector3 movingAxis;
    [SerializeField] private CameraWaypoint actualWaypoint = null;

    private void Start()
    {
        StartCoroutine(RotateTo(Vector3.zero));
    }

    private void Update()
    {
        if (cameraMoving || actualWaypoint == null) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            movingAxis = Vector3.down;
            StartCoroutine(RotateTo(Vector3.left));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            movingAxis = Vector3.up;
            StartCoroutine(RotateTo(Vector3.right));
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            movingAxis = Vector3.left;
            StartCoroutine(RotateTo(Vector3.up));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            movingAxis = Vector3.right;
            StartCoroutine(RotateTo(Vector3.down));
        }
    }

    private IEnumerator RotateTo(Vector3 dir)
    {
        if (cameraMoving || actualWaypoint == null)
            yield break;

        actualWaypoint = actualWaypoint.ProcessMovement(dir);

        cameraMoving = true;

        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = actualWaypoint.transform.rotation;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = actualWaypoint.transform.position;
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;

            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, movementCurve.Evaluate(t));
            transform.position = Vector3.Slerp(startPosition, targetPosition, movementCurve.Evaluate(t));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        transform.position = targetPosition;

        cameraMoving = false;
    }
}
