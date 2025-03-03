using UnityEngine;

public class PickUpAble : MonoBehaviour, IPickUpable
{
    [SerializeField] private Inventory_SO inventorySo;

   
    public void Pickup()
    {
        GameObject newInstance = Instantiate(inventorySo.inventoryImage.gameObject,
            Inventory.Instance.CanvasGrid.transform.position,Quaternion.identity,Inventory.Instance.CanvasGrid.transform);
        newInstance.transform.localRotation = Quaternion.identity;
        Inventory.Instance.itemText.text = inventorySo.inventoryText;
        Destroy(gameObject);
    }
}