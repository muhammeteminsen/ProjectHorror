using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDoor door))
        {
            door.Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDoor door))
        {
            door.Close();
        }
        
    }
}