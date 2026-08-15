using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Data.Common;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;
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

    private QuizType currentQuizType;
    private QuizDestination destination;
    private int currentLevel;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    } 

    void Start()
    {
        quizPanel.SetActive(false);
        nextButton.onClick.AddListener(NextQuestion);
    }

    public void StartQuiz(QuizType quizType, QuizDestination destination)
    {
        currentQuizType = quizType;
        this.destination = destination;

        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }

        currentQuestionIndex = 0;
        quizPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ShowQuestion();
    }

    public void StartPreLevelQuiz(int level)
    {
        currentLevel = level;

        StartQuiz(
            QuizType.Prelevel,
            QuizDestination.Level
        );
    }

    public void StartPostLevelQuiz(int level)
    {
        currentLevel = level;
        StartQuiz(QuizType.Postlevel, QuizDestination.Level);
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

    public void LoadNextLevel ()
    {
        int nextLevel = currentLevel + 1;

        SceneManager.LoadScene("Chamber" + nextLevel);
    }

    void EndQuiz()
    {
        quizPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Quiz Complete!");

        if (currentQuizType == QuizType.Prelevel)
        {
            SceneManager.LoadScene("Chamber" + currentLevel); //Loads Level 1 for now
        }
        else if (currentQuizType == QuizType.Postlevel)
        {
            LoadNextLevel();
        }
    }
}