using UnityEngine;

public class DialogueState : IInteractionState
{
    private readonly Characters character;
    private readonly Collider other;
    public DialogueState(Characters character,Collider other)
    {
        this.character = character;
        this.other = other;
    }

    public void EnterState(Interaction interaction)
    {
        DialogueEvent.OnStartDialogue?.Invoke(character, other);
        interaction.GetInteractionStatus(true);
    }

    public void UpdateState(Interaction interaction)
    {
        
    }

    public void ExitState(Interaction interaction)
    {
        
    }
}