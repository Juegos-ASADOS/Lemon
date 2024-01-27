using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FruitCharacteristics : MonoBehaviour
{
    [SerializeField]
    JuiceType fruit;
    bool isCut = false;

    public void cutFruit() { isCut = true; }
    public JuiceType GetTypeFruit() { return fruit; }
    public bool IsCut() { return isCut; }
}
