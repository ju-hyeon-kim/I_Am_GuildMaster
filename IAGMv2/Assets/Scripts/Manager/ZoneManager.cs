using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public GameObject[] Points;

    private void Start()
    {
        for(int i = 0; i < Points.Length; i++)
        {
            Points[i].GetComponent<SpawnChk>().QuestNum = i + 1;
        }
    }
}
