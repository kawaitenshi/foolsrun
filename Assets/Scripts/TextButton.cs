using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class TextButton : MonoBehaviour, IPointerClickHandler
{
    // add callbacks in the inspector like for buttons
    public UnityEvent onClick;
    public GameObject statusObj;
    private GameStatus gameStatus;
    public GameObject instructions;
    private Tutorial tutorial;

    private void Start()
    {
        gameStatus = statusObj.GetComponent<GameStatus>();
        
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Tutorial") {
            tutorial = instructions.GetComponent<Tutorial>();
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(name + " Game Object Clicked!", this);
        // invoke your event
        onClick.Invoke();
    }

    public void Resume() {
        gameStatus.ResumeGame();
    }

    public void Restart() {
        gameStatus.RestartGame();
    }

    public void NextLevel() {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        if (sceneName == "Tutorial") {
            SceneManager.LoadScene("Level 1.1");
        } else if (sceneName == "Level 1.1") {
            SceneManager.LoadScene("Level 1.2");
        } else if (sceneName == "Level 1.2") {
            SceneManager.LoadScene("Level 1.3");
        } else if (sceneName == "Level 1.3") {
            SceneManager.LoadScene("MainMenu");
//        } else if (sceneName == "Level 2") {
//            SceneManager.LoadScene("MainMenu");
        } else {
            Debug.Log("Scene loading error");
        }
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextIns() {
        tutorial.NextIns();
    }
}
