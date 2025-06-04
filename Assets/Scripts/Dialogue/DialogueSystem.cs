using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueEvent
{
    public static Action<Characters,Collider> OnStartDialogue;
    public static Action<int> OnNextDialogue;
    public static Action<Interaction> OnEndDialogue;
}

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private List<Dialogue_SO> dialogueSOList;
    private Dictionary<CharacterType, Dialogue_SO> dialogueDictionary = new Dictionary<CharacterType, Dialogue_SO>();
    private Characters currentCharacter;
    private UIController uiController;
    private Collider interactionCollider;

    private void Awake()
    {
        InitializeDialogueDictionary();
        uiController = GetComponent<UIController>();
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

    private void StartDialogue(Characters character,Collider other)
    {
        Debug.Log("Starting Dialogue");
        if (dialogueDictionary.TryGetValue(character.GetCharacterType(), out Dialogue_SO dialogueSo))
        {
            Cursor.lockState = CursorLockMode.None;
            uiController.DialogueCanvasStatus(true);
            interactionCollider = other;
            currentCharacter = character;
            Dialogue firstDialogue = dialogueSo.dialogues[0];
            uiController.QuestionsTextStatus();
            uiController.SetQuestionText(firstDialogue.questions);
        }
    }

    private void NextDialogue(int index)
    {
        if (dialogueDictionary.TryGetValue(currentCharacter.GetCharacterType(), out Dialogue_SO dialogueSo))
        {
            Dialogue previousDialogue = dialogueSo.dialogues[0];
            uiController.SetAnswerText(previousDialogue.answer[index]);
            uiController.AnswerTextStatus();
            dialogueSo.dialogues.RemoveAt(0);
            dialogueSo.dialogues.Add(previousDialogue);
            Dialogue nextDialogue = dialogueSo.dialogues[0];
            uiController.SetQuestionText(nextDialogue.questions);
        }
    }

    private void EndDialogue(Interaction interaction)
    {
        Cursor.lockState = CursorLockMode.Locked;
        uiController.DialogueCanvasStatus(false);
        interaction.GetInteractionStatus(false);
        interactionCollider.enabled = true;
        interaction.ChangeState(new NormalState());
    }

    private void InitializeDialogueDictionary()
    {
        dialogueDictionary.Clear();
        foreach (var dialogueSo in dialogueSOList)
        {
            dialogueDictionary.TryAdd(dialogueSo.characterType, dialogueSo);
        }   
    }
}