using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPointerClickHandler
{    
    private string _firstLevelName = "Level 1.1";
    private string _tutorialLevelName = "Tutorial";
    public GameObject startButton;
    public GameObject tutorialButton;
    public GameObject LoadingText;
    public UnityEvent onClick;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnPointerClick(PointerEventData pointerEventData) {
        onClick.Invoke();
    }

    public void NewGame() {
        startButton.SetActive(false);
        tutorialButton.SetActive(false);
        LoadingText.SetActive(true);
        SceneManager.LoadScene(_firstLevelName);
    }

    public void Tutorial() {
        startButton.SetActive(false);
        tutorialButton.SetActive(false);
        LoadingText.SetActive(true);
        SceneManager.LoadScene(_tutorialLevelName);
    }
}
