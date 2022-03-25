using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class TextButtonLvl3 : MonoBehaviour, IPointerClickHandler
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
             SceneManager.LoadScene("MainMenu");
             //AsyncOperation unload = SceneManager.UnloadSceneAsync("MainScene");
        }
    }
}