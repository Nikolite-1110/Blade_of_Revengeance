using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
//CreateAssetMenu = エディット内のAssetから追加できる。
[CreateAssetMenu(fileName = "Enemy", menuName = "CreateEnemy")]
//ScriptableObject = 空のゲームオブジェクトにアタッチしなくても作用する。
//他のスクリプトでこのクラスを使いたいときは [EnemyIndex <変数名> = new EnemyIndex();] を使う。
public class EnemyIndex : ScriptableObject {

    //敵の名前
    [SerializeField]
    private string enemyName;

    //敵の情報（使わないかも）
    [SerializeField]
    private string information;

    //敵のHP
    [SerializeField]
    private int hp;

    //敵が攻撃するまでのターン数
    [SerializeField]
    private int delay;

    //敵の攻撃力
    [SerializeField]
    private int damage;

    //敵のアーマー値（使わないかも)
    [SerializeField]
    private int armor;

    //倒した時に落とすお金の量
    [SerializeField]
    private int dropMoney;

    //敵の画像
    [SerializeField]
    private Sprite icon;

    //各変数を取得するためのメソッド
    //※関数の取得をする際には、直接アクセスするのではなく、こちらのメソッドを使ってください。
    public string GetEnemyName(){
        return enemyName;
    }

    public string GetInformation(){
        return information;
    }

    public int GetHp(){
        return hp;
    }

    public int GetDelay(){
        return delay;
    }

    public int GetDamage(){
        return damage;
    }

    public int GetArmor(){
        return armor;
    }

    public int GetDropMoney(){
        return dropMoney;
    }

    public Sprite GetIcon(){
        return icon;
    }

    public void HitDamage(int damage){
        hp -= damage;
    }

    public void Action(){
        
    }
}