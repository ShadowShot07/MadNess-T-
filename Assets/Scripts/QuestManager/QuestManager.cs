using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{ 
    public List<GameObject> sceneActors;

    [SerializeField] private List<QuestData> sceneQuests;

    private QuestData currentQuest;

    private float time = 0f;

    private bool isQuestActive = false;

    private int currentQuestObjectAmount;

    private bool isCurrentQuestSucceed = false;


    private void Start()
    {
        AudioManager.instance.StartGameMusic();
    }

    private void Update()
    {
        if (isQuestActive && currentQuest.IsTimed())
        {
            time += Time.deltaTime;
        }
    }

    private void StartQuest(QuestData newQuest)
    {
        time = 0f;
        currentQuest = newQuest;
        isCurrentQuestSucceed = false;
        isQuestActive = true;

        // Add Quest to UI  
    }

    public void AddQuest(string actorName)
    {
        foreach (QuestData quest in sceneQuests)
        {
            if (actorName == quest.GetQuestOwner())
            {
                currentQuest = quest;
            }
        }

        StartQuest(currentQuest);
    }

    public void EndActorQuest()
    {
        isQuestActive = false;
        if (currentQuest.IsTimed())
        {
            if (IsTimedQuestSucced())
            {
                
            }
        }
    }

    private bool IsTimedQuestSucced()
    {
        isQuestActive = false;
        if (time <= currentQuest.GetAvailableQuestSecTime())
        {
            time = 0f;
            return true;
        }

        else
        {
            return false; 
        }
        
    }

    public void DeliverQuestObject(string objectName, GameObject actor)
    {
        if (objectName == currentQuest.GetObjectRequiredName())
        {
            currentQuestObjectAmount++;
            if (currentQuestObjectAmount >= currentQuest.GetObjectAmountRequired())
            {
                isCurrentQuestSucceed = true;
            }
        }
        else
        {
            isCurrentQuestSucceed = false;
        }

        SendQuestResolution(actor);
    }

    private void SendQuestResolution(GameObject actor)
    {
        /*if(actor.TryGetComponent<NPC>(out NPC npc))
        {
            npc.SetQuestResolution(isCurrentQuestSucceed);
        }*/ 
    }

    private void ShowQuestUI(QuestData quest)
    {
        // If Quest is timed, Show and Start timer
    }
}
