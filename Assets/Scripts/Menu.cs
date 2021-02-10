using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject gameSettings;
    public GameObject playSettings;
    public GameObject hanoiPlatform;
    public GameObject gameUI;
    public GameObject returnButton;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        ReturnButtonIsActive();
    }


    public void QuitApp()
    {
        Application.Quit();
    }

    public void OpenPlaySettings()
    {
        playSettings.SetActive(true);
        gameObject.SetActive(false);

    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void Resume()
    {
        gameUI.SetActive(true);
        gameObject.SetActive(false);
    }
    private void ReturnButtonIsActive()
    {
        if (hanoiPlatform  != null && hanoiPlatform.activeSelf)
        {
            returnButton.SetActive(true);
        }
        else
        {
            returnButton.SetActive(false);
        }
    }
}
