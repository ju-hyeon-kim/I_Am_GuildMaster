using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogues : MonoBehaviour
{
    public GameObject[] TutoDialogues;
    public GameObject FadeOut;
    public Animator EyeMask;
    public int TalkerNum = 0;

    private void Start()
    {
        TutoDialogues[0].GetComponent<Tutorial1Dialogue>().PlayDialogue();
    }

    private void Update()
    {
        if (TutoDialogues[TalkerNum].GetComponent<Tutorial1Dialogue>().Talking == false)
        {
            if (Input.anyKeyDown)
            {
                SoundManager.Instance.ClickSound.Play();
                if (TalkerNum == 1 && TutoDialogues[TalkerNum].GetComponent<Tutorial1Dialogue>().ContentNum == 4)
                {
                    FadeOut.gameObject.SetActive(true);
                    TutoDialogues[TalkerNum].GetComponent<Tutorial1Dialogue>().Talking = true;
                }
                
                else 
                {
                    //다음대화로 넘어감)
                    ++(TutoDialogues[TalkerNum].GetComponent<Tutorial1Dialogue>().ContentNum);
                    TutoDialogues[TalkerNum].SetActive(false);
                    if (TalkerNum == 0)
                    {
                        if (TutoDialogues[TalkerNum].GetComponent<Tutorial1Dialogue>().ContentNum == 1)
                        {
                            EyeMask.SetTrigger("IsEyeOpen2");
                        }
                        TutoDialogues[TalkerNum + 1].SetActive(true);
                        TutoDialogues[TalkerNum + 1].GetComponent<Tutorial1Dialogue>().PlayDialogue();
                        TalkerNum = 1;
                    }
                    else
                    {

                        TutoDialogues[TalkerNum - 1].SetActive(true);
                        TutoDialogues[TalkerNum - 1].GetComponent<Tutorial1Dialogue>().PlayDialogue();
                        TalkerNum = 0;
                    }
                }
            }
        }
    }
}
