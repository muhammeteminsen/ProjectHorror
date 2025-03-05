public interface IInteractionState
{
    void EnterState(Interaction interaction);
    void UpdateState(Interaction interaction);
    void ExitState(Interaction interaction);
}
