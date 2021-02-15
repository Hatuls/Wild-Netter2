

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotsUI : Slot
{
    int buttonID;

  [SerializeField] TextMeshProUGUI txtMP;
   
    public int GetSetButtonID { get => buttonID; set => buttonID = value; }
    public void SetInsideSprite(Sprite img) =>  this.insideIMG.sprite = img;
    public void SetOutSideSprite(Sprite img) => this.img.sprite = img;
    public void SetInsideImageToTransparent(bool ToAlpha)=>  this.insideIMG.color = ToAlpha ? Color.clear : Color.white;
    public void CleanTextSlot() => txtMP.text = "";  
    public void SetTextSlot(string txt) => txtMP.text = txt;
    public void GotClicked() =>   InventoryUIManager._Instance.ButtonClicked(this.buttonID);
  
    public void Init(int id)
    {
        GetSetButtonID = id;
      
        this.GetBtn.onClick.AddListener(this.GotClicked);
        txtMP = GetComponentInChildren<TextMeshProUGUI>();
    }
}
