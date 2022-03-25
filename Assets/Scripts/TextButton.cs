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

    public void ResumeOrRestart()
    {
        Debug.Log("resume or restart?");

        if (gameStatus.timeLeft > 0 & gameStatus.winStat == false)
        {
            Debug.Log("Resuming!");
            gameStatus.ResumeGame();
        }
        else if (gameStatus.winStat == false)
        {
            Debug.Log("Restarting!");
            gameStatus.RestartGame();
        }
        else {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "MainScene")
             {SceneManager.LoadScene("Level2");}
        else if (sceneName == "Level2")
            {SceneManager.LoadScene("Level3");}
        else if (sceneName == "Level3")
            {SceneManager.LoadScene("MainMenu");}
        else {Debug.Log("Scene loading error");}
             //AsyncOperation unload = SceneManager.UnloadSceneAsync("MainScene");
        }
    }
}
