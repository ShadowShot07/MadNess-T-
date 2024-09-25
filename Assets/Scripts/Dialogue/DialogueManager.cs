using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private int currentDialogueScore = 0;

    [SerializeField] private DialogueBoxCTRL dialogueBox;

    public Vector3 currentActorPosition;
    public GameObject player;
    [SerializeField] private PlayerController playerController;

    private List<Actors> convincedActors = new List<Actors>();

    public enum Actors
    {
        QUEEN,
        PRINCESS,
        NPC_ANTS,
        TWICE_SOLDIER_ANT,
        HYSTERICANT,
        NANNY_ANT,
        SCIANTIST,
        WIDE_EXOSKELETON
    }

    // Cambiar a diccionario con 3 idiomas



    public string[] actorNames_eng = new string[] { "Queen", "The Princess", "NPC Ants", "Twice Soldier Ant", "Hystericant", "Nanny Ant", "Sciantist", "Wide Exoskeleton" };
    public string[] actorNames_cat = new string[] { "Reina", "Princesa", "Formigues NPC", "Formiga doblement soldat", "Formiga histèrica", "Formiga mainadera", "Formiga 7-ciències", "Formiga amb exoesquelet ample" };
    public string[] actorNames_esp = new string[] { "Reina", "Princesa", "Hormigas NPC", "Hormiga doblemente soldado", "Hormiga histérica", "Hormiga niñera", "Hormiga 100-tífika", "Hormiga con exoesqueleto ancho" };


    public static DialogueManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Found more than one AudioManager in the scene!!!");
            Debug.LogError("Destroying new instance!!!");
            Destroy(this);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void AddConvincedActor(int value)
    {
        currentDialogueScore += value;
        if (currentDialogueScore >= 2)
        {
            
        }
    }

    public void AddConvincedActor(Actors actor)
    {
        convincedActors.Add(actor);
    }

    public string GetActorNameByLanguage(ScenesManager.Language language, DialogueManager.Actors actor)
    {
        if (language == ScenesManager.Language.Español)
        {
            return actorNames_esp[(int)actor];
        }
        else if (language == ScenesManager.Language.Ingles)
        {
            return actorNames_eng[(int)actor];

        }
        else if (language == ScenesManager.Language.Catala)
        {
            return actorNames_cat[(int)actor];

        }
        else
        {
            Debug.Log("Lenguaje no Soportado");
            return null;
        }
    } 

    public void StartDialogue(DialogueSceneData dialogueScene, Vector3 actorPosition)
    {
        dialogueBox.gameObject.SetActive(true);
        currentActorPosition = actorPosition;
        dialogueBox.SetDialogueData(dialogueScene);
        // Quitar movimiento player
        GlobalEventSystem.instance.dialogueStarted.Invoke();

    }

    public void EndDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
    }
}
