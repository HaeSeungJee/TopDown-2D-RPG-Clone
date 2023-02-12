using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffector : MonoBehaviour
{
    public int CharPerSeconds;
    public GameObject EndCursor;
    public bool isAnim;
    string targetMsg;
    Text msgText;
    AudioSource audioSource;
    int index;
    float interval;

    private void Awake()
    {
        msgText = GetComponent<Text>();    
        audioSource = GetComponent<AudioSource>();
    }
    public void SetMsg(string msg)
    {
        if (isAnim) {   // Interrupt
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        } else {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        // Start Animation
        interval = 1.0f / CharPerSeconds;

        isAnim = true;

        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        if (msgText.text == targetMsg) {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];
        index++;

        // Sound
        if (msgText.text.Length != targetMsg.Length) {
            if (targetMsg[index] != ' ' || targetMsg[index] != '.')
                audioSource.Play();
        }
        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        isAnim = false;
        EndCursor.SetActive(true);
    }
}
