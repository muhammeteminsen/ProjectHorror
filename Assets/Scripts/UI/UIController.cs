using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public RectTransform crossReact;
    public Image CrossImage { get; private set; }
    public Sprite defaultCross;
    public Sprite interactCross;
    [SerializeField] private List<TextMeshProUGUI> questionTexts;
    [SerializeField] private TextMeshProUGUI answerText;

    private void Awake()
    {
        CrossImage = crossReact.GetComponentInChildren<Image>();
    }

    private void Start()
    {
        CrossImage.sprite = defaultCross;
    }

    public void SetQuestionsText(string question)
    {
        DialogueTextsStatus(true);
        foreach (var questionText in questionTexts)
        {
            questionText.text = question;
            Debug.Log("Question: " + questionText.text);
        }
    }

    public void SetAnswerText(string answer)
    {
        DialogueTextsStatus(false);
        answerText.text = answer;
        Debug.Log("Answer: " + answerText.text);
    }


    private void DialogueTextsStatus(bool status)
    {
        foreach (var questionText in questionTexts)
            questionText.gameObject.SetActive(status);
        answerText.gameObject.SetActive(!status);
    }
    
}