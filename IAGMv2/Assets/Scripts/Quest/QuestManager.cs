using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static CharState;


//메모리 절약은 클래스(가비지 컬렉션 필요), 속도는 구조체(스택 오버플로우 발생 위험)
public class QuestManager : Singleton<QuestManager>
{
    public List<Quest.QuestInfo> RQlist = new List<Quest.QuestInfo>(); //진행X 퀘스트 목록
    //public List<Quest.QuestInfo> PQlist = new List<Quest.QuestInfo>(); //진행O 퀘스트 목록
    public List<Quest.QuestInfo> FQlist = new List<Quest.QuestInfo>(); //완료된 퀘스트 목록
    public GameObject RQuest; // 연결 위치
    public GameObject PQuest; // 연결 위치
    public GameObject FQuest; // 연결 위치
    GameObject RQ;
    GameObject PQ;
    GameObject FQ;
    TMP_Text grade;
    TMP_Text Name;
    TMP_Text info;
    TMP_Text reward;
    public Quest.QuestInfo npcQuest;

    List<Quest> questlist = new List<Quest>();
    //받은 퀘스트 목록 >> 마을 사람 아이콘 클릭 시
    public void PostedQuest(Quest.QuestInfo npc)
    {
        RQlist.Add(npc);
        SpawnManager.Instance.hcount++;

        GameObject RQ = Instantiate(Resources.Load("Prefabs/PostQuest"), RQuest.transform) as GameObject;
        RQ.GetComponentInChildren<QuestInformation>().Questname.text = npc.questname;

    }


    public void ProgressQuest(Quest.QuestInfo npc, GameObject host)
    {
        //SpawnManager.Instance.hcount--;
        SpawnManager.Instance.ADcount++;
        //PQlist.Add(npc);
        GameObject PQ = Instantiate(Resources.Load("Prefabs/ProgressQuest"), PQuest.transform) as GameObject; // PQ랑 호스트 
        PQ.GetComponent<QuestView>().host = host;
        host.GetComponent<Host>().Quest = PQ;
        PQ.GetComponentInChildren<QuestInformation>().Questname.text = npc.questname;

    }

    public void EndQuest(Quest.QuestInfo npc)
    {
        SpawnManager.Instance.ADcount--;
        FQlist.Add(npc);
        GameObject FQ = Instantiate(Resources.Load("Prefabs/FinishQuest"), FQuest.transform) as GameObject;
        FQ.GetComponentInChildren<QuestInformation>().Questname.text = npc.questname;
    }

    //모험가에게 줄 퀘스트 목록 >> 받은 퀘스트 목록에서 모험가에게 퀘스트 분배
    //완료된 퀘스트 목록 >> 모험가 재방문시 퀘스트 성공 실패 여부 확인에 따라 완료

    public void EndQuestClear()
    {
       
        FQlist.Clear();
        for (int i = 0; i < FQuest.transform.childCount; i++)
        {
            Destroy(FQuest.transform.GetChild(i).gameObject);
        }
    }
}
