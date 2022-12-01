using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    public static PlayerStatistics instance;

    private string filename;
    private float timer;

    public LevelStatistics[] stats = new LevelStatistics[4];

    [System.Serializable]
    public struct LevelStatistics
    {
        public int level;
        public int trashCollected;
        public int fishCollected;
        public int correctAnswers;
        public int incorrectAnswers;
        public float time;
        public float score;
        public List<QuestionStatistics> questions;
    }

    [System.Serializable]
    public struct QuestionStatistics
    {
        public QuestionStatistics(string question, string answer, bool correct)
        {
            this.question = question;
            this.answer = answer;
            this.correct = correct;
        }

        public string question;
        public string answer;
        public bool correct;
    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    public void Update()
    {
        timer += Time.deltaTime;
    }

    public void Start()
    {
        filename = "./data.csv";
    }

    public void StartTimer()
    {
        timer = 0;
    }

    public void StopTimer(int level)
    {
        stats[level - 1].time = timer;
    }

    public void TrashCollected(int level)
    {
        stats[level - 1].trashCollected++;
        stats[level - 1].score += 10;
    }

    public void FishCollected(int level)
    {
        stats[level - 1].fishCollected++;
        stats[level - 1].score -= 5;
    }
    public void ScoreAdd(int level, float value)
    {
        stats[level - 1].score += value;
    }

    public void ScoreMultiply(int level, float value)
    {
        stats[level - 1].score *= value;
    }

    public void Answer(int level, Question question, int answerPickedIndex)
    {
        stats[level - 1].questions.Add(new QuestionStatistics(question.question, question.answers[answerPickedIndex].answer, question.answers[answerPickedIndex].isCorrect));

        if (question.answers[answerPickedIndex].isCorrect)
        {
            stats[level - 1].correctAnswers++;
        } else stats[level - 1].incorrectAnswers++;
    }

    public void WriteCSV()
    {
        TextWriter tw = new StreamWriter(filename, true);

        for (int i = 0; i < stats.Length; i++)
        {
            tw.WriteLine("Level " + stats[i].level);

            tw.WriteLine("Trash Collected, Fish Collected, Correct Answer, Incorrect Answer, Time, Score");

            tw.WriteLine($"{stats[i].trashCollected}, {stats[i].fishCollected}, {stats[i].correctAnswers}, {stats[i].incorrectAnswers}, {stats[i].time}, {stats[i].score}");

            for (int j = 0; j < stats[i].questions.Count; j++)
            {
                tw.WriteLine($"-----Question {j + 1}-----");
                tw.WriteLine("Question: " + stats[i].questions[j].question);
                tw.WriteLine("Asnwer: " + stats[i].questions[j].answer);
                tw.WriteLine("Correct: " + stats[i].questions[j].correct);
                tw.WriteLine("");
            }
            tw.WriteLine("");
        }

        tw.Close();
    }
}
