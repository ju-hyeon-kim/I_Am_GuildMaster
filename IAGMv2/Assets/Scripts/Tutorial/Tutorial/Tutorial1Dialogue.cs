using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue1
{
    [TextArea]
    public string dialogue;
    public Sprite cg;
    public string name;
}
public class Tutorial1Dialogue : MonoBehaviour
{
    [SerializeField] public TMP_Text Content;
    [SerializeField] public TMP_Text Name;
    [SerializeField] public Image Profile;
    [SerializeField] public GameObject NextBtn;

    public int ContentNum = 0;
    public bool Talking = false;

    [SerializeField] public Dialogue1[] Contents;

    public void PlayDialogue()
    {
        StartCoroutine(Typing());
        Profile.sprite = Contents[ContentNum].cg;
        Name.text = Contents[ContentNum].name;
    }

    IEnumerator Typing() //글자 타이핑 효과
    {
        NextBtn.SetActive(false);
        Talking = true;
        for (int i = 0; i < Contents[ContentNum].dialogue.Length; ++i)
        {
            SoundManager.Instance.TypingSound.Play();
            Content.text = Contents[ContentNum].dialogue.Substring(0, i + 1);
            yield return new WaitForSecondsRealtime(0.15f);
        }
        NextBtn.SetActive(true);
        Talking = false;
    }
    private void HideDialogue()
    {
        gameObject.SetActive(false);
    }

    private void ImmediatelyShow()
    {
        Content.text = Contents[ContentNum].dialogue;
        Profile.sprite = Contents[ContentNum].cg;
        Name.text = Contents[ContentNum].name;
        NextBtn.SetActive(true);
        Talking = false;
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Talking == false)
            {
                if (ContentNum < Contents.Length)
                {
                    StartCoroutine(Typing());
                }
                else
                {
                    HideDialogue();
                }
            }
            else if (Talking == true)
            {
                if (ContentNum < Contents.Length)
                {
                    StopAllCoroutines();
                    Content.text = null;
                    ImmediatelyShow();
                }
                else
                {
                    HideDialogue();
                }
            }
        }
    }
}
