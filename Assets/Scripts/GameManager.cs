using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class CollectibleSample
    {
        public string sampleName;
        public Sprite sampleIcon;
    }

    public static GameManager instance;

    public GameObject chosenItemsCanvas;
    public float movementMultiplier = 1f; // Will lerp between 0 and 1
    public float smoothSpeed = 2f;
    public bool rovIsMoving;// Everything moves/spawns while true

    public bool gameIsWon;
    public List<SampleData> allCollectibles; // <-- use SampleData here
    public List<SampleData> chosenCollectibles = new List<SampleData>();
    public List<string> collectedSampleNames = new List<string>();

    public Image[] uiTargets; // Assign 3 UI Image slots in Inspector
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        rovIsMoving = false;  //Ensure movement is OFF at game load
        movementMultiplier = 0f; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rovIsMoving = !rovIsMoving;
            Debug.Log("ROV movement toggled: " + (rovIsMoving ? "RESUMED" : "PAUSED"));
        }

        // Smooth transition of movementMultiplier
        float target = rovIsMoving ? 1f : 0f;
        movementMultiplier = Mathf.MoveTowards(movementMultiplier, target, smoothSpeed * Time.deltaTime);
    }



    public void StartGame()
    {
        PickRandomCollectibles();
        DisplayTargets();
        chosenItemsCanvas.SetActive(true);
        rovIsMoving = true;
        Debug.Log("Game started.");
    }

    void PickRandomCollectibles()
    {
        chosenCollectibles.Clear();

        List<SampleData> tempList = new List<SampleData>(allCollectibles);
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, tempList.Count);
            chosenCollectibles.Add(tempList[index]);
            tempList.RemoveAt(index);
        }
    }

    void DisplayTargets()
    {
        for (int i = 0; i < uiTargets.Length; i++)
        {
            if (i < chosenCollectibles.Count)
            {
                uiTargets[i].sprite = chosenCollectibles[i].sampleImage;
                uiTargets[i].preserveAspect = true;
                uiTargets[i].enabled = true;
            }
            else
            {
                uiTargets[i].enabled = false;
            }
        }
    }

    public void CheckCollected(string collectedName)
    {
        for (int i = 0; i < chosenCollectibles.Count; i++)
        {
            var target = chosenCollectibles[i];
            if (target.sampleName == collectedName && !collectedSampleNames.Contains(collectedName))
            {
                collectedSampleNames.Add(collectedName);
                Debug.Log("Collected target: " + collectedName);

                // Show tick overlay on the corresponding UI image
                Transform tick = uiTargets[i].transform.Find("TickOverlay");
                if (tick != null)
                {
                    tick.gameObject.SetActive(true);
                }

                if (collectedSampleNames.Count == 3)
                {
                    GameManager.instance.gameIsWon = true;
                    Debug.Log("All targets collected!");
                }
                return;
            }
        }
    }

    public void PlayGameAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
