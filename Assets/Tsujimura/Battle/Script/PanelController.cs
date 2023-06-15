using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    

    public void SceneCangeButton(){
        FadeManager.Instance.LoadScene ("GameOverScene", 1.0f);
    }
}
