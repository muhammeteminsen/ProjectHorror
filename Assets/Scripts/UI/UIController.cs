using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        CrossImage = crossReact.GetComponentInChildren<Image>();
        foreach (var questionText in questionTexts)
        {
            Button questionButton = questionText.GetComponent<Button>();
            questionButton.onClick.AddListener(()=> NextDialogueListener(questionTexts.IndexOf(questionText)));
        }
        exitButton.onClick.AddListener(ExitDialogueListener);
    }

    private void Start()
    {
        CrossImage.sprite = defaultCross;
    }
    
    public void QuestionsTextStatus()
    {
        answerText.gameObject.SetActive(false);
        foreach (var question in questionTexts)
        {
            Debug.Log(question.name);
            question.gameObject.SetActive(true);
        }
    }
    public void SetQuestionText(List<string> questions)
    {
        for (int i = 0; i < questionTexts.Count; i++)
        {
            questionTexts[i].text = questions[i];
        }
    }

    public void SetAnswerText(string answer)
    {
        answerText.text = answer;
    }
    public void AnswerTextStatus()
    {
        answerText.gameObject.SetActive(true);
        foreach (var questionText in questionTexts)
        {
            questionText.gameObject.SetActive(false);
        }
        Debug.Log("Answer: " + answerText.text);
        StartCoroutine(DelayedQuestionDialogue(1f));
    }

    private IEnumerator DelayedQuestionDialogue(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (var questionText in questionTexts)
        {
            questionText.gameObject.SetActive(true);
        }
    }
    public void DialogueCanvasStatus(bool status)
    {
        dialogueCanvas.gameObject.SetActive(status);
    }
    
    private void NextDialogueListener(int index)
    {
        DialogueEvent.OnNextDialogue?.Invoke(index);
    }

    private void ExitDialogueListener()
    {
        DialogueEvent.OnEndDialogue?.Invoke(GetComponent<Interaction>());
    }
    
}