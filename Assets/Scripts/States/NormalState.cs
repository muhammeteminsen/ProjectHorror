using UnityEngine;

public class NormalState : IInteractionState
{
    public void EnterState(Interaction interaction)
    {
        interaction.UIController?.DialogueCanvasStatus(false);   
    }

    public void UpdateState(Interaction interaction)
    {
        interaction.InteractionAnyObject();
        if (Input.GetKeyDown(KeyCode.E) && interaction.Inventory?.CanvasGrid.transform.childCount>0)
        {
            interaction.ChangeState(new InventoryState());
        }
    }

    public void ExitState(Interaction interaction)
    {
        
    }
}
