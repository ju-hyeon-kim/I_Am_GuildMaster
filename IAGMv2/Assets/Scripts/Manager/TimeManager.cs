using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TimeManager : Singleton<TimeManager>
{
    public int timeSpeed = 1; // 2는 2배로 빠르게
    public float DeadLine = 555.0f;
    public int OneDay = 30;
    public int DayCount = 1;
    public int MonthCount = 1;
    public int SeasonCount = 1;

    public bool clockSound = false;
    // 날짜 설정 가능 단, 각 계절의 시작일만 설정 가능 ex) 봄(1) 3월 1일 / 여름(2) 6월 1일 / 가을(3) 9월 1일 / 겨울(4) 12월 1일

    // 시계 회전
    [SerializeField]
    public Transform myRotateArea;

    // ChangeDay
    public TMPro.TMP_Text myDayText;

    // ChangeMonth
    int OneMonth;
    public TMPro.TMP_Text myMonthText;

    // ChangeSeason
    int OneSeason;
    public GameObject Spring;
    public GameObject Summer;
    public GameObject Fall;
    public GameObject Winter;

    public GameObject DayReportWindow;
    public GameObject CloseNotice;
    public bool GuildClose = false;
    public float temp;
    public int CloseReadyTime = 10; // 마감준비시간

    new private void Awake()
    {

    }

    void Start()
    {
        temp = OneDay;

        // 시계 회전
        StartCoroutine(ArrowRotate(OneDay / timeSpeed));

        // ChangeDay
        myDayText.GetComponent<TMPro.TMP_Text>();
        StartCoroutine(ChangeDay(OneDay / timeSpeed));

        // ChangeMonth
        OneMonth = OneDay * 30;
        myMonthText.GetComponent<TMPro.TMP_Text>();
        StartCoroutine(ChangeMonth(OneMonth / timeSpeed));

        // ChangeSeason
        OneSeason = OneMonth * 3;
        StartCoroutine(ChangeSeason(OneSeason / timeSpeed));
    }

    void Update()
    {
        temp -= Time.deltaTime;
        if (temp <= CloseReadyTime)
        {
            OnCloseReady();
            if (temp <= -0.1f)
            {
                GuildClose = true;
            }
        }

        if (GuildClose)
        {
            OnDayReport();
        }

        if (DayCount > 30)
        {
            DayCount = 1;
        }
        myDayText.text = $"DAY-{DayCount}";

        switch (MonthCount)
        {
            case 1:
                myMonthText.text = "JAN";
                break;
            case 2:
                myMonthText.text = "FEB";
                break;
            case 3:
                myMonthText.text = "MAR";
                break;
            case 4:
                myMonthText.text = "APR";
                break;
            case 5:
                myMonthText.text = "MAY";
                break;
            case 6:
                myMonthText.text = "JUN";
                break;
            case 7:
                myMonthText.text = "JUL";
                break;
            case 8:
                myMonthText.text = "AUG";
                break;
            case 9:
                myMonthText.text = "SEP";
                break;
            case 10:
                myMonthText.text = "OCT";
                break;
            case 11:
                myMonthText.text = "NOV";
                break;
            case 12:
                myMonthText.text = "DEC";
                break;
            case 13:
                MonthCount = 1;
                break;
        }

        switch (SeasonCount)
        {
            case 1:
                Spring.SetActive(true);
                Summer.SetActive(false);
                Fall.SetActive(false);
                Winter.SetActive(false);
                break;
            case 2:
                Spring.SetActive(false);
                Summer.SetActive(true);
                Fall.SetActive(false);
                Winter.SetActive(false);
                break;
            case 3:
                Spring.SetActive(false);
                Summer.SetActive(false);
                Fall.SetActive(true);
                Winter.SetActive(false);
                break;
            case 4:
                Spring.SetActive(false);
                Summer.SetActive(false);
                Fall.SetActive(false);
                Winter.SetActive(true);
                break;
            case 5:
                SeasonCount = 1;
                break;
        }
    }

    IEnumerator ArrowRotate(int Day) // 시계바늘 회전
    {
        while (Day > 0.0f)
        {
            myRotateArea.Rotate(Vector3.back * (360.0f / Day) * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator ChangeDay(int Day)
    {
        yield return new WaitForSeconds(Day);

        DayCount++;

        StartCoroutine(ChangeDay(Day));
    }

    IEnumerator ChangeMonth(int Month)
    {
        yield return new WaitForSeconds(Month);

        MonthCount++;

        StartCoroutine(ChangeMonth(Month));
    }

    IEnumerator ChangeSeason(int Season)
    {
        yield return new WaitForSeconds(Season);

        SeasonCount++;

        StartCoroutine(ChangeSeason(Season));
    }

    public void OnDayReport()
    {
        Time.timeScale = 0;
        CloseNotice.SetActive(false);
        clockSound = false;
        
        DayReportWindow.SetActive(true);
    }

    public void OnCloseReady()
    {
        clockSound = true;
        CloseNotice.SetActive(true);
    }
}
