
using System;

using UnityEngine;

public class PlayerWallet
{
    private static PlayerWallet _instance;
    public static PlayerWallet GetInstance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerWallet();
            }
            return _instance;
        } 
    }
    PlayerWallet() {
        Init();
    }
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
    void Init() {

        GetSetPlayersCopper = 1001;
            GetSetPlayersSilver = 0;
            GetSetPlayersGold = 0;
    }





    // 100 copper = 1 silver;
    //1 gold = 10 silver
    // 1 gold = 1000 copper;
    public void ConvertMoney(CurrencyType convertFrom, CurrencyType convertTo)
    {


        if (convertFrom == convertTo)
            return;
        //if (!HaveEnoughMoneyToConvert(convertFrom, convertTo))
        //{
        //    Debug.Log("Not Enough Money for convertion!");
        //    return;
        //}



        switch (convertFrom) 
        {
            case CurrencyType.Copper:

                if (convertTo == CurrencyType.Silver)
                {
                    // need to put logic behind the convertions
                    if (GetSetPlayersCopper - GetSetPlayersSilver * 100 >= 0)
                    {

                    GetSetPlayersSilver += GetSetPlayersCopper / 100;

                    GetSetPlayersCopper -= GetSetPlayersSilver * 100;
                    }

                    
                }
                else
                {
                    // need to put logic behind the convertions
                    GetSetPlayersGold += GetSetPlayersCopper / 1000;
                    if (GetSetPlayersCopper - GetSetPlayersGold * 1000 >= 0)
                        GetSetPlayersCopper -= GetSetPlayersGold * 1000;
                }
                break;


            case CurrencyType.Silver:
                if (convertTo == CurrencyType.Copper)
                {
                    if (GetSetPlayersSilver <= 0)
                        return;
                   
                    GetSetPlayersCopper += GetSetPlayersSilver * 100;
                    GetSetPlayersSilver -= GetSetPlayersCopper / 100;
                }
                else
                {
                    // need to put logic behind the convertions
                    GetSetPlayersGold += GetSetPlayersSilver / 10;
                    if (GetSetPlayersSilver - GetSetPlayersGold * 10 >= 0)
                        GetSetPlayersSilver -= GetSetPlayersGold * 10;
                }

                break;



            case CurrencyType.Gold:

                if (GetSetPlayersGold <= 0)
                    return;

                if (convertTo == CurrencyType.Silver)
                {
                   
                    GetSetPlayersSilver += GetSetPlayersGold * 10;
                    GetSetPlayersGold -= GetSetPlayersSilver / 10;
                }
                else
                {

                    GetSetPlayersCopper += GetSetPlayersGold * 1000;
                    GetSetPlayersGold -= GetSetPlayersCopper / 1000;
                }

                break;
        }
    }

    private bool HaveEnoughMoneyToConvert(CurrencyType convertFrom, CurrencyType convertTo)
    {
        throw new NotImplementedException();
    }

    public bool HaveEnoughMoneyToBuy() {



        return false; 
    
    
    
    
    
    
    } 


}
