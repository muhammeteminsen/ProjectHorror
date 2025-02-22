using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public RectTransform crossReact; 
    [HideInInspector] public Image crossImage;
    public Sprite defaultCross;
    public Sprite interactCross;

    private void Awake()
    {
        crossImage = crossReact.GetComponentInChildren<Image>();
    }
    private void Start()
    {
        crossImage.sprite = defaultCross;
    }
}
