using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FruitCharacteristics : MonoBehaviour
{
    [SerializeField]
    JuiceType fruit;
    bool isCut = false;
    [SerializeField]
    GameObject cutObject;
    public void CutFruit()
    {
        SetCut();
        FMOD_Manager.instance.PlaySingleInstanceEmitterControllerGroup("KnifeCut");
        GameObject newfruit = Instantiate(cutObject, transform.position, transform.rotation);
        //newfruit.transform.localScale = transform.localScale;
        newfruit.transform.parent = transform.parent;
        if (newfruit.GetComponent<FruitCharacteristics>().GetTypeFruit() != JuiceType.LEMON)
        {
            Vector3 newPos = newfruit.transform.localPosition;
            newPos.y = 0.0f;
            newfruit.transform.localPosition = newPos;
        }
        newfruit.GetComponent<FruitCharacteristics>().SetCut();
        Destroy(gameObject);
    }
    public JuiceType GetTypeFruit() { return fruit; }
    public bool IsCut() { return isCut; }
    public void SetCut() { isCut = true; }
}
