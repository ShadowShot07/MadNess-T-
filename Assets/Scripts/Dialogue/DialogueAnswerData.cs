using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueSystem/AnswerData")]
public class DialogueAnswerData : ScriptableObject
{
    [SerializeField] [TextArea] private string answerText;
    [SerializeField] private int answerValue;

    public string GetAnswerText()
    {
        return answerText;
    }
    public int GetAnswerValue()
    {
        return answerValue;
    }
}
