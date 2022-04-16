using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0;

    public UpdateScoreInfo scInfo;
    public GameStatus gameStat;
    // Start is called before the first frame update
    void Start() {
        scInfo = GameObject.FindObjectOfType<UpdateScoreInfo>();
        gameStat = GameObject.FindObjectOfType<GameStatus>();
    }

    public void UpdateScore() {
        playerScore += 1;
        if (playerScore <= 6) {
            scInfo.UpdateScoreImage(playerScore - 1);
        }
        gameStat.updateScoreGem();
        //scInfo.UpdateScoreText(playerScore);
    }

    public int GetScore() {
        return playerScore;
    }
}
