using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueSystem/DialogueData")]
public class DialogueData : ScriptableObject
{
    [SerializeField] private DialogueManager.Actors actorName;
    [SerializeField] [TextArea(6 , 6)] private string dialogue;
    [SerializeField] private List<DialogueAnswerData> answers = new List<DialogueAnswerData>();
    [SerializeField] private string branchId;

    public DialogueManager.Actors GetActorName()
    {
        return actorName;
    }

    public string GetDialogue()
    {
        return dialogue;
    }

    public List<DialogueAnswerData> GetAnswers()
    {
        return answers;
    }

    public string GetBranchId()
    {
        return branchId;
    }
}
