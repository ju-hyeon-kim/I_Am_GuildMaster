using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int Gold;
    public int Fame;

    public int Day;
    public int Month;
    public int Season;

    public List<Quest.QuestInfo> RQList;

    public int RoomsCount;
    public int TableSetCount;
}
