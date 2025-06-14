using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|This script spawns random collectables above the screen, with random rotation.            |
    //¬|It only runs while the ROV is moving (see GameManager.rovIsMoving).                       |
    //¬|If the spawned item is one of the chosen target samples, it gets a particle system added. |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    public GameObject[] spawningElements;
    public GameObject highlightEffectPrefab;
    private float spawnRangeX = 6f;
    private float spawnY = 7f;
    public float spawnInterval = 1.5f;

    void Start()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Starts the repeating spawn loop using a coroutine.                                        |
        //¬|This loop never ends, but it pauses while the ROV isn’t moving.                           |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Infinite loop that constantly checks whether the ROV is moving.                           |
        //¬|When it is, spawns an element and waits for the set interval before repeating.            |
        //¬|If the ROV is not moving, yield return null keeps checking each frame.                    |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        while (true)
        {
            while (!GameManager.instance.rovIsMoving)
            {
                yield return null;
            }
            SpawnRandomElement();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomElement()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Spawns a random prefab from the list at a random X position, just off-screen above.       |
        //¬|Applies a random Z rotation to make it look more natural/varied.                          |
        //¬|If the item is one of the "chosenCollectibles" from GameManager, add the particle system. |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        Vector2 spawnPos = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), spawnY);
        int elementIndex = Random.Range(0, spawningElements.Length);
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        GameObject newObj = Instantiate(spawningElements[elementIndex], spawnPos, randomRotation);

        CollectableItem item = newObj.GetComponent<CollectableItem>();
        if (item != null && item.data != null)
        {
            if (GameManager.instance.chosenCollectibles.Contains(item.data))
            {
                //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
                //¬|Highlight prefab is a child of the item and centered on it.                               |
                //¬|This sets the rotation of the highlight FX to "identity" = no rotation at all.            |
                //¬|Quaternion is Unity's way of storing rotation in 3.  Quaternion.identity is the default   |
                //¬|rotation (like Euler(0, 0, 0) but Euler might cause gimbal lock.) There is no spin or tilt|
                //¬|This means even if the sample is rotated, the particles will shoot upwards and be         |
                //¬|consistent                                                                                |
                //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
                GameObject fx = Instantiate(highlightEffectPrefab, newObj.transform);
                fx.transform.localPosition = Vector3.zero;
                fx.transform.rotation = Quaternion.identity;
            }
        }
    }
}
