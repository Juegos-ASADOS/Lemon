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
        GameObject newfruit = Instantiate(cutObject, transform.position, transform.rotation);
        newfruit.transform.parent = transform.parent;
        //Vector3 pos = transform.position;
        //pos.y -= 0.05f;
        //cutObject.transform.position = pos;
        //cutObject.transform.rotation = transform.rotation;
        //cutObject.transform.parent = transform.parent;
        newfruit.GetComponent<FruitCharacteristics>().SetCut();
        //cutObject.SetActive(true);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
    public JuiceType GetTypeFruit() { return fruit; }
    public bool IsCut() { return isCut; }
    public void SetCut() { isCut = true; }
}
