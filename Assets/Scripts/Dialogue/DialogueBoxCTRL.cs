using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class DialogueBoxCTRL : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueTxt;
    public bool dialogueStarted;

    [SerializeField] private TMP_Text actorNameText;
    [SerializeField] private GameObject answerPanel;
    [SerializeField] private Button answer1Button;
    [SerializeField] private Button answer2Button;

    private DialogueSceneData currentDialogueSceneData;

    private List<DialogueManager.Actors> activeDialogueActors = new List<DialogueManager.Actors>();

    private string currentDialogueText;
    
    private List<DialogueAnswerData> answers;

    private DialogueAnswerData[]  answersArray;

    private int currentDialogueDataIndex = 0;

    private Movement inputMap;

    private bool isReadyForNewLine = false;
    private bool isAnswering = false;
    private bool isLastDialogue = false;

    private string lastBranchId;

    private int posibleSuccesPoints;
    private int currentSuccessPoints = 0;

    EventSystem eventSystem;


    private void Awake()
    {
        dialoguePanel.SetActive(false);
        answerPanel.SetActive(false);
        inputMap = new Movement();
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
    }

    void Update()
    {        
        if (inputMap.Player.SkipText.WasPressedThisFrame() && dialoguePanel.activeInHierarchy)
        {
            if (isReadyForNewLine)
            {
                NextDialogueLine();
                dialogueStarted = true;
            }
            else
            {
                if (!isAnswering)
                {
                    AudioManager.instance.PauseVoice(currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetActorName());
                    StopAllCoroutines();
                    dialogueTxt.text = currentDialogueText;
                    ShowAnswers();
                }
            }

        }
    }

    private void OnEnable()
    {
        inputMap.Player.Enable();
    }

    private void OnDisable()
    {
        inputMap.Player.Disable();
    }

    public void SetDialogueData(DialogueSceneData dialogueSceneData)
    {
        currentDialogueSceneData = dialogueSceneData;
        currentDialogueDataIndex = 0;
        if (!currentDialogueSceneData.IsEmpty())
        {
            currentDialogueText = currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetDialogue();
            answers = currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetAnswers();
            posibleSuccesPoints = AnswersInScene(currentDialogueSceneData);

            StartDialogue();
        }
    }

    private int AnswersInScene(DialogueSceneData dialogueSceneData)
    {
        int count = 0;
        foreach (DialogueData dialogue in dialogueSceneData.GetAllceneDialogues())
        {
            if (dialogue.GetAnswers().Count > 0)
            {
                count++;
            }
        }

        return count;
    }

    private void StartDialogue()
    {
        dialogueStarted = true;
        isReadyForNewLine = false;
        dialoguePanel.SetActive(true);
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
         isReadyForNewLine = false;
        currentDialogueDataIndex++;
        if (currentDialogueDataIndex < currentDialogueSceneData.DialogueCount() - 2) //Current dialogue hasn't got conclusion dialogues
        {
            if (lastBranchId != "")
            {
                if (currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetBranchId() != lastBranchId && currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetBranchId() != "")
                {
                    currentDialogueDataIndex++;
                }
                else if (lastBranchId == currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetBranchId())
                {
                    lastBranchId = "";
                }
            }
            else
            {
                if (currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetBranchId() != "")
                {
                    currentDialogueDataIndex++;
                }
            }
            currentDialogueText = currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetDialogue();
            answers = currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetAnswers();
            StartCoroutine(ShowLine());
        }
        else if (currentDialogueDataIndex >= currentDialogueSceneData.DialogueCount() - 2 && !isLastDialogue)
        {
            isLastDialogue = true;
            currentDialogueText = currentDialogueSceneData.GetConclusionDialogue(currentSuccessPoints, posibleSuccesPoints).GetDialogue();
            StartCoroutine(ShowLine()); 
        }
        else// Current dialogue has finished
        {
            foreach (DialogueManager.Actors actor in activeDialogueActors)
            {
                AudioManager.instance.StopVoice(actor);
            }
            activeDialogueActors.Clear();

            dialogueStarted = false;
            dialoguePanel.SetActive(false);
            // Activar movimiento del player de nuevo
            GlobalEventSystem.instance.dialogueEnded.Invoke();
            
        }
    }

    private IEnumerator ShowLine()
    {
        
        actorNameText.text = DialogueManager.instance.GetActorNameByLanguage(ScenesManager.instance.CurrentLanguage, currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetActorName());

        Vector3 screenPos = Camera.main.WorldToScreenPoint(GetActorPosition(currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetActorName()));
        Vector3 uiPos = new Vector3(screenPos.x, Screen.height - screenPos.y, screenPos.z);
        dialoguePanel.transform.position = uiPos;

        if (activeDialogueActors.Contains(currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetActorName()))
        {
            AudioManager.instance.UnPauseVoice(currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetActorName());
        }
        else 
        {
            AudioManager.instance.StartVoice(currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetActorName());
            activeDialogueActors.Add(currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetActorName());
        }
        //isAnswering = false;
        dialogueTxt.text = string.Empty;
        if (currentDialogueText != null)
        {
            foreach (char ch in currentDialogueText)
            {
                dialogueTxt.text += ch;
                if (!char.IsWhiteSpace(ch))
                {
                    yield return new WaitForSeconds(0.05f);
                }
                else 
                {
                    yield return new WaitForSeconds(0.1f);
                }
                
            }

            AudioManager.instance.PauseVoice(currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetActorName());

            // Comprobar si hay respuestas
            ShowAnswers();

        }
        else
        {
            print("dialogue is full");
        }
    }

    private void ShowAnswers()
    {
        //AudioManager.instance.PauseVoice(currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetActorName());
        if (answers.Count > 0)
        {
            isAnswering = true;
            // Randomizar respuestas

            List<DialogueAnswerData> copiedAnswers = new List<DialogueAnswerData>();
            for (int i=0; i<answers.Count;i++)
            {
                copiedAnswers.Add(answers[i]);
            }
            int random = Random.Range(0, 2);
            List<DialogueAnswerData> shuffledAnswers = new List<DialogueAnswerData>();
            shuffledAnswers.Add(copiedAnswers[random]);
            copiedAnswers.RemoveAt(random);
            shuffledAnswers.Add(copiedAnswers[0]);

            // Rellenar respuestas

            answer1Button.GetComponentInChildren<TMP_Text>().text = shuffledAnswers[0].GetAnswerText();
            answer2Button.GetComponentInChildren<TMP_Text>().text = shuffledAnswers[1].GetAnswerText();

            // Limpiamos antiguos listeners
            answer1Button.onClick.RemoveAllListeners();
            answer2Button.onClick.RemoveAllListeners();

            // Cada botón deberia tener el valor de respuesta
            answer1Button.onClick.AddListener(delegate { OnAnswerButtonPressed(shuffledAnswers[0].GetAnswerValue()); });
            answer2Button.onClick.AddListener(delegate { OnAnswerButtonPressed(shuffledAnswers[1].GetAnswerValue()); });


            // Mostrar respuestas
            answerPanel.SetActive(true); 
            eventSystem.firstSelectedGameObject = answer1Button.gameObject;
            eventSystem.SetSelectedGameObject(answer1Button.gameObject);
            eventSystem.sendNavigationEvents = true;
        }

        else
        {
            AudioManager.instance.PauseVoice(currentDialogueSceneData.GetSceneDialogue(currentDialogueDataIndex).GetActorName());
            // Siguiente DialogueData
            isReadyForNewLine = true;
            //lastBranchId = "";
        }
    }

    public void OnAnswerButtonPressed(int answerValue)
    {
        if (answerValue == 1)
        {
            lastBranchId = "A";
        }
        else 
        {
            lastBranchId = "B";
        }
        currentSuccessPoints += answerValue;
        print("Current succes points: " + currentSuccessPoints);
        //DialogueManager.instance.AddAnswerValueToDialogueScore(answerValue);
        isAnswering = false;
        answerPanel.SetActive(false);
        NextDialogueLine();
    }

    private Vector3 GetActorPosition(DialogueManager.Actors actor)
    {
        if (actor == DialogueManager.Actors.PRINCESS)
        {
            print(DialogueManager.instance.player.transform.position - new Vector3(0f, -2f));
            return DialogueManager.instance.player.transform.position - new Vector3(0f, -2f);
        }
        else
        {
            print(DialogueManager.instance.currentActorPosition);
            return DialogueManager.instance.currentActorPosition;
        }
    }
}
