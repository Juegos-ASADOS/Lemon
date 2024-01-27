using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType { MUFFIN, CROISSANT, CAKE }

public class FoodCharacteristics : MonoBehaviour
{
    [SerializeField]
    FoodType food;
    bool isCut = false;

    public void CutFood()
    {
        if (food == FoodType.CAKE)
            isCut = true;
    }
    public FoodType GetTypeFood() { return food; }
    public bool IsCut()
    {
        if (food != FoodType.CAKE) return false;
        return isCut;
    }
}
