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
    public int CurrentIndex { get; private set; }
    public List<Image> Items { get; private set; } = new List<Image>();
    public List<Inventory_SO> InventorySos { get; private set; } = new List<Inventory_SO>();

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
                CurrentIndex = (CurrentIndex + 1) % canvasGrid.transform.childCount;
                break;
            case > 0f:
                CurrentIndex = (CurrentIndex - 1 + canvasGrid.transform.childCount) % canvasGrid.transform.childCount;
                break;
        }

        foreach (var item in Items)
        {
            item.sprite = defaultItemSprite;
        }

        itemText.text = InventorySos[CurrentIndex].inventoryText;
        Items[CurrentIndex].sprite = currentItemSprite;
    }

    public void PickupItem(Inventory_SO inventorySo)
    {
        GameObject newInstance = Instantiate(inventorySo.inventoryImage.gameObject,
            CanvasGrid.transform.position, Quaternion.identity, CanvasGrid.transform);
        newInstance.transform.localRotation = Quaternion.identity;
        Image itemImage = newInstance.GetComponent<Image>();
        Items.Add(itemImage);
        InventorySos.Add(inventorySo);
    }
}