using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public Dialogue_SO dialogueSo;
    private Dictionary<CharacterType, Dialogue_SO> dialogueData = new Dictionary<CharacterType, Dialogue_SO>();

    private void Awake()
    {
        dialogueData.TryAdd(dialogueSo.characterType, dialogueSo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueData.TryGetValue(dialogueSo.characterType,out dialogueSo))
            {
                
            }
        }
       
    }
}