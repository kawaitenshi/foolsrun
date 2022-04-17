using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeBonusScore : MonoBehaviour
{
    public GameObject NextLevelButton;

    public GameObject timeBonusObj;
    public Text timeBonusText;
    public GameObject scoreObj;
    public Text scoreText;
    private int maxTimeBonus = 10000;
    private int timeBonus;

    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 1.0f;
        // set up objects
        timeBonusText = timeBonusObj.GetComponent<Text>();
        scoreText = scoreObj.GetComponent<Text>();

        // UI elements
//        NextLevelButton.SetActive(true);

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Level 1.1 cleared") {
            timeBonus = maxTimeBonus - (int)(GameControl.control.first_lvl_time/10 * 500);
            timeBonusText.text = "Time Bonus: " + formatScore(timeBonus);
            GameControl.control.current_score += timeBonus;
            GameControl.control.first_lvl_score = GameControl.control.first_lvl_gems * 1000 + timeBonus;
            scoreText.text = "Score: " + formatScore(GameControl.control.current_score);
        }
        else if (sceneName == "Level 1.2 cleared") {
            timeBonus = maxTimeBonus - (int)(GameControl.control.second_lvl_time/10 * 500);
            timeBonusText.text = "Time Bonus: " + formatScore(timeBonus);
            GameControl.control.current_score += timeBonus;
            GameControl.control.second_lvl_score = GameControl.control.second_lvl_gems * 1000 + timeBonus;
            scoreText.text = "Score: " + formatScore(GameControl.control.current_score);
        }
        else if (sceneName == "Level 1.3 cleared") {
            timeBonus = maxTimeBonus - (int)(GameControl.control.third_lvl_time/10 * 500);
            timeBonusText.text = "Time Bonus: " + formatScore(timeBonus);
            GameControl.control.current_score += timeBonus;
            GameControl.control.third_lvl_score = GameControl.control.third_lvl_gems * 1000 + timeBonus;
            scoreText.text = "Score: " + formatScore(GameControl.control.current_score);
        }

//        timeBonusObj.SetActive(true);
//        scoreObj.SetActive(true);
    }

    string formatScore(int score) {
        return score.ToString();
    }
}

