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
        foreach (var questionText in questionTexts)
        {
            Button questionButton = questionText.GetComponent<Button>();
            questionButton.onClick.AddListener(()=> DialogueListener(questionTexts.IndexOf(questionText)));
        }
    }

    private void Start()
    {
        CrossImage.sprite = defaultCross;
    }

    public void SetQuestionsText(List<string> questions)
    {
        answerText.gameObject.SetActive(false);
        for (int i = 0; i < questionTexts.Count; i++)
        {
            Debug.Log(questionTexts[i].name);
            questionTexts[i].text = questions[i];
            questionTexts[i].gameObject.SetActive(true);
        }
    }

    public void SetAnswerText(string answer)
    {
        answerText.gameObject.SetActive(true);
        foreach (var questionText in questionTexts)
        {
            questionText.gameObject.SetActive(false);
        }
        answerText.text = answer;
        Debug.Log("Answer: " + answerText.text);
    }

    private void DialogueListener(int index)
    {
        DialogueEvent.OnNextDialogue?.Invoke(index);
    }
}