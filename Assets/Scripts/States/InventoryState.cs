public class InventoryState : IInteractionState
{
    public void EnterState(Interaction interaction)
    {
        interaction.Inventory?.InventoryCanvas(true);
    }

    public void UpdateState(Interaction interaction)
    {
       
    }

    public void ExitState(Interaction interaction)
    {
        interaction.Inventory?.InventoryCanvas(false);
    }
}
