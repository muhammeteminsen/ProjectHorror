using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueEvent
{
    public static Action<Characters> OnStartDialogue;
    public static Action<int> OnNextDialogue;
    public static Action OnEndDialogue;
}

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private List<Dialogue_SO> dialogueSOList;
    private Dictionary<CharacterType, Dialogue_SO> dialogueDictionary = new Dictionary<CharacterType, Dialogue_SO>();
    private Characters currentCharacter;

    private void Awake()
    {
        InitializeDialogueDictionary();
    }

    private void OnEnable()
    {
        DialogueEvent.OnStartDialogue += StartDialogue;
        DialogueEvent.OnEndDialogue += EndDialogue;
        DialogueEvent.OnNextDialogue += NextDialogue;
    }


    private void OnDisable()
    {
        DialogueEvent.OnStartDialogue -= StartDialogue;
        DialogueEvent.OnEndDialogue -= EndDialogue;
        DialogueEvent.OnNextDialogue -= NextDialogue;
    }

    private void StartDialogue(Characters character)
    {
        currentCharacter = character;
        Debug.Log("Starting Dialogue");
        if (dialogueDictionary.TryGetValue(character.GetCharacterType(), out Dialogue_SO dialogueSo))
        {
            Dialogue firstDialogue = dialogueSo.dialogues[0];
            GetComponent<UIController>().SetQuestionsText(firstDialogue.questions);
        }
    }

    private void NextDialogue(int index)
    {
        if (dialogueDictionary.TryGetValue(currentCharacter.GetCharacterType(), out Dialogue_SO dialogueSo))
        {
            Dialogue firstDialogue = dialogueSo.dialogues[0];
            GetComponent<UIController>().SetAnswerText(firstDialogue.answer[index]);
            dialogueSo.dialogues.RemoveAt(0);
            dialogueSo.dialogues.Add(firstDialogue);
        }
    }

    private void EndDialogue()
    {
        
    }

    private void InitializeDialogueDictionary()
    {
        dialogueDictionary.Clear();
        foreach (var dialogueSo in dialogueSOList)
        {
            dialogueDictionary.TryAdd(dialogueSo.characterType, dialogueSo);
        }

        Debug.Log(dialogueDictionary.Count);
    }
}