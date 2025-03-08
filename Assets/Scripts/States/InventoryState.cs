using UnityEngine;

public class InventoryState : IInteractionState
{
    public void EnterState(Interaction interaction)
    {
        interaction.Inventory?.InventoryCanvas(true);
        interaction.GetInteractionStatus(true);
    }

    public void UpdateState(Interaction interaction)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interaction.ChangeState(new NormalState());
        }
        interaction.Inventory?.SwitchItem();
    }

    public void ExitState(Interaction interaction)
    {
        interaction.Inventory?.InventoryCanvas(false);
        interaction.GetInteractionStatus(false);
    }
}
