using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Script References:
    public static UiManager _instance;
    PlayerInventory _playerInventory;
    PlayerWallet _wallet;
    // Component References:
    // ****Add here all the panel/buttons/images component of the UI****
    [SerializeField] GameObject playerInventoryUIWindow;
    [SerializeField] GameObject inventorySlotHolder;
    [SerializeField] GameObject[] Slots;
    TextMeshProUGUI currencyTMP;
    TextMeshProUGUI inventoryCapacityTMP;
    Sprite defaultSpriteForSlot;

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
        Init();
    }

    public void Init()
    {
        _playerInventory = PlayerInventory.GetInstance;
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
    public void ToggleMainMenu(bool state) { }


    public void UpdateInventory()
    {
        if (!playerInventoryUIWindow.activeSelf)
            return;
        
        var inventory = _playerInventory.GetInventory;
        //Need to create PlayerWallet - > _wallet
        //  currencyTMP.text = string.Format("Gold : {0}    Silver : {1}    Copper : {2}", _playerInventory.GetCoinCurrency()[2], _playerInventory.GetCoinCurrency()[1], _playerInventory.GetCoinCurrency()[0]);
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


    public void DropItemFromInventory(int i) {
        if (_playerInventory.GetInventory[i] != null)
        {
            _playerInventory.PrintInventory();
            PickUpObject.SpawnItemInWorld(_playerInventory.GetInventory[i], PlayerManager.GetInstance().GetPlayerTransform.position, PlayerManager.GetInstance().GetPlayerTransform);
            _playerInventory.RemoveItemFromInventory(_playerInventory.GetInventory[i]);
            UpdateInventory();

            _playerInventory.PrintInventory();
        }


    
    }


}
