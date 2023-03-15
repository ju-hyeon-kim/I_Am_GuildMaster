using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using TMPro;

public class Extend : MonoBehaviour
{
    ChairBedChk Furniture; //����
    public TMP_Text ChairCountTxt;
    public TMP_Text BedCountTxt;
    int ChairCount;
    int RoomCount;
    public GameObject ExtendIcon;
    public GameObject ExtendWindow;
    public bool ExtendActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        Furniture = GameObject.FindObjectOfType<ChairBedChk>();
        ExtendWindow.SetActive(false);
        StartCoroutine(ChairBedCount());
    }

    // Update is called once per frame
/*    void Update()
    {

    }
*/
    //���� ������ ������ ���� â �߰�
    public void TryOpenExtendWindow()
    {
        ExtendActivated = !ExtendActivated;

        if (ExtendActivated) OpenExtendWindow();
        else CloseExtendWindow();
    }

    public void OpenExtendWindow()
    {
        ExtendWindow.SetActive(true);
    }

    public void CloseExtendWindow()
    {
        ExtendActivated = false;
        ExtendWindow.SetActive(false);
    }
    IEnumerator ChairBedCount()
    {
        ChairCount = Furniture._chairSlot.Count; //���� ���� ��������
        RoomCount = Furniture._bedSlot.Count; //ħ�� ���� ��������
        ChairCountTxt.text = "���� ���̺� ���� : " + (ChairCount * 0.5).ToString() + "��";
        BedCountTxt.text = "���� �� ���� : " + RoomCount.ToString() + "��";
        yield return null;
    }
}
