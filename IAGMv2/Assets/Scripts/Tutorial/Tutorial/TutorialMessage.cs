using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMessage : MonoBehaviour
{

    public TMP_Text Tu_txt;
    public TMP_Text Tu2_txt;
    public Image TutorialBG;
    public GameObject EyeOpenScene;
    public Animator EyeMask;
    public GameObject Tutorial;
    public GameObject NextIcon;
    public GameObject Dialogue;
    public float typing_Speed = 0.2f;
    public string Tu_typing = "아이고 힘들다.. 오늘도 야근이네";
    public string Tu2_typing = "여기가 어디지..? 차에 치였던것 같은데.. 병원인가?";

    public bool SceneCheck = true;

    public enum STATE
    {
        CreateScene, Scene1, Scene2, Scene3, Scene4, Scene5
    }

    public STATE myState = STATE.CreateScene;

    // Start is called before the first frame update
    void Start()
    {
        SoundList.Inst.PlaySound(0);
        ChangeState(STATE.Scene1);
    }

    void Update()
    {
        StateProcess();
    }

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch(myState)
        {
            case STATE.CreateScene:
                break;
            case STATE.Scene1:
                Tutorial.SetActive(true);
                EyeOpenScene.SetActive(false);
                StartCoroutine(_typing());
                break;
            case STATE.Scene2:
                SoundList.Inst.PlaySound(2);
                Tutorial.SetActive(false);
                EyeOpenScene.SetActive(true);
                StartCoroutine(Accident(Tu2_txt, Tu2_typing, typing_Speed));
                break;
            case STATE.Scene3:
                NextIcon.SetActive(true);
                break;
            case STATE.Scene4:
                SoundManager.Instance.ClickSound.Play();
                NextIcon.SetActive(false);
                Tu2_txt.text = "";
                break;
            case STATE.Scene5:
                EyeMask.SetTrigger("IsEyeOpen2");
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.CreateScene:
                break;
            case STATE.Scene1:
                if(SceneCheck == false)
                {
                    ChangeState(STATE.Scene2);
                }
                break;
            case STATE.Scene2:
                if (SceneCheck == true)
                {
                    ChangeState(STATE.Scene3);
                }
                break;
            case STATE.Scene3:
                if (Input.anyKeyDown)
                {
                    ChangeState(STATE.Scene4);
                    Dialogue.SetActive(true);
                }
                break;
            case STATE.Scene4:
                break;
        }
    }
    IEnumerator _typing() //글자 타이핑 효과
    {
        yield return new WaitForSecondsRealtime(0.5f);
        for (int i = 0; i <= Tu_typing.Length; ++i)
        {
            SoundManager.Instance.TypingSound.Play();
            Tu_txt.text = Tu_typing.Substring(0, i);
            yield return new WaitForSecondsRealtime(0.2f);
        }
        yield return new WaitForSecondsRealtime(2.0f);
        Tu_txt.text = " ";
        SoundManager.Instance.SoundList.Stop();
        TutorialBG.color = Color.red;
        SoundList.Inst.PlaySound(1);
        yield return new WaitForSecondsRealtime(6.0f);
        SceneCheck = false;
    }

    IEnumerator Accident(TMP_Text typingText, string message, float speed)
    {
       for (int i = 0; i < message.Length; i++)
        {
            SoundManager.Instance.TypingSound.Play();
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSecondsRealtime(speed);
        }
        SceneCheck = true;
    }

}

   