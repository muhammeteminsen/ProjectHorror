using UnityEngine;

public class PickUpAble : MonoBehaviour,IPickUpable
{
    public void Pickup()
    {
        Destroy(gameObject);
    }
}
