using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
    public bool rovIsMoving;// Everything moves/spawns while true
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rovIsMoving = !rovIsMoving;
            Debug.Log("ROV movement toggled: " + (rovIsMoving ? "RESUMED" : "PAUSED"));
        }
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
        foreach (var target in chosenCollectibles)
        {
            if (target.sampleName == collectedName && !collectedSampleNames.Contains(collectedName))
            {
                collectedSampleNames.Add(collectedName);
                Debug.Log("Collected target: " + collectedName);

                if (collectedSampleNames.Count == 3)
                {
                    Debug.Log("All targets collected!");
                }
                return;
            }
        }
    }
}
