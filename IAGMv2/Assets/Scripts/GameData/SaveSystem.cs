using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SaveSystem : MonoBehaviour
{
    
    public static string SavePath => Application.persistentDataPath + "/saves/"; //저장파일 경로
 
     public static void Save<T>(T adnpc,string saveFileName)
    {
                    
            if (!Directory.Exists(SavePath)) //SavePath에 directory가 존재하지 않는다면
            {
                Directory.CreateDirectory(SavePath); //directory 생성
            }
            string saveJson = JsonUtility.ToJson(adnpc,true); //charState를 Json으로 표현
            string saveFilePath = SavePath + saveFileName + ".json";

            File.WriteAllText(saveFilePath, saveJson); //saveFilePath 파일을 생성하고 Json으로 표현한 charState 내용을 작성
           
            Debug.Log("저장 성공: " + saveFilePath);
           
    }

    public static T Load<T>(string saveFileName)
    {
        string saveFilePath = SavePath + saveFileName + ".json";

        if (!File.Exists(saveFilePath)) //경로에 저장파일이 존재하지 않는다면
        {
            Debug.Log("저장된 파일이 없습니다.");
        }

        string saveFile = File.ReadAllText(saveFilePath);
        T adnpc = JsonUtility.FromJson<T>(saveFile); //saveFile에 CharState 내용을 불러온다

        return adnpc;
      
    }




}
