using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/newNPCData")]
public class NPC_Data : ScriptableObject
{
	[SerializeField] private DialogueManager.Actors npcName;
	[SerializeField] private Sprite npcSprite;
	[SerializeField] private DialogueSceneData npcDialogue;
	[SerializeField] private DialogueSceneData npcDialogueSpanish;
	[SerializeField] private DialogueSceneData npcDialogueCatala;


	public DialogueManager.Actors GetNpcName()
	{
		return npcName;
	}

	public Sprite GetNpcSprite()
    {
		return npcSprite;
    }

	public DialogueSceneData GetDialogueByLanguage(ScenesManager.Language language)
	{
		if (language == ScenesManager.Language.Español)
		{
			Debug.Log("Español");
			return npcDialogueSpanish;
		} 
		else if (language == ScenesManager.Language.Ingles)
		{
			Debug.Log("Ingles");
			return npcDialogue;

        } else if (language == ScenesManager.Language.Catala)
		{
			Debug.Log("Catala");
			return npcDialogueCatala;

        } else
		{
			Debug.Log("Lenguaje no Soportado");
			return null;
		}
	}
}
