using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : MonoBehaviour, IPointerClickHandler
{
    //カード（を模したボタンUI）は使ったあとに削除するのではなく、
    //1~5のナンバーのあるボタンUIをそのまま使い回す。
    //（Prefabの生成・削除を防ぐによる軽量化を図る）

    //ボタンに描写するカードの情報を格納する。
    //(このID番号をもとに描写や処理を行う)

    [SerializeField]
    private CardIndex displayCard;

    //このボタンが右から見て何番目にあるかを示す。
    //この番号は、PlayerControllerのint型の配列「filedCardId」の要素番号に一致する。
    [SerializeField]
    private int ButtonNum;

    //カードデータベースを入れる
    [SerializeField]
    private CardDataBase cardDataBase;

    //カード描写用のスプライトたち
    [SerializeField]
    private Sprite startTopIcon;
    [SerializeField]
    private Sprite startButtomIcon;
    [SerializeField]
    private Sprite startLeftIcon;
    [SerializeField]
    private Sprite startRightIcon;
    [SerializeField]
    private Sprite startNoneIcon;

    [SerializeField]
    private Sprite endTopIcon;
    [SerializeField]
    private Sprite endButtomIcon;
    [SerializeField]
    private Sprite endLeftIcon;
    [SerializeField]
    private Sprite endRightIcon;
    [SerializeField]
    private Sprite endNoneIcon;

    [SerializeField]
    private GameObject topImage;
    [SerializeField]
    private GameObject bottomImage;
    [SerializeField]
    private GameObject leftImage;
    [SerializeField]
    private GameObject rightImage;
    [SerializeField]
    private GameObject text;

    private GameObject playerObj;
    private PlayerController playerController;



    // Start is called start the first frame update
    void Start()
    {
        playerObj = GameObject.Find("PlayerController");
        playerController = playerObj.GetComponent<PlayerController>();
    }



    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (GameManager.isButtleMode){
            playerController.SelectCard(ButtonNum);
        }
        
    }



    //カードを描写する処理
    public void DrawCard(){

        //それぞれの位置の初期化
        topImage.GetComponent<Image>().sprite = startNoneIcon;
        bottomImage.GetComponent<Image>().sprite = startNoneIcon;
        leftImage.GetComponent<Image>().sprite = startNoneIcon;
        rightImage.GetComponent<Image>().sprite = startNoneIcon;


        //始点・終点位置の描写 
        CardIndex.AttackDirection startDirection = displayCard.GetStartDirection();
        CardIndex.AttackDirection endDirection = displayCard.GetEndDirection();

        switch (startDirection){
            case CardIndex.AttackDirection.top:
                topImage.GetComponent<Image>().sprite = startTopIcon;
                break;
            case CardIndex.AttackDirection.bottom:
                bottomImage.GetComponent<Image>().sprite = startButtomIcon;
                break;
            case CardIndex.AttackDirection.left:
                leftImage.GetComponent<Image>().sprite = startLeftIcon;
                break;
            case CardIndex.AttackDirection.right:
                rightImage.GetComponent<Image>().sprite = startRightIcon;
                break;
            default:
                break;
        }

        switch (endDirection){
            case CardIndex.AttackDirection.top:
                topImage.GetComponent<Image>().sprite = endTopIcon;
                break;
            case CardIndex.AttackDirection.bottom:
                bottomImage.GetComponent<Image>().sprite = endButtomIcon;
                break;
            case CardIndex.AttackDirection.left:
                leftImage.GetComponent<Image>().sprite = endLeftIcon;
                break;
            case CardIndex.AttackDirection.right:
                rightImage.GetComponent<Image>().sprite = endRightIcon;
                break;
            default:
                break;
        }

        //カード名の反映
        text.GetComponent<Text>().text = displayCard.GetCardName();


    }
    


    //カードを更新する処理
    public void ReloadCard(CardIndex newCard){
        displayCard = newCard;

        DrawCard();
    }



    //カードを選択した時のアニメーション
    public void SelectCardAnimation(){
        text.GetComponent<Text>().color = Color.red;
    }



    //カード選択をキャンセルした時のアニメーション
    public void CancelCardAnimation(){
        
        text.GetComponent<Text>().color = Color.black;
    }
}
