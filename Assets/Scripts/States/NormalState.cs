using UnityEngine;

public class NormalState : IInteractionState
{
    public void EnterState(Interaction interaction)
    {
        
    }

    public void UpdateState(Interaction interaction)
    {
        interaction.InteractionAnyObject();
        if (Input.GetKeyDown(KeyCode.E))
        {
            interaction.ChangeState(new InventoryState());
        }
    }

    public void ExitState(Interaction interaction)
    {
        
    }
}
