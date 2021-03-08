
using UnityEngine;
    public enum ZoneLocation { Forest , GrassLands, Deserts, Mountains , Treant}
    public enum Rarity { Common , Uncommon, Rare, LegendREI}
public class ItemData
{
    public int amount;
    Sprite img;
    public ItemStruct GetData => Data;
    ItemStruct Data;
    public ItemData(string[] data)
    {
        Data.LoadItemData(data);
        GetRelevantSprite();
        amount = 1;
    }
    void GetRelevantSprite()
       => GetSetSprite = ItemFactory._Instance.GetItemSprite(Data.ID);
    public Sprite GetSetSprite { get => img; set => img = value; }
   public struct ItemStruct
    {
        public ZoneLocation[] location;
        public int ID;
        public string Name;
        public string Description;
        public Rarity rarityLevel;
        public float SellPriceReduction;
        public CurrencyDefinition Price;
        public bool isStackable;



        public void LoadItemData(string[] itemData)
        {
            Price = new CurrencyDefinition();

            // must be implemented first when creating new object (totem, elixer, weapon ,loot)

            if (itemData != null)
            {


                if (int.TryParse(itemData[0], out ID))
                {
                    //Item Name
                    if (itemData[2] != "")
                    {
                        Name = itemData[2];
                    }

                    //Set Location Of the item (where can you find it)
                    if (itemData[3] != "")
                    {
                        string[] locationStringCache = itemData[3].Split(new char[] { '-' });
                        location = new ZoneLocation[locationStringCache.Length];
                        for (int i = 0; i < location.Length; i++)
                        {
                            int.TryParse(locationStringCache[i], out int zone);
                            switch (zone)
                            {
                                case 1:
                                    location[i] = ZoneLocation.Forest;
                                    break;
                                case 2:
                                    location[i] = ZoneLocation.GrassLands;
                                    break;
                                case 3:
                                    location[i] = ZoneLocation.Deserts;
                                    break;
                                case 4:
                                    location[i] = ZoneLocation.Mountains;
                                    break;
                                default:
                                    Debug.Log("Please Check The CVS file for the right location");
                                    break;
                            }
                        }

                    }

                    // sell price reduction
                    if (itemData[4] != "")
                    {
                        float.TryParse(itemData[4], out SellPriceReduction);

                    }

                    // set the price of the item
                    if (itemData[5] != "")
                    {

                        string[] price = itemData[5].Split(new char[] { '-' });

                        int.TryParse(price[0], out this.Price.Price);
                        int.TryParse(price[1], out int currencyType);
                        Price.SetCurrencyType(currencyType);

                    }

                    // rarity level
                    if (itemData[6] != "")
                    {
                        switch (int.Parse(itemData[6]))
                        {
                            case 1:
                                rarityLevel = Rarity.Common;
                                break;
                            case 2:
                                rarityLevel = Rarity.Uncommon;
                                break;
                            case 3:
                                rarityLevel = Rarity.Rare;
                                break;
                            default:
                                rarityLevel = Rarity.LegendREI;
                                break;
                        }

                    }

                    // is stackable
                    if (itemData[7] != "")
                        bool.TryParse(itemData[7], out isStackable);

                }
            }
        }
    }
}


