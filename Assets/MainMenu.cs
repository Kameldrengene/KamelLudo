using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button playNextButton;
    private void Start()
    {
        playButton.interactable = false;
        playNextButton.interactable = false;
    }

    private void Update()
    {
        if (Singleton.Instance.Connected)
        {
            playButton.interactable = true;
            playNextButton.interactable = true;
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUit");
    }
}
