
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoSingleton<UiManager>
{
    // Script References:

    PlayerInventory _playerInventory;
    PlayerWallet _wallet;
    // Component References:
    // ****Add here all the panel/buttons/images component of the UI****
    [SerializeField] GameObject playerInventoryUIWindow;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject loadGameMenu;
    [SerializeField] GameObject newsMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject inventorySlotHolder;
    [SerializeField] GameObject[] Slots;
    TextMeshProUGUI currencyTMP;
    TextMeshProUGUI inventoryCapacityTMP;
    Sprite defaultSpriteForSlot;
    Item[] inventory;
    //SerializeField] TextMeshProUGUI inventoryCapacityText;

     
    // Variables:

    // Getter & Setters:

  
    public override void Init()
    {
        _wallet = PlayerWallet.GetInstance;
        _playerInventory = PlayerInventory.GetInstance;
        inventory = _playerInventory.GetInventory;
         Slots = new GameObject[_playerInventory.maxCapacityOfItemsInList];
        currencyTMP = playerInventoryUIWindow.transform.Find("CurrencyText").GetComponent<TextMeshProUGUI>();
        inventoryCapacityTMP= playerInventoryUIWindow.transform.Find("CapacityText").GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < _playerInventory.maxCapacityOfItemsInList; i++)
        {

            Slots[i]= inventorySlotHolder.transform.GetChild(i).gameObject;
   
        }
        defaultSpriteForSlot = inventorySlotHolder.transform.GetChild(0).GetComponent<Image>().sprite;
      UpdateInventory();
    }
    public void ToggleMainMenu(bool state) 
    {
        mainMenu.SetActive(state);
    }


    public void UpdateInventory()
    {
        if (!playerInventoryUIWindow.activeSelf)
            return;
        
        //Need to create PlayerWallet - > _wallet
          currencyTMP.text = string.Format("Gold : {0}    Silver : {1}    Copper : {2}", _wallet.GetSetPlayersGold, _wallet.GetSetPlayersSilver, _wallet.GetSetPlayersCopper);
        inventoryCapacityTMP.text= string.Format("{0}/{1}", inventory.Length - _playerInventory.GetAmountOfItem(null)  , inventory.Length);
        string text = " / " + _playerInventory.maxCapacityOfItemsInSlot;
    
        for (int i = 0; i < inventory.Length; i++)
        {
            if ( inventory[i] == null)
                {
            Slots[i].GetComponentInChildren<Text>().text = "";
                Slots[i].GetComponent<Image>().sprite = defaultSpriteForSlot;
                continue;
                }

         
            

            if (i < inventory.Length)
            {
                


                Slots[i].GetComponent<Image>().sprite = ItemFactory._Instance.GetItemSprite(inventory[i].ID);
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
    public void ToggleOptionsMenu(bool state) 
    {
        optionsMenu.SetActive(state);
    }
    public void ToggleLoadGameMenu (bool state) 
    {
        loadGameMenu.SetActive(state);
    }
    public void ToggleNewsMenu(bool state)
    {
        newsMenu.SetActive(state);
    }
    public void CloseAllMenus() { }


    public void DropItemFromInventory(int i) {
        if (inventory[i] != null)
        {
            var itemToDrop = ItemFactory._Instance.GenerateItem(inventory[i].ID);
            itemToDrop.amount = inventory[i].amount;
            PickUpObject.SpawnItemInWorld(itemToDrop, PlayerManager._Instance.GetPlayerTransform.position, PlayerManager._Instance.GetPlayerTransform);
            _playerInventory.RemoveItemFromInventory(inventory[i]);
            UpdateInventory();

        }
    }


}
