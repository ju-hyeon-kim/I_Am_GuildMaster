using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodIcon : MonoBehaviour // 스마일 + 앵그리 아이콘
{
    public Transform myIconZone;
    float MoodTime = 1.5f; // 보여지는 시간
    float time = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(myIconZone.position);
        transform.position = pos;

        time += Time.deltaTime;
        if (time > MoodTime)
        {
            Destroy(gameObject);
        }
    }
}
