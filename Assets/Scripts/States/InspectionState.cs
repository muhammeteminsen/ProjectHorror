using UnityEngine;

public class InspectionState : IInteractionState
{
    public void EnterState(Interaction interaction)
    {
        interaction.UIController.crossImage.enabled = false;
        interaction.PlayerMovement.enabled = false;
        interaction.PlayerRb.isKinematic = true;
        interaction.CurrentInspection?.EnterInspection();
    }

    public void UpdateState(Interaction interaction)
    {
        interaction.CurrentInspection?.UpdateInspection(interaction.inspectionSensitivity);
        interaction.InteractionAnyObject();
    }

    public void ExitState(Interaction interaction)
    {
        interaction.UIController.crossImage.enabled = true;
        interaction.PlayerMovement.enabled = true;
        interaction.PlayerRb.isKinematic = false;
        interaction.CurrentInspection?.ExitInspection();
    }
}
