using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public float spawnrate;

    void Start()
    {
        InvokeRepeating("SpawnFish", 0, 1/spawnrate);
    }

    public void SpawnFish()
    {
        Vector2 spawnPos = FindSpawnPosition();

        GameObject fish = Instantiate(fishPrefab, spawnPos, Quaternion.identity);

        if (spawnPos.x > 0)
        {
            fish.GetComponent<FishMovement>().moveLeft = true;
        }
    }

    public Vector2 FindSpawnPosition()
    {
        int x = Random.Range(0, 2);
        if (x == 0)
        {
            x = 1;
        }
        else x = -1;

        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        return new Vector2(screenPoint.x * x * 1.5f, Random.Range(-(screenPoint.y), screenPoint.y));
    }
}
