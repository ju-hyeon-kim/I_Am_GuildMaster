using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayReportWindow : MonoBehaviour
{
    public GameObject SaveLoadWindow;
    public GameObject myNotoch;
    public TMP_Text myClosedDay;
    public TMP_Text NowGoldText;
    public TMP_Text NowFameText;
    string _S;
    public int NowGold;
    public int NowFame;

    private void Update()
    {
        int S = TimeManager.Instance.SeasonCount;
        int M = TimeManager.Instance.MonthCount;
        int D = TimeManager.Instance.DayCount;
        
        switch(S)
        {
            case 1:
                _S = "봄";
                break;
            case 2:
                _S = "여름";
                break;
            case 3:
                _S = "가을";
                break;
            case 4:
                _S = "겨울";
                break;
        }
        myClosedDay.text = _S + $" {M}월 {D - 1}일 영업종료";

        // 현재 골드/평판
        NowGold = GameManager.Instance.Gold;
        NowFame = GameManager.Instance.Fame;
        NowGoldText.text = "현재 골드 : " + NowGold.ToString();
        NowFameText.text = "현재 평판 : " + NowFame.ToString();
    }

    public void OnSaveLoadWindow()
    {
        myNotoch.SetActive(true);
        SaveLoadWindow.SetActive(true);
    }

    public void OnContinue()
    {
        TimeBtns.Instance.OnPlay();
        TimeManager.Instance.GuildClose = false;
        gameObject.SetActive(false);
        TimeManager.Instance.temp = TimeManager.Instance.OneDay;
        SoundManager.Instance.StartSound.Play();
    }
}
