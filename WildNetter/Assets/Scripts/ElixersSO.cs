
public class ElixersSO : ItemData
{
   public enum ElixerType { };
   public ElixerType elixerType;

    public int AmountOfBuff;
    ItemData[] ItemsToMakeThis;


    public ElixersSO(string[] data) : base(data) { }


    
}
