using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "CardDatabase")]
public class CardDataBase : ScriptableObject{
    //各種カード情報を格納するリスト(登録はインスペクター上で行います)
    [SerializeField]
    private List<CardIndex> cardLists = new List<CardIndex>();

    public List<CardIndex> GetCardLists(){
        return cardLists;
    }

    //欲しいカード情報をカード名から検索する場合（見つからない場合はNULLを返します。）
    public CardIndex SearchCard(string target){
        CardIndex searchCard = cardLists.Find(innerCard => innerCard.GetCardName() == target);
        if (searchCard != null){
            return searchCard;
        } else {
            Debug.Log("【データベース検索エラー】：カードが見つかりませんでした。 検索先：" + target);
            return null;
        }
        
    }

    //欲しいカード情報をid番号から検索する場合（見つからない場合はNULLを返します。）
    public CardIndex SearchCard(int id){
        try {
            return cardLists[id];
        } catch {
            Debug.Log("【データベース検索エラー】：ID番号が許容値を超えています。");
            return null;
        }
        
    }

    public int GetCardId(CardIndex target){
        int result = cardLists.IndexOf(target);
        return result;
    }

    //※メソッドのオーバーロードを行っているので、どちらのケースも同じメソッドを使えます。
}    

