
using UnityEngine;


public class LoadData 

{
    static LoadData _instance;

    string[] itemData, weaponData, elixerData, totemData;



    public static LoadData GetInstance() {
        if (_instance == null)
        {
            _instance = new LoadData();
            
        }
        return _instance;
    }
    LoadData() { Init(); }
    void Init() {
        
        itemData =  Resources.Load<TextAsset>("ItemData").text.Split(new char[] { '\n' });
        weaponData = Resources.Load<TextAsset>("WeaponData").text.Split(new char[] { '\n' });
        //elixerData = Resources.Load<TextAsset>("elixerData").text.Split(new char[] { '\n' });
       totemData = Resources.Load<TextAsset>("TotemData").text.Split(new char[] { '\n' });;
    }
  
    
    

    string[] GetCSVFileFromItemID(int id)
    {
        switch (id)
        {
            case 2:
                return weaponData;
            
            case 3:
                return elixerData;
            
            case 4:
                return  totemData;
            
            default:
                Debug.Log("No Such Table Was Found");
                break;
        }
        return null;
    }

    public string[] GetTypeOfItemDataFromOtherTable(int IDForType , int itemID)
    {
      string[] data =  GetCSVFileFromItemID(IDForType);
        if (data != null)
        {

            try
            {
                Debug.Log(data.Length);
                string[] row = null;
                for (int i = 1; i < data.Length - 1; i++)
                {
                    row = data[i].Replace(" " , "" ).Split(new char[] { ',' });
                    
                    if (row[0] != "")
                    {
                       
                        if (itemID == int.Parse(row[0]))
                        {
                            return row;
                        }
                    }
                }


            }
            catch (System.Exception)
            {

                throw;
            }
        }

        return null;
    }


    public string[] GetItemDataFromCSVFile(int id)
    {
        try
        {
            string[] row = null ;
            for (int i = 1; i < itemData.Length - 1; i++)
            {
                row = itemData[i].Replace(" " , "").Split(new char[] { ',' });


                if (row[0] != "")
                {


                    if (int.TryParse(row[0], out int itemID))
                    {

                        if (id == itemID)
                        {
                            Debug.Log("Found Row : " + row.Length);
                           
                            return row;
                        }
                    }
                }
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Cannot Find The Item");
            throw;
        }
        return null;


    }
}



