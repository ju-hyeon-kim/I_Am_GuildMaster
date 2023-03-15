using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class TitleManager : MonoBehaviour
{
    public GameObject DetailSettingWindow;
    public GameObject SaveLoadWindow;
    public GameObject Tuto_YorN;
    public bool IsOpen = false;
    public Color ActiveColor;

    //로드게임 버튼 관련 변수
    public Image LodeBtn;
    public TMP_Text LodeBtnText;
    public GameObject LodeNotouch;
    bool[] isSave = new bool[3];

    //컴포넌트
    SettingWindow DSW;

    private void Start()
    {
        SoundManager.Instance.BGMSound.Play();
        DSW = DetailSettingWindow.GetComponent<SettingWindow>();
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(Path.Combine(Application.dataPath, "Save", $"Save{i}.bin")))
            {
                isSave[i] = true;
            }
            else
            {
                isSave[i] = false;
            }
        }

        if (isSave[0] == true || isSave[1] == true || isSave[2] == true)
        {
            LodeBtn.color = ActiveColor;
            LodeBtnText.color = ActiveColor;
            LodeNotouch.SetActive(false);
        }

        SoundSet();
    }

    public void NewGame()
    {
        //SceneChangeManager.Inst.ChangeScene("Tutorial1");
        Tuto_YorN.SetActive(true);
    }

    public void LoadGame()
    {
        SaveLoadWindow.SetActive(true);
    }

    public void OpenOption()  // 세부설정 창 뜨게하기
    {
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            DetailSettingWindow.SetActive(true);
        }
        else
        {
            SoundSet();
            DetailSettingWindow.SetActive(false);
        }
    }

    public void GameExit()
    {
        Application.Quit();
    }

    void SoundSet()
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

    public void Tuto_Yes()
    {
        //로드 파일 삭제?
        SceneChangeManager.Instance.ChangeScene("Tutorial1");
    }

    public void Tuto_NO()
    {
        //로드 파일 삭제?
        SceneChangeManager.Instance.ChangeScene("Main");
    }

    public void Tuto_Exit()
    {
        Tuto_YorN.SetActive(false);
    }
}
