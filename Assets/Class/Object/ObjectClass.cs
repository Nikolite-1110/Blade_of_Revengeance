using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class ObjectClass
{
    //設計思想としては、抽象クラスを使用してそこからプレイヤーとエネミーにそれぞれ継承する形を取る。
    //共通のフィールド・メソッドとしては、hpや状態異常に関する書処理持っており、
    //プレイヤークラスは加えてデータのセーブ＆ロード
    //エネミークラスは攻撃ターンの進行を主に扱う。

    [SerializeField]
    protected int hp;
    protected int maxHp;

    //以下状態異常
    //状態異常は残りターンを表している。
    protected int poison;
    protected int burn;
    
    public int GetHp(){
        return hp;
    }

    public void SetHp(int input){
        this.hp = input;
    }

    public int GetMaxHp(){
        return maxHp;
    }

    public void SetMaxHp(int input){
        maxHp = input;
    }

    public int GetPoison(){
        return poison;
    }

    public void SetPoison(int input){
        this.poison = input;
    }

    public int GetBurn(){
        return burn;
    }

    public void SetBurn(int input){
        this.burn = input;
    }

    public bool IsDead(){
        if (hp <= 0){
            return true;
        } else {
            return false;
        }
    }

    
}


