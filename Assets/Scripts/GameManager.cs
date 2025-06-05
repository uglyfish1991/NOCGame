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
    public bool rovIsMoving = true; // Everything moves/spawns while true
    public List<CollectibleSample> allCollectibles; // Assign in Inspector
    public List<CollectibleSample> chosenCollectibles = new List<CollectibleSample>();
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

    

    void Start()
    {
        PickRandomCollectibles();
        DisplayTargets();
    }

    void PickRandomCollectibles()
    {
        chosenCollectibles.Clear();

        List<CollectibleSample> tempList = new List<CollectibleSample>(allCollectibles);
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, tempList.Count);
            chosenCollectibles.Add(tempList[index]);
            tempList.RemoveAt(index); // prevents duplicates
        }
    }

    void DisplayTargets()
    {
        for (int i = 0; i < uiTargets.Length; i++)
        {
            if (i < chosenCollectibles.Count)
            {
                uiTargets[i].sprite = chosenCollectibles[i].sampleIcon;
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

                // Optional: Update UI here

                if (collectedSampleNames.Count == 3)
                {
                    Debug.Log("All targets collected! Ending game...");
                }
                return;
            }
        }
    }
}
