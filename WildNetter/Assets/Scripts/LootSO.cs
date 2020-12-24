using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSO : Item
{
   


    public LootSO(string[] lootData) : base(lootData) {
        this.isStackable = true;
    }
    
    public override void Destroy(GameObject objectToDestroy)
    {
        throw new System.NotImplementedException();
    }
    public int GetAmountOfItem() { return amount; }
    public override void PickUp()
    {
        throw new System.NotImplementedException();
    }
}
