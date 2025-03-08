using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject canvasGrid;
    [SerializeField] private Sprite defaultItemSprite;
    [SerializeField] private Sprite currentItemSprite;
    public GameObject CanvasGrid => canvasGrid;
    public TextMeshProUGUI itemText;
    private int _currentIndex;
    private List<Image> items = new List<Image>();
    private List<Inventory_SO> inventorySos = new List<Inventory_SO>();
    private void Awake()
    {
        canvasGrid.transform.parent.gameObject.SetActive(false);
    }

    
    public void InventoryCanvas(bool state)
    {
        canvasGrid.transform.parent.gameObject.SetActive(state);
    }

    public void SwitchItem()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        switch (scroll)
        {
            case < 0f:
                _currentIndex = (_currentIndex + 1) % canvasGrid.transform.childCount;
                break;
            case > 0f:
                _currentIndex = (_currentIndex - 1 + canvasGrid.transform.childCount) % canvasGrid.transform.childCount;
                break;
        }
        foreach (var item in items)
        {
            item.sprite = defaultItemSprite;
        }
        itemText.text = inventorySos[_currentIndex].inventoryText;
        items[_currentIndex].sprite = currentItemSprite;
        
    }

    public void PickupItem(Inventory_SO inventorySo)
    {
        GameObject newInstance = Instantiate(inventorySo.inventoryImage.gameObject, 
            CanvasGrid.transform.position,Quaternion.identity,CanvasGrid.transform);
        newInstance.transform.localRotation = Quaternion.identity;
        Image itemImage = newInstance.GetComponent<Image>();
        items.Add(itemImage);
        inventorySos.Add(inventorySo);
    }
}
