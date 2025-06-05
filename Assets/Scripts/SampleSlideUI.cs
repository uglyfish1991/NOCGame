using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SampleSlideUI : MonoBehaviour
{
    public GameObject panel;
    public Image sampleImage;
    public TMP_Text sampleNameText;
    public TMP_Text descriptionText;
    public Button closeButton;

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

        panel.SetActive(true);
        GameManager.instance.rovIsMoving = false; // Pause movement
    }

    public void HideSlide()
    {
        panel.SetActive(false);
        GameManager.instance.rovIsMoving = true; // Resume movement
    }
}
