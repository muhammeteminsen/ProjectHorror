using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueEvent
{
    public static Action<Characters> OnStartDialogue;
    public static Action<Characters> OnNextDialogue;
    public static Action<Characters> OnEndDialogue;
}

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private List<Dialogue_SO> dialogueSOList;
    private Dictionary<CharacterType, Dialogue_SO> dialogueDictionary = new Dictionary<CharacterType, Dialogue_SO>();

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
        Debug.Log("Starting Dialogue");
        if (dialogueDictionary.TryGetValue(character.GetCharacterType(), out Dialogue_SO dialogueSo))
        {
            foreach (var question in dialogueSo.dialogues[0].questions)
            {
                if (!TryGetComponent(out UIController uiController)) return;
                uiController.SetQuestionsText(question);
            }
        }
    }

    private void NextDialogue(Characters character)
    {
    }

    private void EndDialogue(Characters character)
    {
        if (dialogueDictionary.TryGetValue(character.GetCharacterType(), out Dialogue_SO dialogueSo))
        {
            if (!TryGetComponent(out UIController uiController)) return;
            uiController.SetAnswerText(dialogueSo.dialogues[0].answer);
            Dialogue firstDialogue = dialogueSo.dialogues[0];
            dialogueSo.dialogues.RemoveAt(0);
            dialogueSo.dialogues.Add(firstDialogue);
        }
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