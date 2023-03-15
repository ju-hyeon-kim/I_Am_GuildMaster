using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[Serializable]
public class GameManager : Singleton<GameManager>
{
    public int Gold;
    public int Fame;

    public void GoTitle() //타이틀 화면으로 가기
    {
        SceneManager.LoadScene(0);
    }

    public void GoMain() //메인화면으로 가기
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeGold(int Price)
    {
        Gold += Price;
        UIManager.Inst.UiChangeGold(Price);
    }

    public void ChangeFame(int Price)
    {
        Fame += Price;
        UIManager.Inst.UiChangeFame(Price);
    }
}
