using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    private string _firstLevelName = "MainScene";
    public GameObject startButton;
    public GameObject LoadingText;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Disable this if adding other buttons to the UI.
        if (Input.anyKeyDown.Equals(true))
        {
            NewGame();
        }
        
    }

    public void NewGame()
    {
        // AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(_firstLevelName);
        LoadingText.SetActive(true);
        startButton.SetActive(false);
        SceneManager.LoadScene(_firstLevelName);
        AsyncOperation unload = SceneManager.UnloadSceneAsync("MainMenu");
    }
}
