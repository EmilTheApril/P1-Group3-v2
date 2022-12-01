using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionBox : MonoBehaviour
{
    public Question question;

    public int level;

    public Sprite sprite;
    public Sprite wrongAnswer;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] answerText;

    public void Start()
    {
        transform.GetChild(1).GetComponent<Image>().sprite = sprite;
        SetupQuestion();
    }
    public void SetupQuestion()
    {
        if (GameObject.Find("Fact box(Clone)") != gameObject)
        {
            Destroy(GameObject.Find("Fact box(Clone)"));
        }

        questionText.text = question.question;

        for (int i = 0; i < answerText.Length; i++)
        {
            answerText[i].text = question.answers[i].answer;
        }
    }

    public void AnswerPicked(int index)
    {
        PlayerStatistics.instance.Answer(level, question, index);

        foreach (GameObject sound in GameObject.FindGameObjectsWithTag("Sound"))
        {
            Destroy(sound);
        }

        if (question.answers[index].isCorrect)
        {
            CorrectAnswer();
        }
        else WrongAnswer(index);
    }

    public void CorrectAnswer()
    {
        PlayerStatistics.instance.ScoreMultiply(level, 3);
        Time.timeScale = 1;
        SoundSystem.instance.PlaySound(GameObject.Find("Player").GetComponent<ItemCollector>().levels.rightSoundClip);
        Destroy(gameObject);
    }

    public void WrongAnswer(int index)
    {
        PlayerStatistics.instance.ScoreMultiply(level, 0.75f);
        SoundSystem.instance.PlaySound(GameObject.Find("Player").GetComponent<ItemCollector>().levels.wrongSoundClip);
        transform.GetChild(0).GetChild(index + 1).GetComponent<Image>().sprite = wrongAnswer;
        transform.GetChild(0).GetChild(index + 1).GetComponent<Button>().interactable = false;
    }
}
