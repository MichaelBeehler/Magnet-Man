using System;

[Serializable]
public class QuizQuestion
{
    public string questionId;
    public string concept;

    public string questionText;

    public string[] answers = new string[4];

    public int correctAnswer;
}