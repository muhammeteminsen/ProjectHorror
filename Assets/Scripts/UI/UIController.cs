using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public RectTransform crossReact;
    public Image CrossImage;
    public Sprite defaultCross;
    public Sprite interactCross;

    private void Awake()
    {
        CrossImage = crossReact.GetComponentInChildren<Image>();
    }
    private void Start()
    {
        CrossImage.sprite = defaultCross;
    }
}
