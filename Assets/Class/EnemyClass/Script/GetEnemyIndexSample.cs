using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnemyIndexSample : MonoBehaviour
{
    //EnemyIndexのリストを格納する用
    [SerializeField]
    private EnemyDataBase enemyDataBase;

    void Start()
    {
        //EnemyDataBase内のSearchEnemyメソッドを使用して、欲しいデータを取ってくる(今回はテキスト情報(Information)を取ってきている)。
        //カード名から取ってくる場合
        EnemyIndex getName = enemyDataBase.SearchEnemy("test2");
        Debug.Log("取得した敵情報：" + getName.GetInformation());
        //カードIDから取ってくる場合
        EnemyIndex getId = enemyDataBase.SearchEnemy(1);
        Debug.Log("取得した敵情報：" + getId.GetInformation());
        //メソッドのオーバーロードを使用しているので、名前、IDの両方とも同じメソッドで実行できます。
    }
}
