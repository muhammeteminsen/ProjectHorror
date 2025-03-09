using UnityEngine;

public class PickUpAble : MonoBehaviour, IPickUpable
{
    public Inventory_SO inventorySo;
    public void Pickup(Interaction interaction)
    {
        interaction.Inventory?.PickupItem(inventorySo);
        interaction.GetInteractionStatus(false);
        interaction.CurrentInspection?.ExitInspection();
        Destroy(gameObject);
    }
}