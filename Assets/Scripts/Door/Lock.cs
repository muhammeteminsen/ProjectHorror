using System.Collections;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] private float angleRotate = 90f;
    [SerializeField] private float duration = 1f;
    public InteractType interactType;
    public bool IsOpenDoor { get; private set; }
    private Collider _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void OpenLockDoor(Interaction interaction)
    {
        if (!Input.GetButtonDown("Fire1")) return;
        var inventoryItem = interaction.Inventory.Items[interaction.Inventory.CurrentIndex]
            .GetComponent<InventoryItem>();
        var inventory = interaction.Inventory;
        if (inventoryItem.interactType == interactType)
        {
            Destroy(_collider);
            StopAllCoroutines();
            StartCoroutine(
                LerpController(transform.GetChild(0).rotation, Quaternion.Euler(0, angleRotate, 0), duration));
            Destroy(inventoryItem.gameObject);
            inventory.Items.RemoveAt(interaction.Inventory.CurrentIndex);
            inventory.InventorySos.RemoveAt(interaction.Inventory.CurrentIndex);
            interaction.ChangeState(new NormalState());
            
        }
        else
        {
            Debug.LogWarning("Not Same Item!!");
        }

    }

    public void CheckForEscape(Interaction interaction)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            interaction.ChangeState(new NormalState());
        }
    }

    private IEnumerator LerpController(Quaternion from,
        Quaternion to, float coroutineDuration)
    {
        IsOpenDoor = true;
        float elapsed = 0f;
        while (elapsed < coroutineDuration)
        {
            transform.GetChild(0).rotation = Quaternion.Lerp(from, to, elapsed / coroutineDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.GetChild(0).rotation = to;
        IsOpenDoor = false;
        Destroy(this);
    }
}