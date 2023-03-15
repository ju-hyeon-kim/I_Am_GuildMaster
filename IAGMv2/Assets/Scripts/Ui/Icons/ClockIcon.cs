using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
//using static UnityEditor.PlayerSettings;

public class ClockIcon : MonoBehaviour
{
    delegate void Delegate(); // 기본 함수 대행 
    delegate void DelegateS(string IconName,float WaitSeconds); // 문자열 함수 대행

    Delegate deStaffIcon;
    DelegateS deHostIcon;

    public GameObject myNotouch;

    public GameObject myHost;
    public Transform myIconZone;
    public Button myButton;

    public Image myTimeArea;
    public float LimitTime = 30.0f; // 제한시간 30초

    // Start is called before the first frame update
    void Start()
    {
        deHostIcon = myHost.GetComponent<Host>().CorourineIcon;

        StartCoroutine(TimeChecking());
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(myIconZone.position);
        transform.position = pos;
    }
    IEnumerator TimeChecking()
    {
        myTimeArea.fillAmount = 0.0f;
        float speed = 1.0f / LimitTime;
        while (myTimeArea.fillAmount < 1.0f)
        {
            myTimeArea.fillAmount += speed * Time.deltaTime;
            if (myTimeArea.fillAmount == 1.0f)
            {
                myHost.GetComponent<Host>().onAngry = true;
                myHost.GetComponent<Host>().Questing = false;
                Destroy(this.gameObject);
            }
            yield return null;
        }
    }
    public void OnRemveClock()
    {
        Destroy(gameObject);
    }

    public void OnIcon() 
    {
        SoundManager.Instance.ClickSound.Play();
        switch(myHost.GetComponent<Host>().purpose)
        {
            case 0: // 방문 목적이 로비일때
                deHostIcon("QuestIcon",1.0f);
                break;
            case 1: // 방문 목적이 식당일때
                deHostIcon("MeatIcon",1.0f);
                break;
            case 2: // 방문 목적이 여관일때
                deHostIcon("BedIcon",1.0f);
                break;
        }
    }
    public void OnStaffSmile()
    {
        deStaffIcon = myHost.GetComponent<Host>().myStaff.GetComponent<Staff>().OnSmileIcon;
        deStaffIcon();
    }

    public void NoTouchSound()
    {
        SoundManager.Instance.NoTouchSound.Play();
    }
}