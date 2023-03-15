using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : Singleton<DataManager>
{
    public int SlotNum;
    public string pathSave;
    public string pathLoad;

    new private void Awake()
    {
        pathSave = Path.Combine(Application.dataPath, "Save", "Save");
        pathLoad = Path.Combine(Application.dataPath, "Load", "Load.bin");
    }

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "Main")
        {
            if (File.Exists(pathLoad))// 로드 
            {
                SaveData save = Load_pathLoad();
                Data_to_Game(save); // [LODE] 데이터를 게임에 적용
            }
        }
    }

    public void Save(SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Create(pathSave + $"{SlotNum}.bin");
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public SaveData Load()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.OpenRead(pathSave + $"{SlotNum}.bin");
            SaveData data = (SaveData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        catch // 저장된 파일이 없을 때
        {
            return default;
        }
    }

    public void Save_pathLoad()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Create(pathLoad);
        formatter.Serialize(stream, Load());
        stream.Close();
    }

    public SaveData Load_pathLoad()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.OpenRead(pathLoad);
            SaveData data = (SaveData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        catch // 저장된 파일이 없을 때
        {
            return default;
        }
    }

    public void Data_to_Game(SaveData save) // 데이터를 게임에 적용
    {
        GameManager.Instance.Gold = save.Gold;
        GameManager.Instance.Fame = save.Fame;

        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Main")
        {
            TimeManager.Instance.DayCount = save.Day;
            TimeManager.Instance.MonthCount = save.Month;
            TimeManager.Instance.SeasonCount = save.Season;
        }

        ExtendLoad(save);
        RQListLoad(save);
    }

    public void RQListLoad(SaveData save)
    {
        Quest.QuestInfo[] RQarray = save.RQList.ToArray();
        for (int i = 0; i < RQarray.Length; i++)
        {
            QuestManager.Instance.PostedQuest(RQarray[i]);
        }
    }

    public void ExtendLoad(SaveData save)
    {
        //모텔
        for (int i = 0; i < save.RoomsCount; ++i)
        {
            RoomExtend.Instance.M_ExtendLoad();
        }

        //여관
        for (int i = 0; i < save.TableSetCount; ++i)
        {
            RoomExtend.Instance.P_ExtendLoad();
        }
    }
}
