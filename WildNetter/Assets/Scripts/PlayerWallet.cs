using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    //Varaibles:
    int playersMoney;
    int playersGold;
    int playersCopper;
    int PlayerSilvers;

    //Getters & Setters:
    public int GetSetPlayersGold { get => playersGold; set => playersGold = value; }
    public int GetSetPlayersSilver { get => PlayerSilvers; set => PlayerSilvers = value; }
    public int GetSetPlayersCopper { get => playersCopper; set => playersCopper = value; }
    public int GetSetPlayersMoney { get => playersMoney; set => playersMoney = value; }


    //Functions:
    public void Init() { }
    public void ConvertMoney() { }

//    public bool HaveEnoughMoney() { return false; }  <Adjust when working


}
