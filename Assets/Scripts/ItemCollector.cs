using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public int level;
    private int trash = 0; //This is the start count of the stars.
    private int pointCount = 0;
    public AudioClip pickupTrash;
    public AudioClip pickupFish;
    [SerializeField] private int trashGoal;

    [SerializeField] private Slider trashSlider;

    public GameObject factPrefab;
    public GameObject questionPrefab;

    public Level levels;

    private void Start()
    {
        PointSetup();
        pointCount = trashGoal / levels.questions.Length;
        PlayerStatistics.instance.StartTimer();
    }

    private void Update()
    {
        LevelComplete();
    }

    public void LevelComplete()
    {
        if (levels.questions[levels.questions.Length - 1].isReached && Time.timeScale != 0 && level == PlayerStatistics.instance.stats.Length)
        {
            PlayerStatistics.instance.StopTimer(level);
            PlayerStatistics.instance.WriteCSV();
            SceneManager.LoadScene(2);
        }
        else if (levels.questions[levels.questions.Length - 1].isReached && Time.timeScale != 0)
        {
            PlayerStatistics.instance.StopTimer(level);
            FindObjectOfType<LevelUnlocker>().isUnlocked[level] = true;
            SceneManager.LoadScene(2);
        }
    }

    public void PointSetup()
    {
        for (int j = 0; j < levels.questions.Length; j++)
        {
            levels.questions[j].pointGoal = trashGoal / levels.questions.Length * (j + 1);
        }
    }

    public void CheckIfPointsReached()
    {
        int i = (pointCount / (trashGoal / levels.questions.Length)) - 1;

        if (levels.questions[i].pointGoal > trash && levels.questions[i].isReached) { return; }

        if (levels.questions[i].isFact)
        {
            GiveFact(levels, i);
        }
        else AskQuestion(levels, i);

        Destroy(GameObject.Find("SoundProp(Clone)"));

        SoundSystem.instance.PlaySound(levels.questions[i].soundClip);

        pointCount += trashGoal / levels.questions.Length;

        levels.questions[i].isReached = true;
    }

    public void GiveFact(Level level, int index)
    {
        //Instantiate facts box
        GameObject questionBox = Instantiate(factPrefab, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        questionBox.GetComponent<QuestionBox>().question = level.questions[index];
        questionBox.GetComponent<QuestionBox>().sprite = level.mascotSprite;
        questionBox.GetComponent<QuestionBox>().level = this.level;

        //Destroy
        Destroy(questionBox, 10f);
    }

    public void AskQuestion(Level level, int index)
    {
        //Freeze game
        Time.timeScale = 0;

        //Instantiate questionbox box
        GameObject questionBox = Instantiate(questionPrefab, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        questionBox.GetComponent<QuestionBox>().question = level.questions[index];
        questionBox.GetComponent<QuestionBox>().sprite = level.mascotSprite;
        questionBox.GetComponent<QuestionBox>().level = this.level;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trash")) //References to gameobjects with the tag created for the collectable item (star) attached to it.
        {
            Destroy(collision.gameObject); //The gameobject will disapear.
            PlayerStatistics.instance.TrashCollected(level);
            PlayerStatistics.instance.stats[level - 1].level = level;
            trash++; //This will add +1 star to the star-count.
            trashSlider.value = trash / (float)trashGoal; //This will let us know the star count works by showing it in the console.

            SoundSystem.instance.PlaySound(pickupTrash);

            if (trash == pointCount)
            {
                CheckIfPointsReached();
            }
        }

        if (collision.CompareTag("Fish"))
        {
            Destroy(collision.gameObject);

            PlayerStatistics.instance.FishCollected(level);
            PlayerStatistics.instance.stats[level - 1].level = level;

            SoundSystem.instance.PlaySound(pickupFish);

            if (trash > 0)
            {
                trash--;
                trashSlider.value = trash / (float)trashGoal;
            }
        }
    }
}

[System.Serializable]
public class Level
{
    public int level;
    public Sprite mascotSprite;
    public Question[] questions;
    public AudioClip rightSoundClip;
    public AudioClip wrongSoundClip;
}

[System.Serializable]
public class Question
{
    public string question;
    public int pointGoal;
    public bool isReached;
    public bool isFact;
    public AudioClip soundClip;
    public Answer[] answers;
}

[System.Serializable]
public class Answer
{
    public string answer;
    public bool isCorrect;
}
