using TMPro;
using UnityEngine;
using UnityEngine.UI;
//¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
//¬|This script is the introduction slide manager. This mainly just set up for the inspector  |
//¬|within Unity - there are many references! The script is attached to the IntroManager      |
//¬|Game Object, and the references mainly come from the IntroCanvas panel.
//¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
public class IntroManager : MonoBehaviour
{
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|Inspector set up. Each slide needs an image, a title, the text, and a "next" button.      |
    //¬|[TextArea] just lets me set the size of the textbox for the message field, because        |
    //¬|it was too hard to write in a single line.                                                |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    [System.Serializable]
    public class IntroSlide
    {
        public Sprite image;
        public string title;

        [TextArea(3, 10)]
        public string message;
        public string button;
    }

    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|References to the collection of slides, and a reference to each of the GameObjects that   |
    //¬|reflect the things that will change. (e.g. slideImage is the GO which is the image)       |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    public IntroSlide[] slides;
    public Image slideImage;
    public TMP_Text titleText;
    public TMP_Text bodyText;
    public TMP_Text buttonText;
    public GameObject introPanel;
    private int currentSlide = 0;

    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|Start() runs at start, after Awake. The game should open on the first slide. As above,    |
    //¬| currentSlide starts at index 0                                                           |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    void Start()
    {
        ShowSlide(currentSlide);
    }

    public void NextSlide()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|NextSlide() function is the onClick() event on the button. It just counts up +1 per click |
        //¬| and then calls the ShowSlide function focused on that index position.                    |
        //¬|if checks the value of current slide doesn't go out of range. When it's equal to the      |
        //¬| number of slides, the next click hides the intro panel, and starts the game.             |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        currentSlide++;

        if (currentSlide >= slides.Length)
        {
            // All slides shown — hide panel and start game
            introPanel.SetActive(false);
            GameManager.instance.StartGame();
            return;
        }

        ShowSlide(currentSlide);
    }

    private void ShowSlide(int index)
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Changes the game objects's contents on the IntroCanvas panel to the information written in|
        //¬|the IntroManager Inspector. This is the most scalable way I can think of?                 |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        slideImage.sprite = slides[index].image;
        titleText.text = slides[index].title;
        bodyText.text = slides[index].message;
        buttonText.text = slides[index].button;
    }
}
