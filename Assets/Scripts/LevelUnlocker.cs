using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlocker : MonoBehaviour
{
    public static LevelUnlocker instance;
    public bool[] isUnlocked;
    public Sprite[] lockSprite;

    public void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

}
