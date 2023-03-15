using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue2
{
    [TextArea]
    public string dialogue;
}
public class Tutorial2Dialogue : MonoBehaviour
{
    [SerializeField] public TMP_Text Talk;
    [SerializeField] public Dialogue2[] dialogue;
}