using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
//CreateAssetMenu = エディット内のAssetから追加できる。
[CreateAssetMenu(fileName = "Card", menuName = "CreateCard")]
//ScriptableObject = 空のゲームオブジェクトにアタッチしなくても作用する。
//他のスクリプトでこのクラスを使いたいときは [CardIndex <変数名> = new CardIndex();] を使う。
public class CardIndex : GeneralClass {

    //カードタイプを指定する列挙型
    public enum CardType {
        SkillCard,
        ItemCard
    }

    //列挙型だけを他スクリプトで宣言して使いたいときは
    //[CardIndex.AttackDirection <変数名> = new CardIndex.AttackDirection()]もしくは、
    //[CardIndex.CardType <変数名> = new CardIndex.CardType()]を使う。

    //カード名
    [SerializeField]
    private string cardName;

    //カードのテキスト情報
    [SerializeField]
    private string information;

    //カードの種類
    [SerializeField]
    private CardType cardType;

    //攻撃回数
    [SerializeField]
    private int attackCount;

    //効果量（ダメージ/回復量など）
    //計算の時は、floatにキャストする場合がありそうなので注意！！
    [SerializeField]
    private int effect;

    //カードのアイコン
    [SerializeField]
    private Sprite icon;

    //攻撃始動向き(始点)
    [SerializeField]
    private AttackDirection startDirection;

    //攻撃終了向き(終点)
    [SerializeField]
    private AttackDirection endDirection;

    //各変数を取得するためのメソッド
    //※関数の取得をする際には、直接アクセスするのではなく、こちらのメソッドを使ってください。

    public string GetCardName(){
        return cardName;
    }

    public string GetInformation(){
        return information;
    }

    public CardType GetCardType(){
        return cardType;
    }

    public int GetAttackCount(){
        return attackCount;
    }

    public int GetEffect(){
        return effect;
    }

    public Sprite GetIcon(){
        return icon;
    }

    public AttackDirection GetStartDirection(){
        return startDirection;
    }

    public AttackDirection  GetEndDirection(){
        return endDirection;
    }

    public string GetStrStartDirection(){
        return startDirection.ToString();
    }

    public string GetStrEndDirection(){
        return endDirection.ToString();
    }
}

