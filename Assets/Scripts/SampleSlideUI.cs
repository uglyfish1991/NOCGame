using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class SampleSlideUI : MonoBehaviour
{
    public GameObject panel;
    public Image sampleImage;
    public TMP_Text sampleNameText;
    public TMP_Text descriptionText;
    public TMP_Text extraMessageText;
    public Button closeButton;
     public GameObject winnerPanel;

    private void Start()
    {
        panel.SetActive(false);
        closeButton.onClick.AddListener(HideSlide);
    }

    public void ShowSlide(SampleData data)
    {
        sampleImage.sprite = data.sampleImage;
        sampleNameText.text = data.sampleName;
        descriptionText.text = data.description;

        bool isTarget = GameManager.instance.chosenCollectibles.Any(c => c.sampleName == data.sampleName);

    if (isTarget)
    {
        extraMessageText.text = "Well done! This was one of your target items.";
    }
    else
    {
        extraMessageText.text = "This was not one of your targets.";
    }

        panel.SetActive(true);
        GameManager.instance.rovIsMoving = false; // Pause movement
    }

    public void HideSlide()
    {
        panel.SetActive(false);

        if (GameManager.instance.gameIsWon)
        {
            winnerPanel.SetActive(true);
            Debug.Log("Your slide would be called here maybe");
        }
        else
        {
            GameManager.instance.rovIsMoving = true; // Resume movement
        }
        
    }
}
