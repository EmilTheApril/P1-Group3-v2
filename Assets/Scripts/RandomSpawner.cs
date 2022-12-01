using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    private float screenHeight;   // Height of screen
    public float spawnAngle;      // Used to calculate spawnpoint
    public Vector3 spawnPos;      // Used to set the position the item spawns in
    public GameObject prefab;   // array of prefabs to instantiate
    public float spawnRate;       // spawnrate - spawn prefabs at this rate

    void Start()
    {
        // InvokeRepeating - Call spawnobject function at spawnrate
        InvokeRepeating("SpawnObject", 1/spawnRate, 1/spawnRate);
    }

    void SpawnObjectAtRandom()
    {
        screenHeight = Camera.main.orthographicSize+1; // What the camera can see + 1 so the image doesn't clip into the scene
        spawnAngle = Random.Range(-Mathf.PI, Mathf.PI); // Math.PI: "Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π."

        // Uses Cos and Sin and a random value between -PI and PI * the camera height*2 to decide where on the circle to put the spawnpoint
        spawnPos = new Vector3(Mathf.Cos(spawnAngle), Mathf.Sin(spawnAngle), 0) * screenHeight * 2; 
    }

    void SpawnObject()
    {
        float randomRotation = Random.Range(0, 360); // Random range from 0-360 (The amount of degrees in a circle)

        SpawnObjectAtRandom(); // Get the Spawnpoint and endpoint for the spawn

        // Instantiate the item at spawnPos
        GameObject spawnedItem = Instantiate(prefab, spawnPos, Quaternion.identity);

        // Rotate the spawned item randomly
        spawnedItem.transform.Rotate(0, 0, randomRotation, Space.Self);

        float border = 5f;
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 target = new Vector3(Random.Range(-(screenPoint.x) + border, screenPoint.x - border), Random.Range(-(screenPoint.y) + border, screenPoint.y - border), 0);
        Vector2 dir =  (target - spawnedItem.transform.position);
        dir.Normalize();
        
        // Move the object towards the endPoint (See GarbageMovement script)
        spawnedItem.GetComponent<GarbageMovement>().dir = dir;
    }
}
