using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElixersSO : Item
{
   public enum ElixerType { };
   public ElixerType elixerType;

    public int AmountOfBuff;
    Item[] ItemsToMakeThis;


    public ElixersSO(string[] data) : base(data) { this.isStackable = true; }
    public override void PickUp()
    {
        throw new System.NotImplementedException();
    }

    public override void Destroy(GameObject objectToDestroy)
    {
        throw new System.NotImplementedException();
    }

    
}
