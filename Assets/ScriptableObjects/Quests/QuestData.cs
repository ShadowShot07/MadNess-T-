using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Quest Data")]
public class QuestData : ScriptableObject
{
    //[SerializeField] private DialogueManager.instance.Actors actorQuestOwner;
    [SerializeField] private string actorQuestOwner;
    [SerializeField] [TextArea] private string questDescription;
    [SerializeField] private bool isTimed;
    [SerializeField] private float availableQuestSecTime;
    [SerializeField] private bool isObjectRequired;
    [SerializeField] private string objectRequiredName;
    [SerializeField] private int objectAmountRequired;

    public string GetQuestOwner()
    {
        return actorQuestOwner;
    }

    public string GetObjectRequiredName()
    {
        return objectRequiredName;
    }

    public bool IsTimed()
    {
        return isTimed;
    }

    public bool IsObjectRequired()
    {
        return isObjectRequired;
    }

    public float GetAvailableQuestSecTime()
    {
        return availableQuestSecTime;
    }

    public int GetObjectAmountRequired()
    {
        return objectAmountRequired;
    }
}
