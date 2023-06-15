using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class PlayerSaveData
{
    //最大HP
    [SerializeField]
    private int maxHp;
    //現在のHP
    [SerializeField]
    private int nowHp;
    //所持金
    [SerializeField]
    private int haveMoney;
    //持っているカード(ID管理)
    [SerializeField]
    private List<int> cardListId = new List<int>{};

    //各種ゲッター・セッター
    public int GetMaxHp(){
        return maxHp;
    }

    public void SetMaxHp(int input){
        maxHp = input;
    }

    public int GetNowHp(){
        return nowHp;
    }

    public void SetNowHp(int input){
        nowHp = input;
    }

    public int GetHaveMoney(){
        return haveMoney;
    }

    public void SetHaveMoney(int input){
        haveMoney = input;
    }

    public List<int> GetCardList(){
        return cardListId;
    }


    //カードリストを扱うメソッド類
    //持ち札にカードを追加
    public void AddCardInList(int id){
        cardListId.Add(id);
        SortCardList();
    }
    //持ち札のカードを削除
    public void RemoveCardInList(int id){
        cardListId.Remove(id);
        SortCardList();
    }
    //カードリストのソート
    public void SortCardList(){
        cardListId.Sort();
    }


    //セーブデータに関係のあるメソッド類
    public static void SavingData(PlayerSaveData data){
        StreamWriter writer;

        string toJson = JsonUtility.ToJson(data);
        writer = new StreamWriter(Application.dataPath + "/Class/PlayerSaveData/DataFolder/savedata.json", false);
        writer.Write(toJson);
        writer.Flush();
        writer.Close();
    }

    
    public static PlayerSaveData LoadingData(){
        string loadStr = "";
        StreamReader reader;

        reader = new StreamReader (Application.dataPath + "/Class/PlayerSaveData/DataFolder/savedata.json");
        loadStr = reader.ReadToEnd ();
        reader.Close();
        return JsonUtility.FromJson<PlayerSaveData>(loadStr);
    }
    
    public void ResetData(){
        maxHp = 100;
        nowHp = maxHp;
        haveMoney = 0;
        cardListId = new List<int>{1, 1, 1, 2, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6};
    }
}
