using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Script References:
    public static UiManager _instance;

    // Component References:
    // ****Add here all the panel/buttons/images component of the UI****
    [SerializeField] GameObject playerInventoryUIWindow;
    [SerializeField] GameObject inventorySlotHolder;
    [SerializeField] GameObject[] Slots;
    TextMeshProUGUI currencyTMP;
    TextMeshProUGUI inventoryCapacityTMP;
    //SerializeField] TextMeshProUGUI inventoryCapacityText;
   
    // PlayerInventory _playerInventory;
    // Variables:

    // Getter & Setters:
    private void Awake()
    {
        _instance = this;

    }
    private void Start()
    {
        Slots = new GameObject[PlayerInventory.totalBagSlot];
        currencyTMP = playerInventoryUIWindow.transform.Find("CurrencyText").GetComponent<TextMeshProUGUI>();
        inventoryCapacityTMP= playerInventoryUIWindow.transform.Find("CapacityText").GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < PlayerInventory.totalBagSlot; i++)
        {

            Slots[i]= inventorySlotHolder.transform.GetChild(i).gameObject;
   
        }
      UpdateInventory();
    }

    public void Init()
    {

        

    }
    public void ToggleMainMenu(bool state) { }


    public void UpdateInventory()
    {
    
        var inventory = PlayerInventory.GetInventoryList();
        currencyTMP.text = string.Format("Gold : {0}    Silver : {1}    Copper : {2}",PlayerInventory.GetCoinCurrency()[2], PlayerInventory.GetCoinCurrency()[1], PlayerInventory.GetCoinCurrency()[0]);
        inventoryCapacityTMP.text= string.Format("{0}/{1}", inventory.Count, PlayerInventory.totalBagSlot);
        string text = " / " + PlayerInventory.maxCapacityPerSlot;
    
        for (int i = 0; i < PlayerInventory.totalBagSlot; i++)
        {

         
            Slots[i].GetComponentInChildren<Text>().text = "";

            if (i < inventory.Count)
            {
             


                Slots[i].GetComponent<Image>().sprite = ItemFactory.GetInstance().GetItemSprite(inventory[i].ID);
                if (inventory[i].amount > 1)
                {


                
                    Slots[i].GetComponentInChildren<Text>().text = inventory[i].amount + text;
                 
                }
            }
        }
    }
    
    public void ToggleInventoryMenu(bool state)
    {

        if (state)
        {
            UpdateInventory();
        }
        playerInventoryUIWindow.SetActive(state);
    }
    public void ToggleStatsMenu(bool state) { }
    public void ToggleMapUI(bool state) { }
    public void ToggleTotemModifierMSG(bool state) { }
    public void ToggleMissionMenu(bool state) { }
    public void ToggleGUIinScene(bool state) { }
    public void CloseAllMenus() { }

}
