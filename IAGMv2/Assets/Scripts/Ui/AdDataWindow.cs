using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static ADNpc;

public class AdDataWindow : MonoBehaviour
{
    public GameObject myADNpc;
    public TMP_Text[] Text = new TMP_Text[8];
    public Image ProfileImage;
    // â�� ǥ���� ������ �ؽ�Ʈ Ÿ�� �迭���� -> �� �ؽ�Ʈ ������Ʈ���� �ν����Ϳ��� ���ε� ���� by����

    private void Start()
    {
        GameObject obj = UIManager.Inst.ParentOBJ;
        myADNpc = obj.transform.GetChild(0).gameObject; // ��������Ʈ�� ù��° �ڽ� �������� ���� ���谡

        CharState.NPC adStat = myADNpc.GetComponent<ADNpc>().myStat;

        ProfileImage.sprite = adStat.profile;
        Text[0].text = "�̸�: " + adStat.name;
        Text[1].text = "����: " + adStat.npcJob.ToString();
        Text[2].text = adStat.charGrade.ToString();
        Text[3].text = "ü��: " + adStat.health;
        Text[4].text = "���ݷ�: " + adStat.attack;
        Text[6].text = "��ø: " + adStat.agility;
        Text[7].text = "����: " + adStat.intellect;
        Text[8].text = "����: " + adStat.dexterity;
        Text[9].text = "��: " + adStat.strong;
    }

    public void RemoveWinndow() // X��ư ������ â �ݱ�
    {
        SoundManager.Instance.ClickSound.Play();
        Destroy(gameObject);
    }
}