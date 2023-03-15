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
    // 창에 표시할 내용의 텍스트 타입 배열선언 -> 각 텍스트 오브젝트들을 인스펙터에서 바인딩 해줌 by주현

    private void Start()
    {
        GameObject obj = UIManager.Inst.ParentOBJ;
        myADNpc = obj.transform.GetChild(0).gameObject; // 스폰포인트의 첫번째 자식 오브제가 나의 모험가

        CharState.NPC adStat = myADNpc.GetComponent<ADNpc>().myStat;

        ProfileImage.sprite = adStat.profile;
        Text[0].text = "이름: " + adStat.name;
        Text[1].text = "직업: " + adStat.npcJob.ToString();
        Text[2].text = adStat.charGrade.ToString();
        Text[3].text = "체력: " + adStat.health;
        Text[4].text = "공격력: " + adStat.attack;
        Text[6].text = "민첩: " + adStat.agility;
        Text[7].text = "지력: " + adStat.intellect;
        Text[8].text = "재주: " + adStat.dexterity;
        Text[9].text = "힘: " + adStat.strong;
    }

    public void RemoveWinndow() // X버튼 누르면 창 닫기
    {
        SoundManager.Instance.ClickSound.Play();
        Destroy(gameObject);
    }
}
