using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "EnemyDatabase")]
public class EnemyDataBase : ScriptableObject {

    [SerializeField]
    private List<EnemyIndex> enemyLists = new List<EnemyIndex>();

    public List<EnemyIndex> GetEnemyLists(){
        return enemyLists;
    }

    //欲しい敵情報を名前から検索する場合（見つからない場合はNULLを返します。）
    public EnemyIndex SearchEnemy(string target){
        EnemyIndex searchEnemy = enemyLists.Find(innerEnemy => innerEnemy.GetEnemyName() == target);
        if (searchEnemy != null){
            return searchEnemy;
        } else {
            Debug.Log("【データベース検索エラー】：カードが見つかりませんでした。");
            return null;
        }
        
    }

    //欲しい敵情報をid番号から検索する場合（見つからない場合はNULLを返します。）
    public EnemyIndex SearchEnemy(int id){
        try {
            return enemyLists[id];
        } catch {
            Debug.Log("【データベース検索エラー】：ID番号が許容値を超えています。");
            return null;
        }
        
    }

    //※メソッドのオーバーロードを行っているので、どちらのケースも同じメソッドを使えます。

}
