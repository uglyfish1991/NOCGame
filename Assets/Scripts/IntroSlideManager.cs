using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [System.Serializable]
    public class IntroSlide
    {
        public Sprite image;
        public string message;
    }

    public IntroSlide[] slides;

    public Image slideImage;
    public TMP_Text slideText; // Or use TMP_Text
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
        slideText.text = slides[index].message;
    }
}
