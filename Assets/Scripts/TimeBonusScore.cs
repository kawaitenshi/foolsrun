using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeBonusScore : MonoBehaviour
{
    public GameObject NextLevelButton;

//    public GameObject timeBonusObj;
//    public Text timeBonusText;
    public GameObject scoreObj;
    public Text scoreText;

    // The images to use for the stars
    public Texture fullStarImage;

    // Number of stars player received
    private int numStars = 0;

    // Hold the stars
    public GameObject [] stars;

    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 1.0f;
        // set up objects
        //timeBonusText = timeBonusObj.GetComponent<Text>();
        scoreText = scoreObj.GetComponent<Text>();

        // UI elements
//        NextLevelButton.SetActive(true);

        int fls = GameControl.control.first_lvl_score;
        int sls = GameControl.control.second_lvl_score;
        int tls = GameControl.control.third_lvl_score;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Level 1.1 cleared") {
            if (fls > 12667) {numStars = 3;}
            else if (fls > 9333) {numStars = 2;}
            else if (fls > 6000) {numStars = 1;}
            else {numStars = 0;}}

        if (sceneName == "Level 1.2 cleared") {
            if (sls > 12667) {numStars = 3;}
            else if (sls > 9333) {numStars = 2;}
            else if (sls > 6000) {numStars = 1;}
            else numStars = 0;}

        if (sceneName == "Level 1.3 cleared") {
            if (tls > 12667) {numStars = 3;}
            else if (tls > 9333) {numStars = 2;}
            else if (tls > 6000) {numStars = 1;}
            else numStars = 0;}

        for (int star = 0; star < numStars; star++)
            stars[star].GetComponent<RawImage>().texture = fullStarImage;

            scoreText.text = "Score: " + formatScore(GameControl.control.current_score);


//        timeBonusObj.SetActive(true);
//        scoreObj.SetActive(true);
    }

    string formatScore(int score) {
        return score.ToString();
    }
}
