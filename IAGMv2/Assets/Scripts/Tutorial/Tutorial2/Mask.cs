using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mask : MonoBehaviour
{
    public RectTransform Mask_P;
    public Image Mask_C;
    float x;
    float y;

    private void Start()
    {
        // '마스크_부모'의 가로길이와 세로길이
        x = Mask_P.sizeDelta.x;
        y = Mask_P.sizeDelta.y;
    }

    void Update() // 마우스가 '마스크_부모'의 위치에 있으면 '마스크_자식'의 노터치가 해제됨
    {
        Vector3 mask = Camera.main.ScreenToViewportPoint(new Vector3(Mask_P.position.x, Mask_P.position.y, Mask_P.position.z));
        Vector3 mouse = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        if (mouse.x >= (mask.x - x * 0.0002) && mouse.x <= (mask.x + x * 0.0002))
        {
            if (mouse.y >= (mask.y - y * 0.0004) && mouse.y <= (mask.y + y * 0.0004))
            {
                Mask_C.raycastTarget = false;
            }
            else
            {
                Mask_C.raycastTarget = true;
            }
        }
        else
        {
            Mask_C.raycastTarget = true;
        }
    }
}
