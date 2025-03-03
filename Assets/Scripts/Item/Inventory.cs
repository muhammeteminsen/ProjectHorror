using System;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    [SerializeField] private GameObject canvasGrid;
    public GameObject CanvasGrid => canvasGrid;
    public TextMeshProUGUI itemText;

    private bool _isInventory;

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

    private void Update()
    {
        InventoryCanvas();
    }

    private void InventoryCanvas()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _isInventory = !_isInventory;
            canvasGrid.transform.parent.gameObject.SetActive(_isInventory);
        }
    }
}
