using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerClass : ObjectClass
{
    List<int> haveCardId = new List<int>{};
    private int money;

    public void SetStatus(PlayerSaveData data){
        maxHp = data.GetMaxHp();
        hp = data.GetNowHp();
        haveCardId = data.GetCardList();
        money = data.GetHaveMoney();
    }

    public void DebugHp(){
        Debug.Log(hp);
    }    
}
