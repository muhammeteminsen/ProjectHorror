
public class NormalState : IInteractionState
{
    public void EnterState(Interaction interaction)
    {
        
    }

    public void UpdateState(Interaction interaction)
    {
        interaction.InteractionAnyObject();
    }

    public void ExitState(Interaction interaction)
    {
        
    }
}
