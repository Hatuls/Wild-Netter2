﻿using System;

using UnityEngine;

public class PlayerInventory
{
    private static PlayerInventory _instance;
    //Inventory IInventory.GetInstance => GetInstance;
    public static PlayerInventory GetInstance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerInventory();

            }

            return _instance;
        }
    }
    internal int maxCapacityOfItemsInSlot = 40;
    internal int maxCapacityOfItemsInList = 21;
    bool checkForItem;
    int counter;
    int itemAmountCount;
    public int GetAmountOfItemsInInventoryGetAmountOfItemsInInventory
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < inventoryList.Length; i++)
            {
                counter++;
            }
            return counter;
        }



    }
    ItemData[] inventoryList;
    private int nextAddOnAmountForInventory = 5;



    public ItemData[] GetInventory { get => inventoryList; }





    private PlayerInventory()
    {
        inventoryList = new ItemData[maxCapacityOfItemsInList];
    }





    public void MakeInventoryBigger(int _newSize)
    {
        if (_newSize < maxCapacityOfItemsInList)
            return;
        maxCapacityOfItemsInList += _newSize;
        ItemData[] newInventoryList = new ItemData[maxCapacityOfItemsInList];

        Array.Copy(inventoryList, newInventoryList, inventoryList.Length);
        inventoryList = newInventoryList;
    }

    public bool CheckIfEnoughSpaceInInventory(ItemData item)
    {
        if (item == null)
            return false;

        checkForItem = false;

        counter = 0;
        if (item.amount <= maxCapacityOfItemsInSlot)
        {
            counter = 1;
        }
        else
        {


            counter = item.amount / maxCapacityOfItemsInSlot;

            if (item.amount % maxCapacityOfItemsInSlot > 0 && item.amount > maxCapacityOfItemsInSlot)
            {
                counter += 1;
            }
        }




        if (counter <= GetAmountOfItem(null))
        {
            checkForItem = true;
        }



        counter = 0;

        if (!checkForItem)
        {
            for (int i = 0; i < inventoryList.Length; i++)
            {
                if (item.GetData.ID == inventoryList[i].GetData.ID)
                {
                    counter += maxCapacityOfItemsInSlot;
                }
            }

            if (GetAmountOfItem(item) + item.amount <= counter)
            {
                checkForItem = true;
            }
        }

        return checkForItem;
    }


    void AddAmountOfItem(ItemData item)
    {


        if (item == null || item.amount <= 0)
            return;

        if (!item.GetData.isStackable)
        {
            if (item.amount <= GetAmountOfItem(null))
            {
                counter = 0;
                for (int i = 0; i < item.amount; i++)
                {
                    if (inventoryList[i] == null)
                    {
                        counter++;

                        //inventoryList[i] = new ItemSlot(item.item, item.amount);
                        inventoryList[i] = item;
                        inventoryList[i].amount = 1;
                    }


                    if (counter >= item.amount)
                    {
                        return;
                    }

                }

            }
        }




        int test = item.amount - itemAmountCount;

        for (int i = 0; i < inventoryList.Length; i++)
        {
            if (inventoryList[i] == null)
                continue;

            if (inventoryList[i].GetData.ID == item.GetData.ID)
            {
                if (inventoryList[i].amount == maxCapacityOfItemsInSlot)
                    continue;

                if (test + inventoryList[i].amount > maxCapacityOfItemsInSlot)
                {

                    test = Mathf.Abs(maxCapacityOfItemsInSlot - (inventoryList[i].amount + test));
                    inventoryList[i].amount = maxCapacityOfItemsInSlot;
                    item.amount = test;
                    AddAmountOfItem(item);
                    return;
                }
                else if (test + inventoryList[i].amount == maxCapacityOfItemsInSlot)
                {
                    inventoryList[i].amount = maxCapacityOfItemsInSlot;
                    return;
                }
                else if (test + inventoryList[i].amount < maxCapacityOfItemsInSlot)
                {
                    inventoryList[i].amount += test;
                    return;
                }
            }
        }

        //  inventoryList[GetItemIndexInArray(null)] = new ItemSlot(item.item, test); ;
        item.amount = test;


        inventoryList[GetItemIndexInArray(null)] = item;

    }

    public void AddToInventory(ItemData item)
    {
        if (item == null)
            return;
        // if i dont have it in the inventory
        if (CheckIfEnoughSpaceInInventory(item))
        {
            itemAmountCount = 0;
            AddAmountOfItem(item);
            InventoryUIManager._Instance.UpdateInventorySlots();
            Debug.Log("Added");
            return;
        }
        Debug.Log("Cant Add The Item");
    }


    private void RemoveObjectFromInventory(ItemData item)
    {
        if (item.amount < 0)
            item.amount *= -1;



        if (item == null || item.amount <= 0)
            return;

        // if item is not stackable
        if (!item.GetData.isStackable)
        {
            for (int x = 0; x < item.amount; x++)
            {
                for (int i = inventoryList.Length - 1; i >= 0; i--)
                {
                    if (inventoryList[i] != null && inventoryList[i].GetData.ID == item.GetData.ID)
                    {
                        inventoryList[i] = null;
                        break;
                    }
                }
            }

            return;
        }


        int amountIHave = GetAmountOfItem(item);
        if (item.amount > amountIHave)
        {
            Debug.Log("You are trying To Remove : " + item.amount + " and I Have Only This Amount : " + amountIHave);
            return;
        }


        // if item is istackable

        counter = 0;
        for (int i = inventoryList.Length - 1; i >= 0; i--)
        {
            if (inventoryList[i] == null)
                continue;

            if (inventoryList[i].GetData.ID == item.GetData.ID)
            {

                if (itemAmountCount - inventoryList[i].amount > 0)
                {
                    itemAmountCount = itemAmountCount - inventoryList[i].amount;
                    inventoryList[i] = null;
                    RemoveObjectFromInventory(item);
                    return;
                }
                else if (itemAmountCount - inventoryList[i].amount == 0)
                {
                    inventoryList[i] = null;
                    break;
                }
                else
                {
                    inventoryList[i].amount -= itemAmountCount;
                    break;
                }


            }
        }

    }

    public void RemoveItemFromInventory(ItemData item)
    {
        itemAmountCount = item.amount;
        RemoveObjectFromInventory(item);
    }

    public bool CheckInventoryForItem(ItemData item)
    {

        checkForItem = false;
        for (int i = 0; i < inventoryList.Length; i++)
        {
            if (item == null && inventoryList[i] == null)
            {

                checkForItem = true;
                break;

            }
            else if (item.GetData.ID == inventoryList[i].GetData.ID)
            {
                checkForItem = true;
                break;
            }
        }

        return checkForItem;
    }

    // this function will return true if crafting is ok ***************** run it on later stage
    //public bool CheckEnoughItemsForRecipe(RecipeSO recipe)
    //{
    //    bool haveAllIngridients = true;


    //    foreach (var item in recipe.getitemCostArr)
    //    {
    //        haveAllIngridients = haveAllIngridients && HaveEnoughOfItemFromInventory(item);
    //        if (!haveAllIngridients)
    //        {
    //            haveAllIngridients = false;
    //            break;
    //        }
    //    }

    //    if (haveAllIngridients)
    //    {
    //        for (int i = 0; i < recipe.getitemCostArr.Length; i++)
    //        {
    //            RemoveItemFromInventory(recipe.getitemCostArr[i]);
    //        }
    //        AddToInventory(recipe.getoutcomeItem);
    //    }
    //    else
    //        Debug.Log("Cant Craft Not Enough resources");



    //    return haveAllIngridients;
    //}


    //bool HaveEnoughOfItemFromInventory(Item item)
    //{
    //    counter = 0;
    //    for (int i = 0; i < inventoryList.Length; i++)
    //    {
    //        if (item == null && inventoryList[i] == null)
    //        {
    //            counter += 1;
    //            continue;
    //        }
    //        if (inventoryList[i] == null)
    //        {
    //            continue;
    //        }
    //        if (inventoryList[i].ID == item.ID)
    //        {
    //            counter += inventoryList[i].amount;
    //            if (counter >= item.amount)
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}

    public int GetAmountOfItem(ItemData item)
    {
        counter = 0;
        for (int i = 0; i < inventoryList.Length; i++)
        {
            if (item == null && inventoryList[i] == null)
            {
                counter++;
                continue;
            }
            else if (item != null && inventoryList[i] != null)
            {
                if (item.GetData.ID == inventoryList[i].GetData.ID)
                {
                    counter += inventoryList[i].amount;
                }
            }
        }
        return counter;

    }
    public int GetItemIndexInArray(ItemData item)
    {

        for (int i = 0; i < inventoryList.Length; i++)
        {
            if (item == null)
            {
                if (inventoryList[i] == null)
                    return i;
            }
            else
            if (inventoryList[i] != null && item.GetData.ID == inventoryList[i].GetData.ID)
            {
                return i;
            }
        }

        Debug.Log("You Dont Have This Item In Your Inventory");
        return 0;
    }


    public void PrintInventory()
    {

        for (int i = 0; i < inventoryList.Length; i++)
        {
            if (inventoryList[i] == null)
            {
                Debug.Log("Inventory list in spot " + i + "is Null");
            }
            else
                Debug.Log("Inventory list in spot " + i + " with the amount : " + inventoryList[i].amount + " of type: " + inventoryList[i].GetData.Name);
        }

    }


    public void DragAndDrop() { }

}
public interface IInventory
{
    PlayerInventory GetInstance { get; }
    ItemData[] GetInventory { get; }

    void AddToInventory(ItemData item);
    bool CheckEnoughItemsForRecipe(ItemData recipe);
    bool CheckIfEnoughSpaceInInventory(ItemData item);
    bool CheckInventoryForItem(ItemData item);
    int GetAmountOfItem(ItemData item);
    int GetItemIndexInArray(ItemData item);
    void MakeInventoryBigger(int _newSize);
    void PrintInventory();
    void RemoveObjectFromInventory(ItemData item);
}