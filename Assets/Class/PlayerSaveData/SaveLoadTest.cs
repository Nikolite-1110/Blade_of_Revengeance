using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerSaveData testData = new PlayerSaveData();
        testData.ResetData();
        Debug.Log(testData.GetMaxHp());
        PlayerSaveData.SavingData(testData);
        Debug.Log("セーブしました。");

        PlayerSaveData loadData = PlayerSaveData.LoadingData();
        Debug.Log("取得した値：" + loadData.GetMaxHp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
