using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class QuestInformation : MonoBehaviour // 해당 스크립트는 모험가와 퀘스트 요청서와 같이 사용됨
{
    public GameObject ParentOBJ;
    public GameObject ChildOBJ;
    public GameObject NewsBArea; // NewsBalloon이 자식으로 생성되는 UiCanvas안의 부모 오브젝트

    public Quest.QuestInfo myQuest;
    public TMP_Text Questname;
    public TMP_Text grade;
    public TMP_Text Name;
    public TMP_Text info;
    public TMP_Text reward;
    public TMP_Text Result;
    //string result;
    bool chk; // 성공 실패 여부 체크
    public GameObject myNpc; // 퀘스트를 준 npc
    public int People;
    public bool IsQuestchk;
    public bool NpcChk = true; // 해당 스크립트가 바인딩 되어있는 오브젝트가 모험가인지 아니면 Ui프리팹 인지 구분 (false=요청서/true=모험가)

    GameObject WindowArea; // AdDataWindow가 자식으로 생성되는 UiCanvas안의 부모 오브젝트

    AdDataWindow ADW;
    Host _childHost;

    public void Start()
    {
        WindowArea = UIManager.Inst.WindowArea;
        //UI매니저
        if (NpcChk == false || this.gameObject.GetComponent<Host>().purpose == 0) // Ui프리팹일 경우
        {
            ParentOBJ = UIManager.Inst.ParentOBJ;
            if (ParentOBJ.transform.childCount > 0)
            {
                ChildOBJ = ParentOBJ.transform.GetChild(0).gameObject;
                _childHost = ChildOBJ.GetComponent<Host>();
            }
        }
        NewsBArea = UIManager.Inst.NewsBArea;

    }
    public void ShowQuest(Quest.QuestInfo npc, GameObject host)
    {
        grade.text = npc.questgrade.ToString();
        Name.text = "[" + npc.questname + "]";
        if (IsQuestchk)
        {
            //모험가 보고서 용
            int rnd = Random.Range(0, 10);
            if (host.GetComponent<QuestInformation>().myQuest.questname == "채집" ? (rnd > 5) : host.GetComponent<Host>().myStat.HP != 0)
            {
                chk = true;
            }
            else
            {
                chk = false;
            }
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Tutorial2")
            {
                chk = true;
            }
            //보고서 용
            Result.text = chk ? "[성공]" : "[실패]"; // 성공 실패 여부 체크후 변경
        }
        else
        {
            //퀘스트 신청용
            info.text = "\"" + npc.information + "\"";
        }
        reward.text = "[골드 : " + npc.rewardgold.ToString() + "G]" + "\n" + "[평판 : " + npc.rewardfame.ToString() + "P]";
    }


    public void AddQuest() // 승낙
    {
        SoundManager.Instance.ClickSound.Play();
        if (SpawnManager.Instance.EndTime < TimeManager.Instance.DeadLine)
        {
            if (People == 0) // 마을사람
            {
                QuestManager.Instance.PostedQuest(myQuest);
                GameObject obj = Instantiate(Resources.Load("UiPrefabs/NewsBalloon"), NewsBArea.transform) as GameObject;
                obj.GetComponent<NewsBalloon>().SetText("새로운 퀘스트가 \n추가되었습니다.");
            }
            else // 모험가
            {
                QuestManager.Instance.ProgressQuest(myQuest, ChildOBJ);
                _childHost.Questing = true;
            }
            _childHost.onSmile = true;
        }
        else
        {
            if (People != 0)
            {
                QuestManager.Instance.PostedQuest(myQuest);
            }
            //메시지 출력
            GameObject obj = Instantiate(Resources.Load("UiPrefabs/NewsBalloon"), NewsBArea.transform) as GameObject;
            obj.GetComponent<NewsBalloon>().SetText("마감시간에는 \n새로운 퀘스트를 \n추가할 수 없습니다.");
            _childHost.onAngry = true;
        }
    }

    public void onDestroy()
    {
        if (ADW != null)
        {
            Destroy(ADW.gameObject);
        }
        Destroy(gameObject);
    }

    public void onQuestRfuse()
    {
        SoundManager.Instance.ClickSound.Play();
        _childHost.onAngry = true;
        if (People != 0)
        {
            QuestManager.Instance.PostedQuest(myQuest);
            GameObject obj = Instantiate(Resources.Load("UiPrefabs/NewsBalloon"), NewsBArea.transform) as GameObject;
            obj.GetComponent<NewsBalloon>().SetText("퀘스트가 \n반환되었습니다.");
        }
    }
    public void AddReward()
    {
        _childHost.Questing = false;
        Destroy(ChildOBJ.GetComponent<Host>().Quest.gameObject);
        QuestManager.Instance.EndQuest(myQuest);
        if (chk)
        { // 성공시 증가 연산
            SoundManager.Instance.MoneySound.Play();
            GameManager.Instance.ChangeGold(myQuest.rewardgold);
            GameManager.Instance.ChangeFame(myQuest.rewardfame);
            _childHost.onSmile = true;
        }
        else
        { // 실패시 감소 연산
            GameManager.Instance.ChangeGold(-myQuest.rewardgold);
            if (SpawnManager.Instance.EndTime >= TimeManager.Instance.DeadLine)
            {
                GameManager.Instance.ChangeFame(-myQuest.rewardfame); // 마감시간에 퀘스트 실패하고 돌아왔는데 명성이 안까임 - 종찬(임시로 고침)
            }
            _childHost.onAngry = true;
        }
        //현재 퀘스트 리스트를 완료 퀘스트 리스트에 추가
        Destroy(gameObject); // 확인시 연산 후 페이지 삭제

    }
    /*public void onNewsBalloon() // 뉴스말풍선 생성 "새로운 퀘스트가 추가되었습니다."
    {
        GameObject obj = Instantiate(Resources.Load("UiPrefabs/NewsBalloon"), NewsBArea.transform) as GameObject;
        obj.GetComponent<NewsBalloon>().SetText("새로운 퀘스트가 \n추가되었습니다.");
    }*/
    public void onAdDataWindow() // 모험가 정보창 띄우기
    {
        SoundManager.Instance.ClickSound.Play();
        if (ADW == null)
        {
            GameObject obj = Instantiate(Resources.Load("UiPrefabs/AdDataWindow"), WindowArea.transform) as GameObject;
            ADW = obj.GetComponent<AdDataWindow>();
        }
    }
}
