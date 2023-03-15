using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.ComponentModel;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[Serializable]
public class UIManager : MonoBehaviour
{
    public static UIManager Inst = null;

    //10억부터 벗어남
    public TMP_Text Gold;
    public TMP_Text Fame;

    public bool ChangeGold = false; // 골드의 변화를 감지
    public bool ChangeFame = false;
    public bool ExtendFail = false;
   
    public GameObject GoldIncrease; // 골드의 증감
    public GameObject FameIncrease; // 명성의 증감

    public GameObject NoticeWindow;
    public GameObject QuestListWindow;


    //QuestInfo에서 옮김
    //바인딩
    public GameObject ParentOBJ;
    public GameObject NewsBArea; // NewsBalloon이 자식으로 생성되는 UiCanvas안의 부모 오브젝트
    public GameObject WindowArea; // AdDataWindow가 자식으로 생성되는 UiCanvas안의 부모 오브젝트
    public GameObject IconArea;

    float time = 2.0f; // 보여지는 시간
    float E_time = 2.0f; // 증축
    bool timecheck = false;
    float orgtime = 2.0f; // 보여지고 나서 초기화 되는 값
    //time과orgtime은 값이 같아야함

    //구조체
    public struct USERDATA
    {
        [SerializeField] int gold;
        [SerializeField] int fame;

        public int Gold
        {
            get => GameManager.Instance.Gold;
            set
            {
                GameManager.Instance.Gold = value;
            }
        }
        public int Fame
        {
            get => GameManager.Instance.Fame;
            set
            {
                GameManager.Instance.Fame = value;
            }
        }
    }


    private void Awake()
    {
        Inst = this;
    }

    void Update()
    {
        Gold.text = GameManager.Instance.Gold.ToString();
        Fame.text = GameManager.Instance.Fame.ToString();


        if (timecheck)
        {
            time -= Time.unscaledDeltaTime;
            if(time < 0)
            {
                GoldIncrease.SetActive(false);
                FameIncrease.SetActive(false);
                time = orgtime;
            }
        }

        if (ExtendFail) // 골드가 모자라 증축에 실패했을 때
        {
            NoticeWindow.SetActive(true);

            E_time -= Time.deltaTime;
            if (E_time < 0f) // 변한 값을 몇초 동안 보여주고 사라짐
            {
                NoticeWindow.SetActive(false);
                ExtendFail = false;
                E_time = orgtime;
            }
        }
    }

    public void UiChangeGold(int Price) // 증감된 골드 표시
    {
        timecheck = true; // 시간재기

        if (Price > 0) // 양수 -> 빨간색으로 표시
        {
            GoldIncrease.GetComponent<TMP_Text>().text = $"<color=red>+{Price}";
        }
        else // 음수 -> 파란색으로 표시
        {
            GoldIncrease.GetComponent<TMP_Text>().text = $"<color=blue>{Price}";
        }
        GoldIncrease.SetActive(true);
    }

    public void UiChangeFame(int Price) // 증감된 명예 표시
    {
        timecheck = true; // 시간재기

        if (Price > 0) // 양수 -> 빨간색으로 표시
        {
            FameIncrease.GetComponent<TMP_Text>().text = $"<color=red>+{Price}";
        }
        else // 음수 -> 파란색으로 표시
        {
            FameIncrease.GetComponent<TMP_Text>().text = $"<color=blue>{Price}";
        }
        FameIncrease.SetActive(true);
    }

    public void OnQLWindow()
    {
        QuestListWindow.SetActive(true);
    }
}