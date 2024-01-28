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
    public void cutFruit()
    {
        SetCut();
        
        GameObject newfruit = Instantiate(cutObject, transform.position,transform.rotation);
        newfruit.transform.parent = transform.parent;
        newfruit.GetComponent<FruitCharacteristics>().SetCut();
        Destroy(gameObject);
    }
    public JuiceType GetTypeFruit() { return fruit; }
    public bool IsCut() { return isCut; }
    public void SetCut() { isCut = true; }
}
