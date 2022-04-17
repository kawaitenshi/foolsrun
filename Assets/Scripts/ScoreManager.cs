using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0;

    public UpdateScoreInfo scInfo;
    // Start is called before the first frame update
    void Start() {
        scInfo = GameObject.FindObjectOfType<UpdateScoreInfo>();
    }

    public void UpdateScore() {
        playerScore += 1;
        if (playerScore <= 6) {
            scInfo.UpdateScoreImage(playerScore - 1);
        }
        GameControl.control.current_score += 1000;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Level 1.1") {
            GameControl.control.first_lvl_gems += 1;
            }
        else if (sceneName == "Level 1.2") {
            GameControl.control.second_lvl_gems += 1;
        }
        else if (sceneName == "Level 1.3") {
            GameControl.control.third_lvl_gems += 1;
        }
        //scInfo.UpdateScoreText(playerScore);
    }

    public int GetScore() {
        return playerScore;
    }
}
