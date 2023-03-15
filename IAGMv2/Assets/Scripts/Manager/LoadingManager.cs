using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public GameObject LoadingText;
    public GameObject AnyKeyText;
    public Image myTimeArea;

    // Update is called once per frame
    void Update()
    {
        if(SceneChangeManager.Instance.ao.progress >= 0.9f) // 씬 로딩이 완료 되었을 때
        {
            LoadingText.GetComponent<Animator>().SetTrigger("OnIdle");
            LoadingText.GetComponent<TMP_Text>().text = "로딩완료!";
            AnyKeyText.SetActive(true);
        }
    }

    /*IEnumerator ClockAnim()
    {
        myTimeArea.fillAmount = 0.0f;
        float speed = 1.0f;
        while (myTimeArea.fillAmount < 1.0f)
        {
            myTimeArea.fillAmount += speed * Time.deltaTime;
            if (myTimeArea.fillAmount == 1.0f)
            {
                myTimeArea.fillAmount = 0;
                yield return null;
            }
        }
    }*/
}
