using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    [SerializeField] private GameObject canvasGrid;
    public GameObject CanvasGrid => canvasGrid;
    public TextMeshProUGUI itemText;
    

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        canvasGrid.transform.parent.gameObject.SetActive(false);
    }

    
    public void InventoryCanvas(bool isInventory)
    {
        canvasGrid.transform.parent.gameObject.SetActive(isInventory);
    }
}
