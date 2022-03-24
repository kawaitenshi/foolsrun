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
    private bool _sceneChanged = false;

    private Scene _firstLevel;
    // Start is called before the first frame update
    void Start()
    {
        _firstLevel = SceneManager.GetSceneByName(_firstLevelName);
    }

    // Update is called once per frame
    void Update()
    {
        // Disable this if adding other buttons to the UI.
        if (Input.anyKeyDown.Equals(true))
        {
            NewGame();
        }

        if (_sceneChanged && _firstLevel.isLoaded)
        {
            SceneManager.SetActiveScene(_firstLevel);
            AsyncOperation unload = SceneManager.UnloadSceneAsync("MainMenu");
            _sceneChanged = false;
        }
        
    }

    public void NewGame()
    {
        // AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(_firstLevelName);
        LoadingText.SetActive(true);
        startButton.SetActive(false);
        SceneManager.LoadScene(_firstLevelName);
        _sceneChanged = true;
    }
}
