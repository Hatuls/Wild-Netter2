using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Script References:
  
    // Component References:

    // Variables:
    public bool isInventoryFull;
    public const int maxCapacityPerSlot = 40;   // <- need to check with Dan about max Capacity 
    public const int totalBagSlot = 40;
    static int playerCopperMoney = 2525151;
    // Collections:

    static List<Item> inventoryList;
    // Getter & Setters:
    public static int[] GetCoinCurrency() {
        int[] currency = new int[] { 0, 0, 0 };
        currency[0] = playerCopperMoney; // copper
        currency[1] = (playerCopperMoney / 100)  ; // silver 
        currency[2] = (currency[1] / 100) ;  // gold

        return currency;
    }

    public static List<Item> GetInventoryList() {
        if (inventoryList ==null)
        {
            inventoryList = new List<Item>();
        }

        return inventoryList;
    }
    public static void AddToInventory(Item _item)
    {






        if (_item == null)
            return;
       

        if (GetInventoryList().Contains(_item))
        {
            Debug.Log(_item.isStackable);
            if (_item.isStackable)
            {

                foreach (var item in inventoryList)
                {
                    if (_item == item)
                    {
                        Debug.Log("Added amount");
                        item.amount += _item.amount;
                        return;
                    }
                }

            }
        }

        inventoryList.Add(_item);
    }
    public void Init()
    {

    }
    public void UpdateInventory(Item item, int amount) { }
    public bool CheckInventory(Item item, int amount) {

        if (GetInventoryList().Contains(item))
        {
            foreach (var itemCheck in inventoryList)
            {
                if (itemCheck == item)
                    return (itemCheck.amount >= amount);
            }
        }
        
        return false;
        
    }
   
    // public int GetAmountOfSomething(Item item) { return amount; } <- Fix Later 

    public void DragAndDrop() { }
}
