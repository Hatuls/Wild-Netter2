
using UnityEngine;


public abstract class Item { 
    public enum Rarity { Common , Uncommon, Rare, LegendREI}
    public enum ZoneLocation { Forest , GrassLands, Deserts, Mountains , Treant}
    public ZoneLocation[] location;
    public int ID = 0;
    public string Name;
    public string Description;
    public Rarity rarityLevel;
    public float SellPriceReduction = 0;
    public CurrencyDefinition Price;
    public bool isStackable;
    public int amount;
   
    
    public Item(string[] itemData) {
        Price = new CurrencyDefinition(); 
        LoadItemData(itemData);
       
    }

    public void LoadItemData(string[] itemData)
    {

        // must be implemented first when creating new object (totem, elixer, weapon ,loot)

        if (itemData != null)
        {


            if (int.TryParse(itemData[0], out ID))
            {
                //Item Name
                if (itemData[2] != "")
                {
                    this.Name = itemData[2];
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
                                this.location[i] = ZoneLocation.Forest;
                                break;
                            case 2:
                                this.location[i] = ZoneLocation.GrassLands;
                                break;
                            case 3:
                                this.location[i] = ZoneLocation.Deserts;
                                break;
                            case 4:
                                this.location[i] = ZoneLocation.Mountains;
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
                    float.TryParse(itemData[4], out this.SellPriceReduction);

                }

                // set the price of the item
                if (itemData[5] != "")
                {
                   
                    string[] price = itemData[5].Split(new char[] { '-' });

                    int.TryParse(price[0], out this.Price.Price);
                    int.TryParse(price[1], out int currencyType);
                    this.Price.SetCurrencyType(currencyType);

                }

                // rarity level
                if (itemData[6] != "")
                {
                    switch (int.Parse(itemData[6]))
                    {
                        case 1:
                            this.rarityLevel = Rarity.Common;
                            break;
                        case 2:
                            this.rarityLevel = Rarity.Uncommon;
                            break;
                        case 3:
                            this.rarityLevel = Rarity.Rare;
                            break;
                        default:
                            this.rarityLevel = Rarity.LegendREI;
                            break;
                    }

                }
              
                amount = 1;
            }

        }

    }

    public void Print() {
        if (this.Name != null)
        Debug.Log(" Name :" + this.Name);

            Debug.Log("ID : " + this.ID);

        if (this.Description != null)
            Debug.Log("Description : " + this.Description);


      
                Debug.Log("SellPriceReduction : " + this.SellPriceReduction);

      


            Debug.Log("Price : " + this.Price.Price);






            Debug.Log("ID : " + this.Price.currency);
       

  
            Debug.Log("rarityLevel : " + this.rarityLevel);
             
    
    }

    
}
