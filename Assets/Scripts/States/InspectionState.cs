public class InspectionState : IInteractionState
{
    public void EnterState(Interaction interaction)
    {
        interaction.GetInteractionStatus(true);
        interaction.CurrentInspection?.EnterInspection();
    }

    public void UpdateState(Interaction interaction)
    {
        interaction.CurrentInspection?.UpdateInspection(interaction.inspectionSensitivity);
        interaction.InteractionAnyObject();
    }

    public void ExitState(Interaction interaction)
    {
        interaction.GetInteractionStatus(false);
        interaction.CurrentInspection?.ExitInspection();
    }
}
