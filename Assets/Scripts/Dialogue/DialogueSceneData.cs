using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DialogueSystem/DialogueScene")]
public class DialogueSceneData : ScriptableObject
{
    [SerializeField] private DialogueManager.Actors sceneActor;
    [SerializeField] private List<DialogueData> sceneDialogues = new List<DialogueData>();

    public DialogueManager.Actors GetSceneActor()
    {
        return sceneActor;
    }

    public DialogueData GetSceneDialogue(int index)
    {
        if (index < sceneDialogues.Count)
        {
            DialogueData currentDialogueData = sceneDialogues[index];

            return currentDialogueData;
        }
        else
        {
            return null;
        }
    }

    public DialogueData GetConclusionDialogue(int succesPoints, int possibleSuccesPoints)
    {
        DialogueData lastDialogue = CreateInstance("DialogueData") as DialogueData;
        if (succesPoints > possibleSuccesPoints/2)
        {
            for (int i=sceneDialogues.Count-2; i < sceneDialogues.Count; i++)
            {
                if (sceneDialogues[i].GetBranchId() == "A")
                {
                    lastDialogue = sceneDialogues[i];
                }
            }
        }
        else
        {
            for (int i = sceneDialogues.Count - 2; i < sceneDialogues.Count; i++)
            {
                if (sceneDialogues[i].GetBranchId() == "B")
                {
                    lastDialogue = sceneDialogues[i];
                }
            }
        }
        return lastDialogue;
    }

    public bool IsEmpty()
    {
        if (sceneDialogues.Count > 0)
        {
            return false;
        }

        else 
        {
            return true;
        }
    }

    public List<DialogueData> GetAllceneDialogues()
    {
        return sceneDialogues;
    }

    public int DialogueCount()
    {
        return sceneDialogues.Count;
    }

}
