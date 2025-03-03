using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory")]
public class Inventory_SO : ScriptableObject
{
    public Image inventoryImage;
    [TextArea] public string inventoryText;
}
