using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawningElements;
    private float spawnRangeX = 6f;
    private float spawnY = 7f;
    public float spawnInterval = 1.5f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Wait until movement is allowed
            while (!GameManager.instance.rovIsMoving)
            {
                yield return null; // wait a frame
            }

            SpawnRandomElement();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomElement()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), spawnY);
        int elementIndex = Random.Range(0, spawningElements.Length);
        Instantiate(spawningElements[elementIndex], spawnPos, Quaternion.identity);
    }
}
