using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //カードの扱いは、カードのデータをアクセスするまでは、IDのみで管理し、値を使用する直前にデータベースから情報を受け取る。
    //（容量を削減するため）
    //データにアクセスするタイミングは、カードを手札に引いたタイミングであり、それ以降はCardIndex型を使用する。

    //場にあるカードを格納する
    [SerializeField]
    CardIndex[] fieldCardIndex = new CardIndex[5];
    
    //選択したカードを格納する
    [SerializeField]
    List<CardIndex> selectCardIndex = new List<CardIndex>{};

    //プレイヤーの山札を格納する（テスト用）
    private List<int> deckCardId = new List<int>{1, 1, 2, 2, 3, 3, 4, 4};

    //プレイヤーの捨て札を格納する
    [SerializeField]
    List<int> discardCardId = new List<int>{};

    //カードボタンオブジェクトを格納する
    [SerializeField]
    GameObject[] cardButtons = new GameObject[5];

    //　カードボタンが選択されているか
    [SerializeField]
    bool[] isSelect = new bool[5];

    //CardIndexのリストを格納する用
    [SerializeField]
    private CardDataBase cardDataBase;

    private GameObject playerHp;

    void Awake() {
        playerHp = GameObject.Find("PlayerHp");
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++){
            isSelect[i] = false;
        }
    }

    //山札から手札に補充する処理
    public void GetCard(){
        for(int i = 0; i < fieldCardIndex.Length; i++){
            if(fieldCardIndex[i] == null){
                fieldCardIndex[i] = GenerateCard();
                CardController cardController = cardButtons[i].GetComponent<CardController>();
                cardController.ReloadCard(fieldCardIndex[i]);
            }
        }
    }

    //カードのドローのみする処理
    CardIndex GenerateCard(){

        int randNum = Random.Range(0, deckCardId.Count);
        int GenCardIndex = deckCardId[randNum];

        Debug.Log("出した値：" + randNum + "   引く前の山札の総数：" + deckCardId.Count);

        CardIndex GetCardIndex = cardDataBase.SearchCard(GenCardIndex);
        
        Debug.Log("カードを引きました");
        
        
        deckCardId.RemoveAt(randNum);
        
        //山札の補充(捨て札を山札にする)処理
        if(deckCardId.Count <= 0){
            deckCardId = discardCardId;
            discardCardId = new List<int>{};
            Debug.Log("山札が切れたので捨て札から補充します");
        }

        return GetCardIndex;
    }



    //カード選択する(引数には、fieldCardの要素番号を入れる）
    public void SelectCard(int select){
        CardIndex inputCard = fieldCardIndex[select];

        //カードが既に選択されている時の処理
        if (isSelect[select]){
            if (selectCardIndex[selectCardIndex.Count - 1] == inputCard){
                //一番最後に選択されている→選択キャンセル
                Debug.Log("選択をキャンセルしました");
                selectCardIndex.Remove(inputCard);
                isSelect[select] = false;
                cardButtons[select].GetComponent<CardController>().CancelCardAnimation();

            } else {
                //一番最後に選択されていない→選択キャンセルしない
                Debug.Log("選択をキャンセルできません");

            }
        } 



        //カードを一度も選択していない時の処理
        else {
            if(selectCardIndex.Count == 0){
                selectCardIndex.Add(inputCard);
                Debug.Log(inputCard.GetCardName() + "を選択しました");
                isSelect[select] = true;
                cardButtons[select].GetComponent<CardController>().SelectCardAnimation();

            } else {
                CardIndex beforeCard = selectCardIndex[selectCardIndex.Count - 1];
                if (beforeCard.GetEndDirection() == inputCard.GetStartDirection()){
                    selectCardIndex.Add(inputCard);
                    //終点と始点が一致している場合
                    Debug.Log(inputCard.GetCardName() + "を選択しました");
                    isSelect[select] = true;
                    cardButtons[select].GetComponent<CardController>().SelectCardAnimation();
                } else {
                    //終点と始点が一致していない場合
                    Debug.Log("始点と終点が一致していません。");
                }
            }
        }   
    }

    

    //使用したカードを捨てる処理
    //ガードして無効化されたカードも捨て札にしておく(これはゲームバランスとの兼ねあい)
    public void Discard(){
        for (int i = 0; i < isSelect.Length; i++){
            if(isSelect[i]){
                discardCardId.Add(cardDataBase.GetCardId(fieldCardIndex[i]));
                fieldCardIndex[i] = null;
                isSelect[i] = false;
                cardButtons[i].GetComponent<CardController>().CancelCardAnimation();
            }
        }
        selectCardIndex = new List<CardIndex>{};

    }



    //選択したカードの出力用（攻撃の処理はGameManagerで行うため）
    public List<CardIndex> GetSelectCardList(){
        return selectCardIndex;
    }

    //プレイヤーのて持ち札の入力用
    public void SetDeckCard(List<int> input){
        deckCardId = input;
    }



    public void DrawPlayerHp(int input){
        playerHp.GetComponent<Text>().text = "プレイヤーのHP：" + input.ToString();
    }
}
