using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public RectTransform pausePanel;
    public Button cameraButton;
    public RawImage fullCameraImage;
    public TextMeshProUGUI LoseWinText;
    public Canvas canvas;
    public Image blackScreen;

    // Start is called before the first frame update
    void Start()
    {
        fullCameraImage.gameObject.SetActive(false);
        cameraButton.gameObject.SetActive(true);
        pausePanel.gameObject.SetActive(false);
    }

    public void onCameraButtonClick()
    {
        fullCameraImage.gameObject.SetActive(!fullCameraImage.gameObject.activeSelf);
    }

    public void PlayPressed()
    {
        Time.timeScale = 1f;
        pausePanel.gameObject.SetActive(!pausePanel.gameObject.activeSelf);
    }

    public void PausePressed()
    {
        Time.timeScale = 0f;
        pausePanel.gameObject.SetActive(!pausePanel.gameObject.activeSelf);
    }

    public void youWon()
    {
        LoseWinText.text = "You Won!";
        Color col = new Color(0, 1, 0);
        LoseWinText.color = col;
        LoseWinText.gameObject.SetActive(true);
        canvas.gameObject.SetActive(false);
        blackScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void youLost()
    {
        LoseWinText.text = "You Lost!";
        Color col = new Color(1, 0, 0);
        LoseWinText.color = col;
        LoseWinText.gameObject.SetActive(true);
        canvas.gameObject.SetActive(false);
        blackScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
