using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCamera : MonoBehaviour
{
    [SerializeField] private AnimationCurve movementCurve;

    [SerializeField] private float rotatingAngles = 90;
    [SerializeField] private float animationDuration = .5f;
    private bool cameraMoving = false;
    private Vector3 movingAxis;

    private void Update()
    {
        if (cameraMoving) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            movingAxis = Vector3.down;
            StartCoroutine(RotateTo());
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            movingAxis = Vector3.up;
            StartCoroutine(RotateTo());
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            movingAxis = Vector3.left;
            StartCoroutine(RotateTo());
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            movingAxis = Vector3.right;
            StartCoroutine(RotateTo());
        }
    }

    private IEnumerator RotateTo()
    {
        if (cameraMoving)
            yield break;

        cameraMoving = true;

        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + movingAxis * rotatingAngles);
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, movementCurve.Evaluate(t));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;

        cameraMoving = false;
    }
}
