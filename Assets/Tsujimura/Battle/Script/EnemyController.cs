using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class EnemyController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject hpGauge;
    GameObject attackDeray;


    // Start is called before the first frame update
    void Awake() {
        hpGauge = GameObject.Find("EnemyHpGauge");
        attackDeray = GameObject.Find("AttackDeray");
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameObject managerObj = GameObject.Find("GameManager");
        managerObj.GetComponent<GameManager>().SetIsButtonPush();
    }

    public void DrawHpGauge(int input, int max){
        float inputF = input;
        float maxF = max;
        Debug.Log(inputF / maxF);
        hpGauge.GetComponent<Image>().fillAmount = inputF / maxF;
    }

    public void DrawAttackDeray(int input){
        attackDeray.GetComponent<Text>().text = "敵の攻撃まで：" + input.ToString();
    }

    
}
