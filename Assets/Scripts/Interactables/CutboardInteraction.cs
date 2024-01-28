using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutboardInteraction : InteractableObject
{
    [SerializeField]
    private float holdTime = 1.0f;
    private float restingTime = 0.0f;

    private GameObject knife;

    private bool hover = false;
    private bool canHold = false;
    [SerializeField]
    private Image fillBar;
    [SerializeField]
    private Canvas bar;
    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject != null)
        {
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.CUCHILLO &&
                transform.childCount == 4 && !transform.GetChild(3).GetComponent<FruitCharacteristics>().IsCut())
            {
                canHold = true;
                knife = pickedObject;
                FMOD_Manager.instance.PlaySingleInstanceEmitterControllerGroup("KnifeCut");
                if(SceneManager.GetActiveScene().name == "Dia 7")
                {
                    FMOD_Manager.instance.PlaySingleInstanceEmitterControllerGroup("Scream");
                }
            }
            else if (transform.childCount < 4 && pickedObject.GetComponent<FruitCharacteristics>() != null)
            {
                FMOD_Manager.instance.PlaySingleInstanceEmitterControllerGroup("Table");
                transform.GetComponent<BoxCollider>().enabled = false;
                pickedObject.GetComponent<InteractableObject>().destMovement = transform.GetChild(0);
                pickedObject.transform.parent = transform;
                if(pickedObject.GetComponent<FruitCharacteristics>().GetTypeFruit() == JuiceType.CAKE)
                {
                    Vector3 newT = pickedObject.GetComponent<InteractableObject>().destMovement.position;
                    newT.y = 2.4f;
                    pickedObject.GetComponent<InteractableObject>().destMovement.position = newT;
                }
                PlayerInstance.instance.RemoveHandObject();
            }
        }
    }

    void OnMouseOver() { hover = true; }
    void OnMouseExit() { hover = false; }

    void Update()
    {
        if (hover && canHold)
        {
            if (Input.GetMouseButton(0) && restingTime >= 0.0f)
            {
                if(!bar.gameObject.activeSelf)bar.gameObject.SetActive(true);
                restingTime -= Time.deltaTime;
                fillBar.fillAmount -= Time.deltaTime/holdTime;
            }
        }
        if (restingTime <= 0.0f)
        {
            if (transform.childCount == 4)
            {
                transform.GetChild(3).GetComponent<FruitCharacteristics>().CutFruit();

                canHold = false;
                transform.GetComponent<BoxCollider>().enabled = false;
            }
            //cutParticles.transform.position = transform.GetChild(0).position;
            //cutParticles.Play();
            restingTime = holdTime;
            fillBar.fillAmount = 1.0f;
            bar.gameObject.SetActive(false);
            if (knife != null)
            {
                FMOD_Manager.instance.SetGlobalParameterByName("Cutting", 1);
                FMOD_Manager.instance.StopSingleInstanceEmitterControllerGroup("KnifeCut");
                knife.GetComponent<KnifeInteraction>().CutEnd();
                knife = null;
                transform.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
