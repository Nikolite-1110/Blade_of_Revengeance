using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//   << このスクリプトの記述範囲  >>
// このスクリプトは、戦闘の進行を主に行います。
// 具体的には、GameEngineコルーチンを中心に、戦闘を行う上で必要な処理（ダメージや死亡処理など）を記述します。

//   << クラスとデータベースの扱いについて >>
// 今回の記述の考え方としては,データベースやセーブデータは、あくまで参照用であるため、
// そのインスタンスを直接操作するのではなく、一度別インスタンスに値を移してからそのインスタンス内で操作する流れにしてある。
// (EnemyIndex => EnemyClass,  PlayerSaveData => PlayerClass)


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CardDataBase cardDataBase;
    //敵のデータベース参照用
    [SerializeField]
    private EnemyDataBase enemyDataBase;



    //プレイヤー関連
    //プレイヤーオブジェクト
    private GameObject player;
    //プレイヤーコントローラのスクリプト
    private PlayerController playerController;
    //プレイヤーのクラス;
    [SerializeField]
    private PlayerClass playerClass;
    //プレイヤーのセーブデータ
    private PlayerSaveData saveData;



    //敵関連
    //戦う敵のID
    private int enemyId = 0;
    //敵オブジェクト
    private GameObject enemy;
    //エネミーコントローラのスクリプト
    private EnemyController enemyController;
    //敵情報のデータベース(もしかしたら、IDのみの管理にするかも)
    private EnemyIndex enemyData;
    //敵のクラス
    [SerializeField]
    private EnemyClass enemyClass;



    //その他バトル用の変数等
    //現ターン数の保存用
    [SerializeField]
    private int battleTurn;
    // ターン数表示ゲームオブジェクト
    private GameObject turnText;
    //カード選択が可能かどうか
    public static bool isButtleMode;
    //攻撃対象の選択待ちをするためのやつ（コルーチンに使う）
    [SerializeField]
    private bool isButtonPush = false;
    //クリアパネル用のオブジェクト
    //うまくアタッチできなかったので、とりあえずシリアル化して代入します
    [SerializeField]
    private GameObject clearPanel;
    [SerializeField]
    //もらえるお金を表示するオブジェクト
    //パネル同様うまくいかなかったので、とりあえずシリアル化
    private GameObject moneyText;

    private void Awake() {
        //デバッグ用の仮セーブデータ作成
        PlayerSaveData debugData = new PlayerSaveData();
        debugData.ResetData();
        PlayerSaveData.SavingData(debugData);
    }


    // Start is called before the first frame update
    void Start()
    {
        //値の初期化
        player = GameObject.Find("PlayerController");
        playerController = player.GetComponent<PlayerController>();

        enemy = GameObject.Find("Enemy");
        enemyController = enemy.GetComponent<EnemyController>();

        turnText = GameObject.Find("TurnText");


        playerClass = new PlayerClass();
        enemyClass = new EnemyClass();

        battleTurn = 0;
        isButtleMode = true;
        enemyData = enemyDataBase.SearchEnemy(enemyId);
        enemyClass.SetStatus(enemyData);

        saveData = PlayerSaveData.LoadingData();
        playerClass.SetStatus(saveData);
        playerController.SetDeckCard(saveData.GetCardList());

        //敵の描写(初回)
        enemyController.DrawHpGauge(enemyClass.GetHp(), enemyClass.GetMaxHp());
        enemyController.DrawAttackDeray(enemyClass.GetNowDeray());

        //プレイヤーの描写（主にHP）
        playerController.DrawPlayerHp(playerClass.GetHp());

        StartCoroutine("BattleEngine");
        Debug.Log("ButteEngineをよびました");
    }



    //ゲームの進行に関するプログラム
    IEnumerator BattleEngine(){
        Debug.Log("ButtleEngileを起動しました");
        while(true){
            TurnStart();

            //論理型isButtonPusuを使って、カード入力まで待機
            //isButtonPushの変更は下の方にセッターがあります
            yield return new WaitUntil(() => isButtonPush == true);

            //ここから先カード選択後の処理
            isButtonPush = false;

            UseCard(enemyClass);

            if(enemyClass.IsDead()){
                //戦闘勝利処理
                Debug.Log("戦闘に勝利しました");
                
                clearPanel.SetActive(true);
                int money = saveData.GetHaveMoney();
                //もらえるお金の計算
                int addmoney = enemyData.GetDropMoney() + UnityEngine.Random.Range(-5, 6);
                DrawMoneyText(addmoney);
                saveData.SetHaveMoney(money + addmoney);
                PlayerSaveData.SavingData(saveData);
                yield break;
            }

            enemyClass.Action();
            enemyController.DrawAttackDeray(enemyClass.GetNowDeray());

            //敵の攻撃猶予が0になったら
            if(enemyClass.GetNowDeray() <= 0){
                DealDamage(playerClass, enemyClass);
                playerController.DrawPlayerHp(playerClass.GetHp());

                enemyClass.ResetDeray();
            }

            if(playerClass.IsDead()){
                //戦闘敗北処理
                Debug.Log("戦闘に敗北しました");
                FadeManager.Instance.LoadScene ("GameOverScene", 2.0f);
                yield break;
            }

            TurnEnd();
        }
    }



    //プレイヤーから敵に与える処理
    private void DealDamage(EnemyClass target, CardIndex attackCard){
        int enemyHp = target.GetHp();
        enemyHp -= attackCard.GetEffect();
        target.SetHp(enemyHp);
        Debug.Log("敵残りHP： " + enemyHp);
    }



    //敵からプレイヤーにダメージを与える処理
    private void DealDamage(PlayerClass target, EnemyClass attackEnemy) {
        int playerHp = target.GetHp();
        playerHp -= attackEnemy.GetDamage();
        target.SetHp(playerHp);
        Debug.Log("プレイヤー残りHP：" + playerHp);
    }



    //カードの使用処理
    private void UseCard(EnemyClass target){
        List<CardIndex> useCard = playerController.GetSelectCardList();
        for (int i = 0; i < useCard.Count; i++) {
            if (target.GetDefenseDirection() != useCard[i].GetStrStartDirection()){
                DealDamage(target, useCard[i]);

            } else {
                switch(target.GetDefenseType()){
                    //ガード処理
                    case EnemyClass.DefenseType.guard:
                        Debug.Log("攻撃をガードしました");
                        break;

                    //カウンター処理
                    case EnemyClass.DefenseType.counter:
                        Debug.Log("攻撃をカウンターしました");
                        DealDamage(playerClass, target);
                        break;
                }
                //攻撃中止処理(ガードかカウンター成立)
                Debug.Log("攻撃を防ぎました。");
                //ガードかカウンターが成立すると、コンボを止める。
                break;
            }
            if (target.GetHp() >= 0){
                enemyController.DrawHpGauge(target.GetHp(), target.GetMaxHp());
            } else {
                //オーバーキル処理
                //エフェクトとか
            }
            
        }
    }



    //ターン開始処理
    //主に状態異常に関する処理を書けるようにするスペース
    private void TurnStart(){
        battleTurn += 1;
        playerController.GetCard();
        DrawTurnText(battleTurn);
    }



    //ターン終了処理
    private void TurnEnd(){
        playerController.Discard();
    }


    public void DrawTurnText(int input){
        turnText.GetComponent<Text>().text = "現在：" + input.ToString() + "ターン目";
    }

    public void DrawMoneyText(int input){
        moneyText.GetComponent<Text>().text = input.ToString() + "円取得!!";
    }



    //
    public bool GetIsButtonPush(){
        return isButtonPush;
    }



    public void SetIsButtonPush(){
        isButtonPush = true;
    }
}
