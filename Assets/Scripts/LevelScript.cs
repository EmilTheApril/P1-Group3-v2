using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    public int level;
    public int levelSceneIndex;
    public Image lockSprite;

    private LevelUnlocker levelUnlocker;

    private void Start()
    {
        levelUnlocker = FindObjectOfType<LevelUnlocker>();

        CheckIfLocked();
    }

    public void ShowLock()
    {
        if (!lockSprite.gameObject.activeInHierarchy)
        {
            lockSprite.gameObject.SetActive(true);
            CheckIfLocked();
        } else lockSprite.gameObject.SetActive(false);
    }

    public void CheckIfLocked()
    {
        if (levelUnlocker.isUnlocked[level - 1])
        {
            lockSprite.sprite = levelUnlocker.lockSprite[0];
        } else lockSprite.sprite = levelUnlocker.lockSprite[1];
    }

    public void PlayLevel()
    {
        if (levelUnlocker.isUnlocked[level - 1])
        {
            SceneManager.LoadScene(levelSceneIndex);
        }
    }
}
