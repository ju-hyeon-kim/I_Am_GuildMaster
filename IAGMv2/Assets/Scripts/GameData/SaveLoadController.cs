
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class SaveLoadController : MonoBehaviour
{
    SaveLoadController inst = null;
    public int NowSave = 0;
    public string SaveName = "Save_";
    public TMP_Text[] SaveBtn;
    public Color changecolor;
    public TMP_Text[] ChooseFile;
    public int num;

    bool[] SaveFile = new bool[3]; //세이브파일 존재유무 저장

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        for (int i = 0; i < SaveBtn.Length; ++i)
        {
            SaveBtn[i].text = "비어있음";
        }
    }

    public void LoadClick()
    {
        CharState.NPC LoadData = SaveSystem.Load<CharState.NPC>(SaveName + NowSave.ToString());
        UIManager.USERDATA uIManager = SaveSystem.Load<UIManager.USERDATA>(SaveName + NowSave.ToString());

        string temp = JsonUtility.ToJson(uIManager);
        string statinfo = JsonUtility.ToJson(LoadData);
        Debug.Log("adnpc 결과: " + statinfo);
        Debug.Log("uimanager 정보: " + temp);
    }

    public void ChoiceFile(int number)
    {
        SaveBtn[number].GetComponentInParent<UnityEngine.UI.Image>().color = changecolor;
        for(int i = 0; i < SaveBtn.Length; i++)
        {
            if(i != number)
            {
                SaveBtn[i].GetComponentInParent<UnityEngine.UI.Image>().color = Color.white;
            }
        }
        ChooseFile[number] = SaveBtn[number];
        num = number;
    }

    public void OnSaveFile()
    {
        inst.NowSave = num;
        //저장하기 버튼 한번 누를때마다 save_숫자 늘어나게

        CharState.NPC adnpc = new CharState.NPC();
        UIManager.USERDATA uIManager = new UIManager.USERDATA();

        SaveSystem.Save<CharState.NPC>(adnpc, SaveName + NowSave.ToString());
        SaveSystem.Save<UIManager.USERDATA>(uIManager, SaveName + NowSave.ToString());

        Debug.Log(SaveName + NowSave.ToString());
        if (File.Exists(SaveSystem.SavePath + $"{SaveName} + 0")) //데이터가 있는 경우
        {
            LoadClick();
            SaveBtn[num].text = SaveName + NowSave.ToString();
        }
        else
        {
            SaveBtn[num].text = SaveName + NowSave.ToString();
        }

        ChooseFile[num].GetComponentInParent<UnityEngine.UI.Image>().color = Color.white;
        //Debug.Log("저장 성공");
    }

    public void OnLodeFile()
    {
        //SceneManager.LoadScene("Main");
    }
}
