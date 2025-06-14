using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

//¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
//¬|This script is for controlling the slides which pop up with sample info on. It also       |
//¬|controls the game ending, which MIGHT have been better in another script.                 |
//¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|

public class SampleSlideUI : MonoBehaviour
{

    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|References to the objects in the sample panel which will be changed dynamically           |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|

    public GameObject panel;
    public Image sampleImage;
    public TMP_Text sampleNameText;
    public TMP_Text descriptionText;
    public TMP_Text extraMessageText;
    public Button closeButton;
    public GameObject winnerPanel;

    private void Start()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Adds the HideSlide function to the button's onClick()                                     |
        //¬|Easier to do it this way than in the inspector, but this convention is inconsistent as    |
        //¬|used the inspector for other slides.                                                      |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        panel.SetActive(false);
        closeButton.onClick.AddListener(HideSlide);
    }

    public void ShowSlide(SampleData data)
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|ShowSlide is called by the PickUpController. The data of the item we collide with is      |
        //¬|passed into the function to show the correct info on the slide.                           |
        //¬|The final sentence is controlled by if the item picked up was a target or not.            |
        //¬|The lambda is "for each c in the list, check if c.sampleName equals data.sampleName.      |
        //¬|Any returns true if any item in the list matches the condition.                           |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        sampleImage.sprite = data.sampleImage;
        sampleNameText.text = data.sampleName;
        descriptionText.text = data.description;

        bool isTarget = GameManager.instance.chosenCollectibles.Any(c => c.sampleName == data.sampleName);

        if (isTarget)
        {
            extraMessageText.text = "Well done! This was one of your target items!";
        }
        else
        {
            extraMessageText.text = "This was not one of your targets. Let's put it back safely!";
        }
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Once this has all been checked, show the slide with the right info, and flip the ROV bool |
        //¬|to false so everything stops.                                                             |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        panel.SetActive(true);
        GameManager.instance.rovIsMoving = false;
    }

    public void HideSlide()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|On the button click, it hides the sample panel, and flips the ROV bool to true so other   |
        //¬|scripts start moving again.                                                               |
        //¬|If you've collected all the targets, though, the winner panel comes up instead            |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        panel.SetActive(false);

        if (GameManager.instance.gameIsWon)
        {
            winnerPanel.SetActive(true);
            GameManager.instance.chosenItemsCanvas.SetActive(false);
        }
        else
        {
            GameManager.instance.rovIsMoving = true;
        }

    }
}
