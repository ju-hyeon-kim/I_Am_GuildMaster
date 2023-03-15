using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    public Image myFadeOut;

    void Update()
    {
        if (myFadeOut.fillAmount < 1.0f)
        {
            myFadeOut.fillAmount += 0.5f * Time.deltaTime;
        }
        else
        {
            Scene scene = SceneManager.GetActiveScene();

            if(scene.name == "Tutorial1")
            {
                SceneChangeManager.Instance.ChangeScene("Tutorial2");
            }
            else
            {
                SceneChangeManager.Instance.ChangeScene("Main");
            }
        }

    }
}
