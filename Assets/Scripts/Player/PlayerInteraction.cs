using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool isInteracting;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDoor door))
        {
            door.Open();
        }

        if (other.TryGetComponent(out Characters character))
        {
            if (isInteracting) return;
            Interaction interaction = FindAnyObjectByType<Interaction>();
            interaction.ChangeState(new DialogueState(character, other));
            other.GetComponent<Collider>().enabled = false;
            isInteracting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDoor door))
        {
            door.Close();
        }
        if (other.TryGetComponent(out Characters character))
        {
            Debug.Log(character.GetCharacterType());
            isInteracting = false;
        }
    }
}