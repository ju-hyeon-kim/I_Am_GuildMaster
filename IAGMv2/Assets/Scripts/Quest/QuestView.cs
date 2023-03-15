using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour
{
    public GameObject host;
    public Button myButton;
    public int num;

    public void HostView()
    {
        SoundManager.Instance.ClickSound.Play();
        if (host.GetComponent<Host>().myState == Host.STATE.Battle || host.GetComponent<Host>().myState == Host.STATE.Farming)
        {
            num = host.transform.parent.parent.GetComponent<SpawnChk>().QuestNum;
            switch (num)
            {
                case 1:
                    CamManager.Instance.QuestCam1();
                    break;
                case 2:
                    CamManager.Instance.QuestCam2();
                    break;
                case 3:
                    CamManager.Instance.QuestCam3();
                    break;
                case 4:
                    CamManager.Instance.QuestCam4();
                    break;
                case 5:
                    CamManager.Instance.QuestCam5();
                    break;
                case 6:
                    CamManager.Instance.QuestCam6();
                    break;
                case 7:
                    CamManager.Instance.QuestCam7();
                    break;
            }
        }
        else
        {
            CamManager.Instance.QuestCam0();
        }
    }

}
