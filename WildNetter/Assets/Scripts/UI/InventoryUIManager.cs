
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoSingleton<InventoryUIManager>
{
    [SerializeField] InventorySlotsUI[] inventorySlots;
    [SerializeField] Transform SlotsContainer;
    [SerializeField] TextMeshProUGUI capacityTMP, copperTMP, silverTMP, goldTMP;
 
    Item[] inventoryData;
    PlayerInventory playerInventory;
    PlayerWallet playerWallet;


    public override void Init()
    {
        playerWallet = PlayerWallet.GetInstance;
        playerInventory = PlayerInventory.GetInstance;
        inventoryData = playerInventory.GetInventory;
        if (SlotsContainer != null)
     
            inventorySlots = SlotsContainer.GetComponentsInChildren<InventorySlotsUI>();

            if (inventorySlots == null|| playerWallet == null)
                return;

            for (int i = 0; i < inventorySlots.Length; i++)
                inventorySlots[i].Init(i);

        InventoryUIManager._Instance.UpdateInventorySlots();
    }




    private void UpdateCurrencyTxt() {

        copperTMP.text = playerWallet.GetSetPlayersCopper.ToString();
        silverTMP.text = playerWallet.GetSetPlayersSilver.ToString();
        goldTMP.text = playerWallet.GetSetPlayersGold.ToString();


    }
    private void SetCapacityTextOfTotalItemsInUI()
    {
        if (capacityTMP.text != 
            playerInventory.GetAmountOfItemsInInventoryGetAmountOfItemsInInventory
            + " / " +
            playerInventory.maxCapacityOfItemsInList)
      
        {

            capacityTMP.text =
                playerInventory.GetAmountOfItemsInInventoryGetAmountOfItemsInInventory
                + " / " +
                playerInventory.maxCapacityOfItemsInList;
        }
    }
  public void UpdateInventorySlots() {
        UpdateCurrencyTxt();
        for (int i = 0; i < inventorySlots.Length; i++)
           UpdateSlotsSprite(i);
        
    }

    internal void ButtonClicked(int buttonID)
    {
        Debug.Log("Button Id : " + buttonID + " Was Pressed");
    }

    public void UpdateSlotsSprite(int i)
    {
        if (inventoryData == null)
            return;


        if (inventoryData[i] == null)
        {
            (inventorySlots[i] as InventorySlotsUI).CleanTextSlot();
            (inventorySlots[i] as InventorySlotsUI).SetInsideImageToTransparent(true);
            SetCapacityTextOfTotalItemsInUI();
            return;
        }


            (inventorySlots[i] as InventorySlotsUI).SetInsideImageToTransparent(false);
            (inventorySlots[i] as InventorySlotsUI).SetInsideSprite(inventoryData[i].GetSetSprite);



        if (!inventoryData[i].isStackable)
            (inventorySlots[i] as InventorySlotsUI).CleanTextSlot();
        else
            (inventorySlots[i] as InventorySlotsUI).SetTextSlot(inventoryData[i].amount + " / " + PlayerInventory.GetInstance. maxCapacityOfItemsInSlot);

        SetCapacityTextOfTotalItemsInUI();
    }



}
