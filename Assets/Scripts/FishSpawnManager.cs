using UnityEngine;

public class FishSpawnManager : MonoBehaviour
{
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|This script is for managing the environment creatures which spawn. These are not samples. |
    //¬|Fish always swim (because they likely would in the sea!)                                  |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|


    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|Public list which will contain the prefabs to spawn.                                      |
    //¬|Public variables set to define where and when fish spawn.                                 |
    //!|I don't think these need to be public! I don't need to see them in the inspector?         |
    //¬|spawnRangeX sets the side to side value a fish would spawn at. This is in range 12 to -12.|
    //¬|spawnY sets the fish to spawn just out of view, at the top of the screen. This is set.    |
    //¬|Fish only spawn startDelay seconds into the game.                                         |
    //¬|Fish spawn at a rate of one fish every spawnInterval seconds.                             |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    public GameObject[] fishPrefabs;
    public float spawnRangeX = 12f;
    public float spawnY = 3f;
    public float spawnInterval = 3.5f;
    private float startDelay = 2;

    void Start()
    {
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|After the script has started and a startDelay, repeat the SpawnRandomFish functions every |
    //¬|spawnInterval seconds                                                                     |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //? will repeated call this method after 2 seconds have elapsed, and then every 1.5 seconds
        InvokeRepeating("SpawnRandomFish", startDelay, spawnInterval);
    }

    void SpawnRandomFish() {
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|Make a new x,y co-ordinate that the fish will spawn at, (left right is a range between the|
    //¬|spawnXRange value at + and -, y is hardcoded in so fish seem to come from out of sight.   |
    //¬|Pick a random fish from the list by generating a random index position from the fishPrefab|
    //¬| list - not as nice as random.choice()!                                                   |
    //¬| Make a copy of the fish prefab that was in that list position, at the point we said      |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        Vector2 spawnPos = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), spawnY);
        int fishIndex = Random.Range(0, fishPrefabs.Length);
        Instantiate(fishPrefabs[fishIndex], spawnPos, fishPrefabs[fishIndex].transform.rotation);
    }
}
