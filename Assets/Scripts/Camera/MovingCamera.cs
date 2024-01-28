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
    [SerializeField] private CameraWaypoint CounterWaypoint = null;
    [SerializeField] private CameraWaypoint UpWaypoint = null;

    private CameraEvent cameraEvent;
    private bool cameraLock = false;

    private void Awake()
    {
        DialogueSystem.ImportantClientEvent += rotateToCounter;
        DialogueSystem.EndDialogueEvent += unLockCamera;
    }

    public void unLockCamera()
    {
        //it will unlock everytime a dialogue is ended, it will not end with limoncio tho, he is eternal!
        cameraLock = false;
    }

    public void lockCamera()
    {
        cameraLock = true;
    }
    public void rotateToCounter()
    {
        cameraLock = true;
        StartCoroutine(rotateImportant());
    }

    public void rotateUp()
    {
        cameraLock = true;
        StartCoroutine(lookUp());
    }

    private IEnumerator lookUp()
    {
        //por si ya se estaba moviendo el mingui que no se queden bloqueados o se cancelen
        while (cameraMoving)
            yield return null;

        actualWaypoint = UpWaypoint;

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

    private IEnumerator rotateImportant()
    {
        //por si ya se estaba moviendo el mingui que no se queden bloqueados o se cancelen
        while (cameraMoving)
            yield return null;

        actualWaypoint = CounterWaypoint;

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

    private void Start()
    {
        cameraEvent = GetComponent<CameraEvent>();
        // Ve al primer Waypoint (Se pasa 0.0.0 para evitar que cambie de waypoint)
        StartCoroutine(RotateTo(Vector3.zero));
    }

    private void Update()
    {
        if (cameraLock || cameraMoving || actualWaypoint == null) return;

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
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            cameraEvent.CallEvent("PeopleTalking");
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            cameraEvent.RemoveEvent();
        }
    }

    private IEnumerator RotateTo(Vector3 dir)
    {
        if (cameraLock || cameraMoving || actualWaypoint == null)
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

    public void movetocamera(CameraWaypoint waypoint)
    {
        StartCoroutine(ForcedRotateTo(waypoint, true));
    }

    public IEnumerator ForcedRotateTo(CameraWaypoint waypoint, bool ignorelock = false)
    {


        if (!ignorelock && cameraLock)
            yield break;
        while (cameraMoving)
            yield return null;

        cameraMoving = true;

        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = waypoint.transform.rotation;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = waypoint.transform.position;
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
    public bool getCameraMoving() { return cameraMoving; }
}
