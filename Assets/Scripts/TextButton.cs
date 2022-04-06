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
    public GameObject statusObj;
    public UnityEvent onClick;
    private GameStatus gameStatus;

    private void Start()
    {
        gameStatus = statusObj.GetComponent<GameStatus>();
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
            SceneManager.LoadScene("Level 2");
        } else if (sceneName == "Level 2") {
            SceneManager.LoadScene("MainMenu");
        } else {
            Debug.Log("Scene loading error");
        }
    }

    /*
    public void ResumeOrRestart() {
        Debug.Log("resume or restart?");

        if (gameStatus.timeLeft > 0 & gameStatus.winStat == false) {
            Debug.Log("Resuming!");
            gameStatus.ResumeGame();
        
        } else if (gameStatus.winStat == false) {
            Debug.Log("Restarting!");
            gameStatus.RestartGame();
        
        } else {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            
            if (sceneName == "Tutorial") {
                SceneManager.LoadScene("Level 1.1");
            } else if (sceneName == "Level 1.1") {
                SceneManager.LoadScene("Level 1.2");
            } else if (sceneName == "Level 1.2") {
                SceneManager.LoadScene("Level 1.3");
            } else if (sceneName == "Level 1.3") {
                SceneManager.LoadScene("Level 2");
            } else if (sceneName == "Level 2") {
                SceneManager.LoadScene("MainMenu");
            } else {
                Debug.Log("Scene loading error");
            }
        }
    }
    */
}
