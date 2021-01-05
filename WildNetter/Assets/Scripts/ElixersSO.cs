using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElixersSO : Item
{
   public enum ElixerType { };
   public ElixerType elixerType;
    //public int amount = 1;;
    public int AmountOfBuff;
    Item[] ItemsToMakeThis;


    public ElixersSO(string[] data) : base(data) { this.isStackable = true;  }
   
}
