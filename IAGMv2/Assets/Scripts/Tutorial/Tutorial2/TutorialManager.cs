using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] Dialogues;
    public GameObject NoTouch;
    public GameObject NextBtn;
    public GameObject FadeOut;
    public GameObject QuestListWindow;
    public Transform PQ_Content;
    public Transform SpawnPointQ;
    public Transform QuestZone_F;
    public Transform QuestZone_M;
    public Animator Mask;
    public Animator CamManager;

    Tutorial2Dialogue myDialogue;
    public GameObject Host;
    int Parents_Num = 0; // Dialogue 순서 (부모)
    int Child_Num = 0; // Dialogue안의 대화 순서 (자식)
    bool TalkEnd;
    bool UserTurn = false;
    bool CamMove = false;
    float wait = 2.0f;

    Host.STATE hostExit = (Host.STATE)8;
    Host.STATE hostBattle = (Host.STATE)7;
    Host.STATE hostFarming = (Host.STATE)9;


    public enum STATE
    {
        CreateScene, VL_Moving, VL_Wait, VL_Wait2, VL_Leave, VL_Leave2, Q_AD_Moving, Q_AD_Wait, Q_AD_Questing, Q_AD_Questing2, Q_AD_ReGuild, Q_AD_Leave, P_AD_Wait, P_AD_Leave, M_AD_Leave
    }
    public STATE myState = STATE.CreateScene;

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        UserTurn = false;
        Dialogues[Parents_Num].SetActive(true);
        StartCoroutine(NextDialogue());

        switch (myState)
        {
            case STATE.VL_Leave2:
                Mask.SetTrigger("M_QuestList");
                break;
            case STATE.Q_AD_Questing:
                PQ_Content.GetChild(0).GetComponent<QuestView>().myButton.onClick.AddListener(QuestClick);
                Mask.SetTrigger("M_QuestList");
                break;
            case STATE.Q_AD_Questing2:
                Mask.SetTrigger("M_QuestList");
                break;
            case STATE.Q_AD_Leave:
                Mask.gameObject.SetActive(false);
                SpawnNpc("P_AD");
                break;
            case STATE.P_AD_Wait:
                Mask.SetTrigger("M_AD");
                CamManager.SetTrigger("PubCam");
                Mask.gameObject.SetActive(true);
                break;
            case STATE.P_AD_Leave:
                CamManager.SetTrigger("MotelCam");
                SpawnNpc("M_AD");
                break;
            case STATE.M_AD_Leave:
                CamManager.SetTrigger("LobbyCam");
                Mask.gameObject.SetActive(false);
                break;

        }
    }

    public void QuestClick()
    {
        Time.timeScale = 1.0f;
        CamMove = !CamMove;
        if(CamMove == true)
        {
            Mask.SetTrigger("M_AD");
        }
        else if(CamMove == false)
        {
            Mask.SetTrigger("M_Lobby");
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.VL_Moving:
                // 시계 아이콘이 나타나면
                if (Host != null)
                {
                    if (Host.GetComponent<Host>().Clockchk == true) 
                    {
                        ChangeState(STATE.VL_Wait);
                    }
                }
                break;
            case STATE.VL_Wait:
                // 퀘스트 아이콘이 나오면
                if (Host.GetComponent<Host>().Icon != null)
                {
                    if (Host.GetComponent<Host>().Icon.GetComponent<QuestIcon>()) 
                    {
                        ChangeState(STATE.VL_Wait2);
                    }
                }
                break;
            case STATE.VL_Wait2:
                // 게시된 퀘스트에 퀘스트가 추가되면 (= 수락하기를 누르면)
                if(QuestManager.Instance.RQlist.Count > 0)
                {
                    ChangeState(STATE.VL_Leave);
                }
                break;
            case STATE.VL_Leave:
                //퀘스트목록창이 활성화 되면
                if(QuestListWindow.active == true)
                {
                    ChangeState(STATE.VL_Leave2);
                }
                break;
            case STATE.Q_AD_Moving:
                //모험가의 시계가 나타나면
                if (Host != null)
                {
                    if (Host.GetComponent<Host>().Clockchk == true)
                    {
                        ChangeState(STATE.Q_AD_Wait);
                    }
                }
                break;
            case STATE.Q_AD_Wait:
                // 모험가가 퀘스트 존으로가서 배틀 상태면
                if (Host.GetComponent<Host>().myState == hostBattle || Host.GetComponent<Host>().myState == hostFarming)
                {
                    ChangeState(STATE.Q_AD_Questing);
                }
                break;
            case STATE.Q_AD_Questing:
                // 캠이 퀘스트 존으로 갔을 때
                if (CamMove)
                {
                    if(SpawnPointQ.childCount > 0) // 모험가가 길드로 갔을 때
                    {
                        ChangeState(STATE.Q_AD_Questing2);
                    }
                }
                break;
            case STATE.Q_AD_Questing2:
                // 퀘스트를 클릭했을 때
                if (!CamMove)
                {
                    ChangeState(STATE.Q_AD_ReGuild);
                }
                break;
            case STATE.Q_AD_ReGuild:
                // 퀘스트가 끝났을 때(= 종료된 퀘스트에 퀘스트가 추가되었을 때)
                if(QuestManager.Instance.FQlist.Count > 0)
                {
                    ChangeState(STATE.Q_AD_Leave);
                }
                break;
            case STATE.Q_AD_Leave:
                // 펍 모험가의 시계가 나왔을 때
                if (Host != null)
                {
                    if (Host.GetComponent<Host>().Clockchk == true)
                    {
                        ChangeState(STATE.P_AD_Wait);
                    }
                }
                break;
            case STATE.P_AD_Wait:
                // 펍 모험가의 식사가 끝났을 때
                if (Host.GetComponent<Host>().myState == hostExit)
                {
                    wait -= Time.deltaTime;
                    if(wait < 0)
                    {
                        wait = 2.0f;
                        ChangeState(STATE.P_AD_Leave);
                    }
                }
                break;
            case STATE.P_AD_Leave:
                // 모텔 모험가의 수면이 끝났을 때
                if (Host.GetComponent<Host>().myState == hostExit)
                {
                    wait -= Time.deltaTime;
                    if (wait < 0)
                    {
                        wait = 2.0f;
                        ChangeState(STATE.M_AD_Leave);
                    }
                }
                break;
        }
    }

    void Start()
    {
        Dialogues[Parents_Num].SetActive(true);
        StartCoroutine(NextDialogue());
    }

    void Update()
    {
        StateProcess();

        if (Input.anyKeyDown)
        {
            if(!UserTurn)
            {
                SoundManager.Instance.ClickSound.Play();
                if (TalkEnd == true)
                {
                    if (Child_Num < myDialogue.dialogue.Length - 1)
                    {
                        Child_Num++;
                        StartCoroutine(NextDialogue());
                    }
                    else
                    {
                        Dialogues[Parents_Num].SetActive(false);

                        Parents_Num++;
                        Child_Num = 0;
                        if(myState == STATE.Q_AD_Questing || myState == STATE.Q_AD_Questing2)
                        {
                            Time.timeScale = 0.0f;
                        }
                        else
                        {
                            Time.timeScale = 1.0f;
                        }
                        NextBtn.SetActive(false);
                        UserTurn = true;
                    }
                    EventPaly();
                }
                else if (TalkEnd == false) // 대사 스킵
                {
                    StopAllCoroutines();
                    myDialogue.Talk.text = null;
                    myDialogue.Talk.text = myDialogue.dialogue[Child_Num].dialogue;
                    TalkEndSet();
                }
            }
        }
    }

    public void EventPaly()
    {
        if (Parents_Num == 0 && Child_Num == 2) // 대화 0 - 2
        {
            Mask.gameObject.SetActive(true);
            MaskControl("M_Lobby");
            SpawnNpc("VL");
            ChangeState(STATE.VL_Moving);
        }
        else if (Parents_Num == 3 && Child_Num == 1) // 대화 3 - 1
        {
            MaskControl("M_MenuIcon");
        }
        else if (Parents_Num == 4 && Child_Num == 2) // 대화 3 - 1
        {
            MaskControl("M_Lobby");
            SpawnNpc("Q_AD");
            ChangeState(STATE.Q_AD_Moving);
        }
        else if (Parents_Num == 12 && Child_Num == 1) // 대화 12 - 1
        {
            FadeOut.SetActive(true);
        }
    }

    IEnumerator NextDialogue()
    {
        TalkStartSet();

        Time.timeScale = 0.0f;
        myDialogue = Dialogues[Parents_Num].GetComponent<Tutorial2Dialogue>();
        for (int i = 0; i < myDialogue.dialogue[Child_Num].dialogue.Length; ++i)
        {
            SoundManager.Instance.TypingSound.Play();
            myDialogue.Talk.text = myDialogue.dialogue[Child_Num].dialogue.Substring(0, i + 1);
            yield return new WaitForSecondsRealtime(0.1f);
        }

        TalkEndSet();
    }

    public void TalkStartSet()
    {
        TalkEnd = false;
        NextBtn.SetActive(false);
        NoTouch.SetActive(true);
    }

    public void TalkEndSet()
    {
        TalkEnd = true;
        NextBtn.SetActive(true);
        NoTouch.SetActive(false);
    }

    public void MaskControl(string TriggerName)
    {
        Mask.SetTrigger(TriggerName);
    }

    public void SpawnNpc(string npc)
    {
        switch (npc)
        {
            case "VL":
                Host = SpawnManager.Instance.Host = Instantiate(SpawnManager.Instance.VL[0], SpawnManager.Instance.spawnPoints[0]) as GameObject;
                SpawnManager.Instance.Host.GetComponent<VLNpc>().job = VLNpc.NPCJOB.COMMONS;
                SpawnManager.Instance.Host.GetComponent<Host>().VLchk = true;
                SpawnManager.Instance.Host.GetComponent<Host>().purpose = 0;
                break;
            case "Q_AD":
                Host = SpawnManager.Instance.Host = Instantiate(SpawnManager.Instance.AD[0], SpawnManager.Instance.spawnPoints[0]) as GameObject;
                int j = UnityEngine.Random.Range(0, QuestManager.Instance.RQlist.Count);
                SpawnManager.Instance.Host.GetComponent<QuestInformation>().myQuest = QuestManager.Instance.RQlist[j];
                QuestManager.Instance.RQlist.RemoveAt(j);
                Destroy(QuestManager.Instance.RQuest.transform.GetChild(j).gameObject);
                SpawnManager.Instance.Host.GetComponent<ADNpc>().adtype = 0;
                SpawnManager.Instance.Host.GetComponent<Host>().VLchk = false;
                SpawnManager.Instance.Host.GetComponent<Host>().People = 1;
                break;
            case "P_AD":
                Host = SpawnManager.Instance.Host = Instantiate(SpawnManager.Instance.AD[1], SpawnManager.Instance.spawnPoints[1]) as GameObject;
                SpawnManager.Instance.Host.GetComponent<ADNpc>().adtype = 0;
                SpawnManager.Instance.Host.GetComponent<Host>().VLchk = false;
                SpawnManager.Instance.Host.GetComponent<Host>().People = 1;
                SpawnManager.Instance.Host.GetComponent<Host>().purpose = 1;
                break;
            case "M_AD":
                Host = SpawnManager.Instance.Host = Instantiate(SpawnManager.Instance.AD[2], SpawnManager.Instance.spawnPoints[2]) as GameObject;
                SpawnManager.Instance.Host.GetComponent<ADNpc>().adtype = 0;
                SpawnManager.Instance.Host.GetComponent<Host>().VLchk = false;
                SpawnManager.Instance.Host.GetComponent<Host>().People = 1;
                SpawnManager.Instance.Host.GetComponent<Host>().purpose = 2;
                break;
        }
    }
}
