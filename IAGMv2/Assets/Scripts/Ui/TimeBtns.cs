using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBtns : Singleton<TimeBtns>
{
    public Color OrgColor;
    public Color ChangeColor;

    public Image FastButton;
    public Image PlayButton;
    public Image PauseButton;

    public bool returncheck = true;

    public bool pausecheck = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PlayButton.color = ChangeColor;
    }
    public void OnFastPlay()
    {
        Time.timeScale = 2;
        FastButton.color = ChangeColor;
        PlayButton.color = OrgColor;
        PauseButton.color = OrgColor;
        returncheck = false;
    }
    public void OnPlay()
    {
        Time.timeScale = 1;
        FastButton.color = OrgColor;
        PlayButton.color = ChangeColor;
        PauseButton.color = OrgColor;
        returncheck = true;
    }
    public void OnPause()
    {
        if (pausecheck == false) // 처음 눌렀을 때
        {
            Time.timeScale = 0;
            PauseButton.color = ChangeColor;
            pausecheck = true;
        }
        else // 일시 정지 상태에서 눌렀을 때 -> 전의 상태로 돌아감
        {
            PauseButton.color = OrgColor;
            pausecheck = false;
            if (returncheck == true)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 2;
            }
        }
    }
}
