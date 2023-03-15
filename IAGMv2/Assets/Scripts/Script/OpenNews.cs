using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenNews : MonoBehaviour
{

    public static bool NewsActived = false;
    public GameObject NewsWindow;
    

    // Start is called before the first frame update
    void Start()
    {
        NewsWindow.SetActive(false);
    }

    
    public void TryOpenNews()
    {
        NewsActived = !NewsActived;

        if (NewsActived) OpenNewsWindow();
        else CloseNewsWindow();
    }

    public void OpenNewsWindow()
    {
        NewsWindow.SetActive(true);
    }

    public void CloseNewsWindow()
    {
        NewsWindow.SetActive(false);
    }
}

