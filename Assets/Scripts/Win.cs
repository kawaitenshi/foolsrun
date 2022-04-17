using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public GameObject NextLevelButton;
    public GameObject timeObj;
    public Text timeText;
    public GameObject scoreObj;
    public Text scoreText;
    public GameObject level1;
    public Text level1Text;
    public GameObject level2;
    public Text level2Text;
    public GameObject level3;
    public Text level3Text;
    public GameObject gems;
    public Text gemsText;

    // Start is called before the first frame update
    void Start() {
        // set up objects
        timeText = timeObj.GetComponent<Text>();
        scoreText = scoreObj.GetComponent<Text>();
        level1Text = level1.GetComponent<Text>();
        level2Text = level2.GetComponent<Text>();
        level3Text = level3.GetComponent<Text>();
        gemsText = gems.GetComponent<Text>();

        // UI elements
//        NextLevelButton.SetActive(true);

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        timeText.text = "Total Time: " + formatTime(GameControl.control.first_lvl_time + GameControl.control.second_lvl_time + GameControl.control.third_lvl_time);
        scoreText.text = "Total Score: " + formatScore(GameControl.control.current_score);
        level1Text.text = "Level 1: " + formatScore(GameControl.control.first_lvl_score);
        level2Text.text = "Level 2: " + formatScore(GameControl.control.second_lvl_score);
        level3Text.text = "Level 3: " + formatScore(GameControl.control.third_lvl_score);
        gemsText.text = "Total Gems: " + formatScore(GameControl.control.first_lvl_gems + GameControl.control.second_lvl_gems + GameControl.control.third_lvl_gems);

//        timeObj.SetActive(true);
//        scoreObj.SetActive(true);
    }

    /**
     * Accepts the time in seconds and returns a formatted time string.
     */
    string formatTime(float secondsTaken) {
        if (secondsTaken <= 0) {
            return "00:00";
        }
        return $"{padInt((int)(secondsTaken / 60))}:{padInt((int)(secondsTaken % 60))}";
    }

    public string padInt(int time) {
        if (time < 10) {
            return $"0{time}";
        }

        return $"{time}";
    }

    string formatScore(int score) {
        return score.ToString();
    }
}
