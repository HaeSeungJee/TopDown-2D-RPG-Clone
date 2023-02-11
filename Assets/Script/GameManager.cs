using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Animator talkPanel;
    public Image portraitImg;
    public Animator portraitAmi;
    public Sprite prevPortrait;
    public TypeEffector talk;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;


    void Start()
    {
        Debug.Log(questManager.CheckQuest());    
    }

    public void Action(GameObject scanObj)
    {
        // Get Current Object
        scanObject = scanObj;
        ObjData objData = scanObj.GetComponent<ObjData>();
        Talk(objData.id, objData.isNPC);

        // Visible Talk for Action
        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNPC)
    {
        // Set Talk Data
        int questTalkIndex = 0;
        string talkData = "";
        if (talk.isAnim) {
            talk.SetMsg("");
            return;
        } else {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }


        // End Talk
        if (talkData == null) {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        // Continue Talk
        if (isNPC) {
            talk.SetMsg(talkData.Split(':')[0]);

            // Show Portrait
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
            // Animation Portrait
            if (prevPortrait != portraitImg.sprite) {
                portraitAmi.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
            
        } else {
            talk.SetMsg(talkData);

            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}
