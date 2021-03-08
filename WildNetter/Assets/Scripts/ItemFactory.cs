
using UnityEngine;

public class ItemFactory : MonoSingleton<ItemFactory>
{

    public Transform itemPF;
    public Sprite[] itemSprites;
  [SerializeField] SpriteSO sprite;
    public override void Init()
    {
     
    }

    public Sprite GetItemSprite(int _ID)
    {
        Debug.Log(itemSprites.Length);
        Debug.Log("****" + _ID);

        if (_ID % 10000 > sprite.SpriteByIMG.Length)
        {
            Debug.Log("Sprite Was not in Scriptable object");
            return null;
        }
       return sprite.SpriteByIMG[_ID % 10000];
     //  return sprite.SpriteByIMG[_ID % 10000];
      // return sprite.SpriteByIMG[0];
        


     //  return itemSprites[_ID %10000];
    }

    public ItemData GenerateItem(int id)
    {
        ItemData itemToGenerate = null;
        string[] data =LoadData.GetInstance().GetItemDataFromCSVFile(id);
        if (int.TryParse(data[1], out int Type))
        {
            switch (Type)
            {
                case 1:
                       itemToGenerate = new LootSO(data);
                    break;
                case 2:
                    itemToGenerate = new WeaponSO(data, LoadData.GetInstance().GetTypeOfItemDataFromOtherTable(Type,id));
                    break;
                case 3:
                   // itemToGenerate = new ElixersSO();
                    break;
                case 4:
                   itemToGenerate = GenerateTotem(data, LoadData.GetInstance().GetTypeOfItemDataFromOtherTable(Type, id));
                    break;
                default:
                    Debug.Log("Cant Generate That type id please check the data base");
                    break;
            }
        }
        return itemToGenerate;
    }

    public ItemData GenerateTotem(string[] itemData, string[] totemData)
    {
        if(int.TryParse(totemData[1], out int totemType))
        {
            switch(totemType)
            {
                case 1:
                    return new TotemOfDetection(itemData, totemData);
                case 2:
                    return new TotemOfHealing(itemData, totemData);
                case 3:
                    return new TotemOfPrey(itemData, totemData);
                case 4:
                    return new TotemOfStamina(itemData, totemData);
                case 5:
                    return new TotemOfShock(itemData, totemData);
                default:
                    break;
            }
        }
        Debug.Log("There is no such totem type in data!");
        return null;
    }



}
