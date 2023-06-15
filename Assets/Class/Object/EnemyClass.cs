using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyClass : ObjectClass
{
    public enum DefenseType{
        none,
        guard,
        counter
    }

    public enum DefenseDirection{
        none,
        top,
        bottom,
        left,
        right,
        center
    }

    [SerializeField]    
    private int nowDeray;

    private int fullDeray;
    [SerializeField]
    private int damage;
    private int armor = 0;

    DefenseDirection defenseDirection;

    DefenseType defenseType;

    public void SetStatus(EnemyIndex input){
        maxHp = input.GetHp();
        hp = maxHp;

        fullDeray = input.GetDelay();
        nowDeray = fullDeray;
        damage = input.GetDamage();
    }

    public void Action(){
        nowDeray--;
    }

    public int GetNowDeray(){
        return nowDeray;
    }

    public void ResetDeray(){
        nowDeray = fullDeray;
    }

    public void AddArmor(int add){
        armor += add;
    }

    public int GetDamage(){
        return damage;
    }

    public void SetDefenseDirection(DefenseDirection changeDirection){
        defenseDirection = changeDirection;
    }

    public string GetDefenseDirection() {
        return defenseDirection.ToString();
    }

    public DefenseType GetDefenseType(){
        return defenseType;
    }
}
