using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSO : Item
{
   


    public LootSO(string[] lootData) : base(lootData) {
        this.isStackable = true;
    }
    

}
