using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [System.Serializable]
    public class IntroSlide
    {
        public Sprite image;
        public string title;

        [TextArea(3, 10)]
        public string message;
        public string button;
    }

    public IntroSlide[] slides;

    public Image slideImage;

    public TMP_Text titleText;
    public TMP_Text bodyText;
    public TMP_Text buttonText; 
    public GameObject introPanel;

    private int currentSlide = 0;

    void Start()
    {
        ShowSlide(currentSlide);
    }

    public void NextSlide()
    {
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
        slideImage.sprite = slides[index].image;
        titleText.text = slides[index].title;
        bodyText.text = slides[index].message;
        buttonText.text = slides[index].button;
    }
}
