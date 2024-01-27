using UnityEngine;
using System.Collections;

public class SimpleCameraShake : MonoBehaviour
{
	private Transform camTransform;

	[SerializeField] float shakeDuration = 0f;

	[SerializeField] float shakeAmount = 0.7f;
	[SerializeField] float decreaseFactor = 1.0f;
	float orD;
	float orDF;
	float orA;

	Vector3 originalPos;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
		orD = shakeDuration;
		orDF = decreaseFactor;
		orA = shakeAmount;
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		//TODO: Si esta shakeando no se puede mover la camara a otra seccion de la mesa
		if (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
			restartShake();
			this.enabled = false;
		}
	}

	public void restartShake()
    {
		shakeDuration = orD;

		shakeAmount = orA;
		decreaseFactor = orDF;
	}
}