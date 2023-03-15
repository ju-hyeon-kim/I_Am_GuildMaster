using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLodeWindow : MonoBehaviour
{
    public GameObject NoTouch;
    public TMP_Text[] Slots;
    public TMP_Text SelectedSlot;
    public Color ChangeColor;
    Color OrgColor = Color.white;

    string DayText;
    string MonthText;
    string SeasonText;


    private void Start()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (File.Exists(Path.Combine(Application.dataPath, "Save", $"Save{i}.bin"))) // 슬롯들에 저장된 이름들 불러오기
            {
                DataManager.Instance.SlotNum = i;
                SaveData save = new SaveData();
                save = DataManager.Instance.Load();
                SeasonTextSet(save);
                Slots[i].text = $"{SeasonText}-{save.Month}월-{save.Day}일"; ;
            }
        }
       
    }
    public void SelectSlot(int slotnum)
    {
      
        DataManager.Instance.SlotNum = slotnum;
        SelectedSlot = Slots[slotnum];
        for (int i = 0; i < 3; i++)
        {
            if (SelectedSlot == Slots[i])
            {
                Slots[i].gameObject.GetComponentInParent<Image>().color = ChangeColor;
            }
            else
            {
                Slots[i].gameObject.GetComponentInParent<Image>().color = OrgColor;
            }
        }
    }

    public void SaveButton()
    {
        SaveData save = new SaveData();
        Game_to_Data(save); // 게임의 정보를 데이터로 저장
        SeasonTextSet(save); // 계절에 맞는 단어 선택
        Slots[DataManager.Instance.SlotNum].text = $"{SeasonText}-{save.Month}월-{save.Day}일";

        DataManager.Instance.Save(save);
    }

    public void LoadButton()
    {
        DataManager.Instance.Save_pathLoad();
        SceneChangeManager.Instance.ChangeScene("Main");
    }

    public void Game_to_Data(SaveData save) // 게임의 정보를 데이터로 저장
    {
        save.Gold = GameManager.Instance.Gold;
        save.Fame = GameManager.Instance.Fame;
        save.Day = TimeManager.Instance.DayCount;
        save.Month = TimeManager.Instance.MonthCount;
        save.Season = TimeManager.Instance.SeasonCount;
        save.RQList = QuestManager.Instance.RQlist;
        save.RoomsCount = RoomExtend.Instance.RoomsCount;
        save.TableSetCount = RoomExtend.Instance.TableSetCount;
    }

    public void Exit()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name != "Title")
        {
            NoTouch.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void SeasonTextSet(SaveData save)
    {
        switch (save.Season)
        {
            case 1:
                SeasonText = "봄";
                break;
            case 2:
                SeasonText = "여름";
                break;
            case 3:
                SeasonText = "가을";
                break;
            case 4:
                SeasonText = "겨울";
                break;
        }
    }
}