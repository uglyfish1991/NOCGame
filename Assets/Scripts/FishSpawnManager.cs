using UnityEngine;

public class FishSpawnManager : MonoBehaviour
{
    //public game object array called animalPrefabs. This is visualised in the inspector with a drop down menu. 
    public GameObject[] fishPrefabs;
    public float spawnRangeX = 12f;
    public float spawnY = 3f;
    public float spawnInterval = 3.5f;
    private float startDelay = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //? will repeated call this method after 2 seconds have elapsed, and then every 1.5 seconds
        InvokeRepeating("SpawnRandomFish", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRandomFish() {
        Vector2 spawnPos = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), spawnY);
        int fishIndex = Random.Range(0, fishPrefabs.Length);
        Instantiate(fishPrefabs[fishIndex], spawnPos, fishPrefabs[fishIndex].transform.rotation);
    }
}
