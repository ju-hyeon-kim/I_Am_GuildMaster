using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeasonSystem : MonoBehaviour
{
    public float OneSeason = 6000.0f ; // 1계절=10일=100분=6000초
    float time = 0;

    public GameObject Spring;
    public GameObject Summer;
    public GameObject Fall;
    public GameObject Winter;

    void Update()
    {
        time += Time.deltaTime;

        if(time >=0 && time < OneSeason)
        {
            Spring.SetActive(true);
            Summer.SetActive(false);
            Fall.SetActive(false);
            Winter.SetActive(false);
        }
        else if(time >= OneSeason && time < OneSeason * 2)
        {
            Spring.SetActive(false);
            Summer.SetActive(true);
        }
        else if (time >= OneSeason * 2 && time < OneSeason * 3)
        {
            Summer.SetActive(false);
            Fall.SetActive(true);
        }
        else if (time >= OneSeason * 3 && time < OneSeason * 4)
        {
            Fall.SetActive(false);
            Winter.SetActive(true);
        }
        else
        {
            time = 0;
        }
    }


}
