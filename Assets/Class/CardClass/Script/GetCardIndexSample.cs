using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCardIndexSample : MonoBehaviour
{
    
    //CardIndexのリストを格納する用
    [SerializeField]
    private CardDataBase cardDataBase;

    void Start()
    {
        //CardDataBase内のSearchCardメソッドを使用して、欲しいデータを取ってくる(今回はテキスト情報(Information)を取ってきている)。
        //カード名から取ってくる場合
        //CardIndex getName = cardDataBase.SearchCard("縦切り");
        //Debug.Log("取得したカード情報：" + getName.GetInformation());
        
        //カードIDから取ってくる場合
        //CardIndex getId = cardDataBase.SearchCard(1);
        //Debug.Log("取得したカード情報：" + getId.GetInformation());
        //メソッドのオーバーロードを使用しているので、名前、IDの両方とも同じメソッドで実行できます。

        //ID番号を使用する場合
        //ID番号はメンバ変数としては用意せず、CardDataBase内の各要素番号を使用する。
        //CardDataBase内のcardListsはプライベート変数で外から直接IndexOf関数が使えないので、別途ゲッターを用意している。
        //CardIndex useId = cardDataBase.SearchCard(1);
        //Debug.Log("取得したカードの情報：" + cardDataBase.GetCardId(useId));

        //CardType・AttackDirectionにアクセスする場合
        //CardIndex.CardType cardType = cardDataBase.SearchCard("横切り").GetCardType();
        //Debug.Log(cardType);
    }

    
}
