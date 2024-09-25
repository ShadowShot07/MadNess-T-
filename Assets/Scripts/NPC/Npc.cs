using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Npc : MonoBehaviour, IInteractuable
{
    [SerializeField] private NPC_Data npcData;
    [SerializeField] private Sprite interactDialogueImage;

    private DialogueManager.Actors npcName;
    [SerializeField] private GameObject interactImage;
    
    private enum InteractionState
    {
        DIALOGUE,
        QUEST,
        NO_INTERACTABLE

    }

    private InteractionState currentInteractionState = InteractionState.DIALOGUE;

    private BoxCollider2D npcCollider;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        npcName = npcData.GetNpcName();
        spriteRenderer = GetComponent<SpriteRenderer>();
        npcCollider = GetComponent<BoxCollider2D>();
    }
    public void Interactuar(PlayerController player)
    {
        if (currentInteractionState == InteractionState.DIALOGUE)
        {
            interactImage.SetActive(false);
            DialogueManager.instance.StartDialogue(npcData.GetDialogueByLanguage(ScenesManager.instance.CurrentLanguage), transform.position);
            currentInteractionState = InteractionState.NO_INTERACTABLE;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (currentInteractionState) 
            {
                case InteractionState.DIALOGUE:

                    interactImage.GetComponent<SpriteRenderer>().sprite = interactDialogueImage;
                    interactImage.SetActive(true);
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactImage.SetActive(false);
        }
    }

}
