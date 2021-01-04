using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSO : Item
{
  //public int amount;


    public LootSO(string[] lootData) : base(lootData) {
        this.isStackable = true;
        //amount = 1;
    }

}
