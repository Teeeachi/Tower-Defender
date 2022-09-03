using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public RectTransform pausePanel;
    public Button cameraButton;
    public RawImage fullCameraImage;

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
}
