using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemSO : Item
{

    // Object References:
   public Item[] itemsToBuildThis;
    // Variables:
    public enum TotemTypes { };
    public TotemTypes totemTypes;
    public int duration;
    public int range;
    public int MinimumPlayerLevel;


    public TotemSO(string[] data) : base(data) { this.isStackable = false; }
    public override void PickUp()
    {
        throw new System.NotImplementedException();
    }

    public override void Destroy(GameObject objectToDestroy)
    {
        throw new System.NotImplementedException();
    }
}
