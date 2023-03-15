using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainBoard : MonoBehaviour
{
    bool Active = false;
    public GameObject QuestListWindow;
    public void OnAction()
    {
        Active = !Active;

        if(Active)
        {
            QuestListWindow.SetActive(true);
        }
        else 
        {
            QuestListWindow.SetActive(false);
        }
    }
    public void ExitBtn()
    {
        Active = false;
        QuestListWindow.SetActive(false);
    }
  }
   
