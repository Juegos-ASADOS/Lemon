using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FruitCharacteristics : MonoBehaviour
{
    [SerializeField]
    JuiceType fruit;
    bool isCut = false;
    [SerializeField]
    Mesh cutMesh = null;
    public void cutFruit() { 
        isCut = true; 
        gameObject.GetComponent<MeshFilter>().mesh = cutMesh;
        gameObject.GetComponent<MeshCollider>().sharedMesh = cutMesh;
    }
    public JuiceType GetTypeFruit() { return fruit; }
    public bool IsCut() { return isCut; }
}
