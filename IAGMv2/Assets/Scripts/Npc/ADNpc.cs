using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using static ADNpc;
using static CharState;

public class ADNpc : MonoBehaviour
{

    public GameObject AI_Per;

    public int adtype;

    public Sprite[] NpcProfiles;
    public CharState.NPC myStat;
    int Str, Dex, Int, Agi, bStr, bDex, bInt, bAgi, Atk, bAtk, Hel, bHel, mainStat;
    float AD, AR;

    //������Ʈ
    Host _host;
    public CharState.NPC RandomNPC(int a, Sprite[] np)
    {
        CharState.NPC adnpc = new CharState.NPC();
        adnpc.profile = np[a]; // ������ �̹����� ���� a�� ���� �ٲ�Ƿ� �ش� �������� ����
        switch (a)
        {
            case 0: // ���� �ü�
                adnpc.profile = np[0];
                adnpc.name = "������";
                adnpc.npcJob = CharState.NPCJOB.ACHER;
                AR = 40.0f;
                AD = 1.0f;
                break;
            case 1: // ���� ����
                adnpc.profile = np[1];
                adnpc.name = "�糪";
                adnpc.npcJob = CharState.NPCJOB.THIEF;
                AR = 15.0f;
                AD = 2.0f;
                break;
            case 2: // ���� ������
                adnpc.profile = np[2];
                adnpc.name = "�ڸ���";
                adnpc.npcJob = CharState.NPCJOB.WIZARD;
                AR = 30.0f;
                AD = 1.0f;
                break;
            case 3: // ���� �ü�
                adnpc.profile = np[3];
                adnpc.name = "�긮��";
                adnpc.npcJob = CharState.NPCJOB.ACHER;
                AR = 40.0f;
                AD = 1.0f;
                break;
            case 4: // ���� ����
                adnpc.profile = np[4];
                adnpc.name = "����";
                adnpc.npcJob = CharState.NPCJOB.THIEF;
                AR = 15.0f;
                AD = 2.0f;
                break;
            case 5: // ���� ������
                adnpc.profile = np[5];
                adnpc.name = "Ŭ��";
                adnpc.npcJob = CharState.NPCJOB.WIZARD;
                AR = 30.0f;
                AD = 1.0f;
                break;
            case 6: // ���� ����
                adnpc.profile = np[6];
                adnpc.name = "����";
                adnpc.npcJob = CharState.NPCJOB.WARRIOR;
                AR = 15.0f;
                AD = 2.0f;
                break;
        }
        adnpc = Setstat(adnpc);
        return adnpc;
    }
    void Start()
    {
        NpcProfiles = Resources.LoadAll<Sprite>("Images/ProfileImages");
        myStat = RandomNPC(adtype, NpcProfiles); // ��ŸƮ���� ĳ���Ϳ��� ������ �־���
        _host = GetComponent<Host>();
        _host.myStat.RotSpeed = 360.0f;
        _host.myStat.MoveSpeed = 15.0f;
        _host.myStat.AttackRange = AR;
        _host.myStat.AttackDelay = AD;
        _host.myStat.AP = myStat.attack;
        _host.myStat.MaxHp = myStat.health;
        _host.myStat.HP = myStat.health;
        AI_Per.SetActive(false);
    }
    public CharState.NPC Setstat(CharState.NPC npc)
    {
        switch (npc.npcJob)
        {
            case CharState.NPCJOB.WARRIOR:
                Str = 10; Dex = 50; Int = 100; Agi = 20; //����ġ (�������� Ŀ��)
                bStr = 40; bDex = 20; bInt = 10; bAgi = 30; //�⺻��
                Atk = 5; bAtk = 10; Hel = 5; bHel = 30;
                break;
            case CharState.NPCJOB.ACHER:
                Str = 100; Dex = 10; Int = 50; Agi = 20;
                bStr = 10; bDex = 40; bInt = 20; bAgi = 30;
                Atk = 5; bAtk = 20; Hel = 5; bHel = 15;
                break;
            case CharState.NPCJOB.THIEF:
                Str = 50; Dex = 20; Int = 100; Agi = 10;
                bStr = 40; bDex = 20; bInt = 10; bAgi = 30;
                Atk = 5; bAtk = 20; Hel = 5; bHel = 20;
                break;
            case CharState.NPCJOB.WIZARD:
                Str = 100; Dex = 20; Int = 10; Agi = 50;
                bStr = 10; bDex = 30; bInt = 40; bAgi = 20;
                Atk = 5; bAtk = 30; Hel = 5; bHel = 10;
                break;
        }

        npc.strong = bStr + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / Str)), 0, 99999999); // 10�� 1�� ����
        npc.dexterity = bDex + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / Dex)), 0, 99999999); // 10�� 10�� ����
        npc.intellect = bInt + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / Int)), 0, 99999999); // 10�� 1�� ����
        npc.agility = bAgi + Mathf.Clamp(UnityEngine.Random.Range(0, (GameManager.Instance.Fame / Agi)), 0, 99999999); // 10�� 10������

        switch (npc.npcJob)
        {
            case CharState.NPCJOB.WARRIOR:
                mainStat = npc.strong;
                break;
            case CharState.NPCJOB.ACHER:
                mainStat = npc.dexterity;
                break;
            case CharState.NPCJOB.WIZARD:
                mainStat = npc.intellect;
                break;
            case CharState.NPCJOB.THIEF:
                mainStat = npc.agility;
                break;
        }

        npc.health = bHel + (mainStat / Hel);

        npc.attack = bAtk + (mainStat / Atk);
        npc.charGrade = Grade(mainStat);
        return npc;
    }

    public CharState.GRADE Grade(int main)
    {
        CharState.GRADE grade = new CharState.GRADE();
        if (main <= 140) // �⺻���� 40
        {
            grade = CharState.GRADE.F;
        }
        else if (main <= 640 && main > 140)
        {
            grade = CharState.GRADE.E;
        }
        else if (main <= 1640 && main > 640)
        {
            grade = CharState.GRADE.D;
        }
        else if (main <= 4640 && main > 1640)
        {
            grade = CharState.GRADE.C;
        }
        else if (main <= 9640 && main > 4640)
        {
            grade = CharState.GRADE.B;
        }
        else if (main > 9640)
        {
            grade = CharState.GRADE.A;
        }
        return grade;
    }
}


