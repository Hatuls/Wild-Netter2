
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    private static ItemFactory _instance;
    public Transform itemPF;
    public Sprite[] itemSprites; 

    public static ItemFactory GetInstance()
    {
     
        return _instance;
    }
    private void Awake()
    {
        _instance = this;
    }
    public Sprite GetItemSprite(int _ID)
    {
        // needed to add dictionary to search for those pictures
        Debug.Log(itemSprites.Length);
        return itemSprites[_ID %10000];
    }

    public Item GenerateItem(int id)
    {
        Item itemToGenerate = null;
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
                   itemToGenerate = new TotemSO(data, LoadData.GetInstance().GetTypeOfItemDataFromOtherTable(Type, id));
                    break;
                default:
                    Debug.Log("Cant Generate That type id please check the data base");
                    break;
            }
        }
        return itemToGenerate;
    }



}
