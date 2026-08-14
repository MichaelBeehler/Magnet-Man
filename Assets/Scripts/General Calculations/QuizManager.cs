using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class QuizManager : MonoBehaviour
{

    [Header("Questions")]
    public List<QuizQuestion> questions = new List<QuizQuestion>();

    [Header("Main Menu")]
    public GameObject mainMenuPanel;

    [Header("UI")]
    public GameObject quizPanel;
    public TMP_Text questionText;
    public TMP_Text feedbackText;

    public Button[] answerButtons;
    public Button nextButton;

    private int currentQuestionIndex;
    private bool answered;

    void Start()
    {
        quizPanel.SetActive(false);
        nextButton.onClick.AddListener(NextQuestion);
    }

    public void StartQuiz()
    {

        mainMenuPanel.SetActive(false);
        currentQuestionIndex = 0;
        quizPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ShowQuestion();
    }

    void ShowQuestion()
    {
        answered = false;
        feedbackText.text = "";

        nextButton.gameObject.SetActive(false);

        QuizQuestion question = questions[currentQuestionIndex];

        questionText.text = question.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;

            answerButtons[i].GetComponentInChildren<TMP_Text>().text = question.answers[i];

            answerButtons[i].interactable = true;

            answerButtons[i].onClick.RemoveAllListeners();

            answerButtons[i].onClick.AddListener(
                () => SelectAnswer(index));
        }
    }

    void SelectAnswer(int answerIndex)
    {
        if (answered)
        {
            return;
        }

        answered = true;

        QuizQuestion question = questions[currentQuestionIndex];

        if (answerIndex == question.correctAnswer)
        {
            // Set some variable to true, showing answer was correct
            //feedbackText.text = "Correct";
        }
        else
        {
            //feedbackText.text = "Incorrect";
        }

        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }

        nextButton.gameObject.SetActive(true);
    }

    void NextQuestion()
    {
        currentQuestionIndex ++;

        if (currentQuestionIndex >= questions.Count)
        {
            EndQuiz();
            return;
        }
        ShowQuestion();
    }

    void EndQuiz()
    {
        quizPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        Debug.Log("Quiz Complete!");
    }
}