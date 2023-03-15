using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeManager : Singleton<SceneChangeManager>
{

    public AsyncOperation ao;
    public bool LoadingChk = false; // 현재 로딩 중인지를 검사하는 불값

    new private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void ChangeScene(string s)
    {
        if (!LoadingChk)
        {
            StartCoroutine(Loading(s));
        }
    }

    IEnumerator Loading(string s)
    {
        LoadingChk = true;
        yield return SceneManager.LoadSceneAsync("Loading"); // 로딩 씬을 반환 한다 = 로딩 씬을 보여준다.
        yield return StartCoroutine(LoadNextScene(s));
        LoadingChk = false;
    }

    IEnumerator LoadNextScene(string s)
    {       
        ao = SceneManager.LoadSceneAsync(s);
        ao.allowSceneActivation = false; //씬로딩이 끝나기 전까지 씬을 활성화 시키지 않는다.

        //씬로딩이 완료되면 씬전환
        while (!ao.isDone)
        {
            if (ao.progress >= 0.9f)
            {
                if(Input.anyKeyDown)
                {
                    ao.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
