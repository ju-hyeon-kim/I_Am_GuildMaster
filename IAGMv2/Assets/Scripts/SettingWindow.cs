using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingWindow : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    Vector2 dragOffset = Vector2.zero;
    public AudioSource BGM;
    public AudioSource[] Eff;

    public Toggle BGMMute;
    public Toggle EffMute;

    public Slider BGMvolume;
    public Slider Effvolume;

    public Vector3 orgPos;
    // Start is called before the first frame update
    void Start()
    {
        orgPos = transform.localPosition;
    }

    public void bgmMute()
    {
        if (BGMMute.isOn)
        {
            BGM.mute = true;
        }
        else
        {
            BGM.mute = false;
        }
    }
    public void effMute()
    {
        if (EffMute.isOn)
        {
            foreach (AudioSource source in Eff)
            {
                source.mute = true;
            }
        }
        else
        {
            foreach (AudioSource source in Eff)
            {
                source.mute = false;
            }
        }
    }
    public void BGMTestVolume()
    {
        BGM.volume = BGMvolume.value;
    }
    public void EffVolume()
    {
        foreach (AudioSource source in Eff)
        {
            source.volume = Effvolume.value;
        }
    }
    public void SetVolume()
    {
        PlayerPrefs.SetFloat("volume", BGMvolume.value);
        BGM.volume = PlayerPrefs.GetFloat("volume");
        PlayerPrefs.SetFloat("eff", Effvolume.value);
        foreach (AudioSource source in Eff)
        {
            source.volume = PlayerPrefs.GetFloat("eff");
        }
        /*if (!BGMMute.isOn)
        {
            BGM.mute = false;
        }
        else
        {
            BGM.mute = true;
        }
        if (!EffMute.isOn)
        {
            foreach (AudioSource source in Eff)
            {
                source.mute = false;
            }
        }
        else
        {
            foreach (AudioSource source in Eff)
            {
                source.mute = true;
            }
        }*/

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = (Vector2)transform.position - eventData.position;

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + dragOffset;
    }
}
