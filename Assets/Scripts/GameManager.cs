using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
//¬|This script is the main gameplay manager. It controls the main variables, keeps track of  |
//¬|movement, and picks the three random targets.                                             |
//¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
public class GameManager : MonoBehaviour
{
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|I think this is a remainder from a previous version, but I'm not going to delete it just  |
    //¬|yet                                                                                       |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    [System.Serializable]
    public class CollectibleSample
    {
        public string sampleName;
        public Sprite sampleIcon;
    }

    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|Game Manager - static so it's a singleton. This means I can use the "this" keyword and not|
    //¬|have to make loads of references everywhere                                               |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    public static GameManager instance;

    public GameObject chosenItemsCanvas;
    public float movementMultiplier = 1f; 
    public float smoothSpeed = 2f;
    public bool rovIsMoving;

    public bool gameIsWon;
    public List<SampleData> allCollectibles; 
    public List<SampleData> chosenCollectibles = new List<SampleData>();
    public List<string> collectedSampleNames = new List<string>();

    public Image[] uiTargets; // Assign 3 UI Image slots in Inspector
    void Awake()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Very first thing that happens                                                             |
        //¬|Sets up singleton pattern (instance = this), and destroys any others that try to create.  |
        //¬|There is only one GameManager, and all other scripts know where it is to reference it.    |
        //¬|Sets movement values to false / 0 - they SHOULD default to false anyway, but just in case.|              
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|   
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        rovIsMoving = false; 
        movementMultiplier = 0f;
    }

    void Update()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Update runs per frame                                                                     |
        //¬|If the space bar is pressed, flip the bool. Other scrips then swap their movement based on|
        //¬|that.                                                                                     |         
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rovIsMoving = !rovIsMoving;
            Debug.Log("ROV movement toggled: " + (rovIsMoving ? "RESUMED" : "PAUSED"));
        }

        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|This manages the "smoother movement" of the ROV, when it was just harsh start/stop in     |
        //¬|earlier versions. Ternary operator - one line if/else. (if rovIM==true, movement = 1)     |
        //¬|that.                                                                                     |         
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        float target = rovIsMoving ? 1f : 0f;
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|REMEMBER - this happens once per frame!!!!!                                               |
        //¬|Mathf.MoveTowards increments towards whatever the movementMultiplier is step by step.     |
        //¬|It doesn't take long at all - but works very well.                                        |         
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        movementMultiplier = Mathf.MoveTowards(movementMultiplier, target, smoothSpeed * Time.deltaTime);
    }



    public void StartGame()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Game set up - couldn't be called as Start() because it needs to be triggered specifically.|
        //¬|Calls the set up functions, makes the 3 chosen images panel active, and flips the         |
        //¬|rovIsMoving bool to true, which other scripts rely on (movement scripts mainly)           |         
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|   
        PickRandomCollectibles();
        DisplayTargets();
        chosenItemsCanvas.SetActive(true);
        rovIsMoving = true;
        Debug.Log("Game started.");
    }

    void PickRandomCollectibles()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Function for picking the three collectible items                                          |
        //¬|Clears the list of collectibles - just incase! Useful for game replays.                   |
        //¬|Runs a for loop  - one iteration per item in list. I prefer Python's syntax=[             |         
        //¬|We copy the allCollectibles list (filled in in the editor) because we'll be changing it,  |
        //¬|and don't want to impact the original copy.                                               |
        //¬|We pick a random item by generating an int which corresponds to an index position, we add |
        //¬|the item at that index position from the temp list to the chosen list, and then remove it |
        //¬|amazed there is no random.choice() equivialent?                                           |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|   
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
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Function to show the three chosen items' icons in the slots at the top of the screen.     |
        //¬|Runs a for loop  - one iteration per slot in the uiTargets collection. (Should always be  |
        //¬|3, realistically.)                                                                        |
        //¬|Assigns the sprite from the chosenCollectible in position [i] to the UI slot at position  |
        //¬|[i], sets it to preserve the aspect ratio, and then enables it, making it visible.        |
        //¬|Else would trigger if there were fewer than three - V UNLIKELY                            |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|   
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
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Function to see if the picked up item was one of the requests, and if so, "ticks" it off. |
        //¬|collectedName arg comes from the sampleName string of the data file attached to that      |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|   
        for (int i = 0; i < chosenCollectibles.Count; i++)
        {
            //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
            //¬|Runs a for loop  - one iteration per item in list.            |
            //¬|Make a variable for our iteration value. This ISN'T NEEDED but does make for more readable|
            //¬|syntax.                                                                                   | 
            //¬|If the target from the list has the same name as the pickup that triggered the check      |
            //¬|(PickUpController.cs) and the list of already collected samples doesn't contain that name |
            //¬|then add that name to the list.                                                           |
            //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|    
            var target = chosenCollectibles[i];
            if (target.sampleName == collectedName && !collectedSampleNames.Contains(collectedName))
            {
                collectedSampleNames.Add(collectedName);
                Debug.Log("Collected target: " + collectedName);

                //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
                //¬|All "chosen collectible" slots in the UI have a child object called "tick overlay" which  |
                //¬|holds the "tick" image. It's deactivated by default.                                      |
                //¬|We are asking the code to find the TickOverlay of that point in the list. We use the      |
                //¬|Transform, as Find() returns a Transform, not a GameObject.                               |
                //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
                Transform tick = uiTargets[i].transform.Find("TickOverlay");
                if (tick != null)
                {
                    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
                    //¬|If tick is not null is just error handling, checking something was found.                 |
                    //¬|We reference the whole GameObject of the tick here, and set it to true.                   |
                    //¬|This makes the tick appear!                                                               |
                    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
                    tick.gameObject.SetActive(true);
                }

                if (collectedSampleNames.Count == 3)
                {
                    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
                    //¬|If the list of collected sample names has three things in (names only get added IF the    |
                    //¬|item was a target) flip the gameIsWon bool to true. Other conditions (SampleSlideUI) rely |
                    //¬|on this to display the right panel at the right time.                                     |
                    //¬|I don't think I need to say GameManager.instance - this is GameManager. But I'm not sure. |
                    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
                    GameManager.instance.gameIsWon = true;
                    Debug.Log("All targets collected!");
                }
                return;
            }
        }
    }

    public void PlayGameAgain()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Reloads the scene. Probably not the best way to do this! But there's nothing to carry     |
        //¬|over from the previous game, so it's not wrong?                                           |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
