using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class SettingManager : MonoBehaviour
{
    public GameObject SettingWindow; //ESC 누르면 나오는 창
    public GameObject ReCheckWindow; // 그만하기 누르면 나오는 창
    public GameObject DetailSettingWindow; // ESC Window 안의 설정 아이콘 누르면 나오는 설정 창
    public GameObject SaveLodeWindow;
    public GameObject NoTouch;
    bool[] IsOpen = new bool[3];
 
    TimeBtns Time;

    SettingWindow DSW;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<IsOpen.Length; ++i)
        {
            IsOpen[i] = false;
        }

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "Tutorial2")
        {
            DSW = DetailSettingWindow.GetComponent<SettingWindow>();
            SettingWindow.SetActive(false);
            DetailSettingWindow.SetActive(false);
            ReCheckWindow.SetActive(false);
            DSW.BGMvolume.value = PlayerPrefs.GetFloat("volume");
            //DetailSettingWindow.GetComponent<SettingWindow>().Effvolume.value = PlayerPrefs.GetFloat("eff");
            DSW.Effvolume.value = PlayerPrefs.GetFloat("eff");
        }
    }

    // Update is called once per frame
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsOpen[0] = !IsOpen[0];
            if (IsOpen[0])
            {
                SettingWindow.SetActive(true);
            }
            else
            {
                SettingWindow.SetActive(false);
            }
        }

    }
   
    public void OpenS_Window() // 설정 창 뜨게하기
    {
        IsOpen[0] = !IsOpen[0];
        if (IsOpen[0])
        {
            SettingWindow.SetActive(true);
        }
        else
        {
            SettingWindow.SetActive(false);
        }
    }

    public void OpenDS_Window()  // 세부설정 창 뜨게하기
    {
        IsOpen[1] = !IsOpen[1];
        if (IsOpen[1])
        {
            DetailSettingWindow.SetActive(true);
            
        }
        else
        {
           
            DetailSettingWindow.SetActive(false);
            Soundset();
        }
    }

    public void OpenReCheckWindow() // 그만하기 버튼 누르면 다시 물어보는 창 뜨게
    {
        IsOpen[2] = !IsOpen[2];
        if (IsOpen[2])
        {
            ReCheckWindow.SetActive(true);
            NoTouch.SetActive(true);
            Time = FindObjectOfType<TimeBtns>();
            Time.OnPause(); //창 뜨면 일시정지

        }
        else
        {
            ReCheckWindow.SetActive(false);
        }
    }
    public void OpenSL_Window()  // 세부설정 창 뜨게하기
    {
        SaveLodeWindow.SetActive(true);
    }

    public void S_WindowExit() //세팅 창 나가기
    {
        IsOpen[0] = false;
        SettingWindow.SetActive(false);
    }

    public void SL_WindowExit()
    {
        SaveLodeWindow.SetActive(false);
    }

    public void ClickNo()
    {
        IsOpen[2] = false;
        ReCheckWindow.SetActive(false);
        Time.OnPlay();
        NoTouch.SetActive(false);
    }

    public void ClickYes()
    {
        Application.Quit();
    }

    void Soundset()
    {
        DSW.BGM.volume = PlayerPrefs.GetFloat("volume");
        DSW.BGMvolume.value = PlayerPrefs.GetFloat("volume");
        foreach (AudioSource source in DSW.Eff)
        {
            source.volume = PlayerPrefs.GetFloat("eff");
        }
        DSW.Effvolume.value = PlayerPrefs.GetFloat("eff");
        DetailSettingWindow.transform.localPosition = DSW.orgPos;
    }
}
