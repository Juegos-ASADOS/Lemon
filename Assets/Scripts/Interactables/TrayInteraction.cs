using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum comandas { No_Tray, Empty_Cup, Empty_Plate, Lemon, Orange, grapefruit, Muffin, Croissant, Cake, Error}
public class TrayInteraction : InteractableObject
{
    private GameObject objectContained;


    public static event Action<comandas> GiveOrder = delegate { };

    private void Awake()
    {
        InteractableTimbre.SendOrder += giveOrder;
    }

    //recive el evento de mandar el pedido, asi que comprobamos que hay
    //en la bandeja y generamos una salida
    void giveOrder()
    {
        //recibe el evento de donde sea y envia el evento
        //GiveOrder con lo que sea que haya en la bandeja
        GiveOrder(GetComandas());
        
    }

    public comandas GetComandas()
    {
        //pedirle al objeto contenido su hijo, ver si es fruta o comida, is es otra cosa mega cagada ajaja
      
        if (objectContained != null)
        {
            return comandas.No_Tray;
        }
            InteractableCup cup = objectContained.GetComponent<InteractableCup>();
        if (cup)
        {
            if (cup.GetJuice() == JuiceType.ORANGE)
                return comandas.Orange;
            if (cup.GetJuice() == JuiceType.LEMON)
                return comandas.Lemon;
            if (cup.GetJuice() == JuiceType.GRAPEFRUIT)
                return comandas.grapefruit;
            if (cup.GetJuice() == JuiceType.EMPTY)
                return comandas.Empty_Cup;
        }
        InteractablePlate plate = objectContained.GetComponent<InteractablePlate>();
        if (plate)
        {
            FoodCharacteristics food = plate.getFood().GetComponent<FoodCharacteristics>();

            if (!food)
                return comandas.Empty_Plate;
            if (food.GetTypeFood() == FoodType.MUFFIN)
                return comandas.Muffin;
            if (food.GetTypeFood() == FoodType.CROISSANT)
                return comandas.Croissant;
            if (food.GetTypeFood() == FoodType.CAKE)
                return comandas.Cake;
        }
        return comandas.Error; //esot no ocurrira nunca
    }
    public override void Interact(GameObject pickedObject)
    {
        if (pickedObject != null)
        {            
            if (pickedObject.GetComponent<InteractableObject>().objType == ObjectType.VASO||
                pickedObject.GetComponent<InteractableObject>().objType == ObjectType.PLATO &&
                transform.childCount < 2)
            {
                pickedObject.transform.position = transform.GetChild(0).position;
                pickedObject.transform.parent = transform;
                objectContained = pickedObject;
                PlayerInstance.instance.RemoveHandObject();
            }
        }
    }
}
