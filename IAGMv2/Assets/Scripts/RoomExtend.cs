using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomExtend : Singleton<RoomExtend>
{
    ChairBedChk _furniture = null;
    ChairBedChk Furniture
    {
        get
        {
            if (_furniture == null)
            {
                _furniture = GameObject.FindObjectOfType<ChairBedChk>();
            }
            return _furniture;
        }
    }
    public GameObject RoomExtendBtn; // 방 증축 버튼
    public GameObject PubChairBtn; // 식당 의자 증축 버튼
    public GameObject[] Rooms;
    public GameObject[] PubChairs;
    public int RoomsCount = 0;
    public int TableSetCount = 0;
    int Price = -1000; // 가격

    public void RExtend() //방 증축
    {
        if (GameManager.Instance.Gold >= -Price)
        {
            if (Furniture._bedSlot.Count < 5)
            {
                Furniture._bedSlot.Add(ChairBedChk.BedSlot.None);
                Rooms[RoomsCount].SetActive(true);
                RoomsCount++;
                GameManager.Instance.ChangeGold(Price);
            }
        }
        else
        {
            UIManager.Inst.ExtendFail = true;
        }
    }

    public void PubExtend() //식당 증축
    {
        if (GameManager.Instance.Gold >= -Price)
        {
            if (Furniture._chairSlot.Count < 11)
            {
                for (int i = 0; i < 2; i++)
                {
                    Furniture._chairSlot.Add(ChairBedChk.ChairSlot.None);
                }
                PubChairs[TableSetCount].SetActive(true);
                TableSetCount++;
                GameManager.Instance.ChangeGold(Price);
            }
        }
        else
        {
            UIManager.Inst.ExtendFail = true;
        }
    }

    public void M_ExtendLoad() // 여관게임 로드 할때 실행되는 함수
    {
        Furniture._bedSlot.Add(ChairBedChk.BedSlot.None);
        Rooms[RoomsCount].SetActive(true);
        RoomsCount++;
    }

    public void P_ExtendLoad() // 식당게임 로드 할때 실행되는 함수
    {
        for (int i = 0; i < 2; i++)
        {
            Furniture._chairSlot.Add(ChairBedChk.ChairSlot.None);
        }
        PubChairs[TableSetCount].SetActive(true);
        TableSetCount++;
    }
}