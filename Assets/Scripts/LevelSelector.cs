using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class LevelSelector : MonoBehaviour
{
    public Image[] theButtons;

    void Start()
    {
        for(int i = 0; i < theButtons.Length; i++)
        // Sets the minimum amount a image should be visible before it is conidered as part of the button
        theButtons[i].alphaHitTestMinimumThreshold = 1f;
    }
}