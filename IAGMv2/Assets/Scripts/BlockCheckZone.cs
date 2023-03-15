using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheckZone : MonoBehaviour
{
    public GameObject mySpawnManager; // 스폰매니저 바인딩

    private void OnTriggerStay(Collider obj) // 해당 구역에 Npc가 서있으면 새로운 Npc 생성 중단
    {
        mySpawnManager.GetComponent<SpawnManager>().BlockChk = true;
    }

    private void OnTriggerExit(Collider obj)
    {
        mySpawnManager.GetComponent<SpawnManager>().BlockChk = false;
    }
}
