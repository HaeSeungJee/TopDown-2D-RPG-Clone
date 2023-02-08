using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;

    public void Action(GameObject scanObj)
    {
        if (isAction) {     // Exit Action
            isAction = false;
        } else {            // Enter Action
            isAction = true;
            scanObject = scanObj;
            talkText.text = "[" + scanObject.name + "] 입니다.";
        }
        talkPanel.SetActive(isAction);
    }


    void Update()
    {
        
    }
}
