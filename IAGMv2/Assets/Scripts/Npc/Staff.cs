using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Staff : MonoBehaviour
{
    public Transform myIconZone;
    public GameObject myIconArea;
    public void OnSmileIcon()
    {
        // 스마일 생성
        GameObject obj = Instantiate(Resources.Load("IconPrefabs/SmileIcon"), myIconArea.transform) as GameObject;
        obj.GetComponent<MoodIcon>().myIconZone = myIconZone; // 해당구역의 스태프가 스마일 아이콘을 생성한다.
    }
}
