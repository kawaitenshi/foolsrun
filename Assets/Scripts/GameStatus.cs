using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameStatus : MonoBehaviour
{
    // audio related
    public AudioClip short_time_left;
    private bool played_stl = false;
    public GameObject playerObj;

    // score manager
    public ScoreManager scoreManager;
    public int requiredScoreToWin = 6;
    public int requiredScoreToWinTut = 3;
    public bool winStat = false;

    // game status texts and buttons
    public GameObject GamePaused;
    public GameObject YouWin;
    public GameObject YouLose;
    public GameObject LevelCleared;
    public GameObject ResumeButton;
    public GameObject RestartButton;
    public GameObject NextLevelButton;
    public GameObject MainMenuButton;

    // text timer related
    public GameObject timeRemainingObj;
    public GameObject timeRemainingClockContents;
    private Text timeRemainingText;
    private TextMeshProUGUI timeRemainingClockContentsText;

    // new timer UI related
    public float timeLeft = 10;
    private float totalTime;
    private float timeCost = 0;
    public Slider slider; // Slider for time
    public Image fill; // Fill for the slider

    public GameObject statObj;
    public Text statText;

    // game score related
    public GameObject gameScore;
    private TextMeshProUGUI gameScoreText;
    
    // instructions for the game
    private Image beginningInstructionsImage;
    public GameObject beginningInstructionsMessage;
    private const string CollectMoreGemsMessage = "Collect more gems and come back!";

    // Start is called before the first frame update
    void Start() {
        // set up objects
        timeRemainingText = timeRemainingObj.GetComponent<Text>();
        timeRemainingClockContentsText = timeRemainingClockContents.GetComponent<TextMeshProUGUI>();
        beginningInstructionsImage = beginningInstructionsMessage.GetComponent<Image>();
        timeRemainingText.text = "Time Remaining: " + timeLeft;
        statText = statObj.GetComponent<Text>();
        
        // setup timer
        timeRemainingClockContentsText.text = formatTime(timeLeft);
        slider.value = 1f;
        totalTime = timeLeft;

        // lock and hide cursor
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // hide all UI elements
        GamePaused.SetActive(false);
        YouWin.SetActive(false);
        YouLose.SetActive(false);
        LevelCleared.SetActive(false);
        ResumeButton.SetActive(false);
        RestartButton.SetActive(false);
        NextLevelButton.SetActive(false);
        MainMenuButton.SetActive(false);
        statObj.SetActive(false);

        // show instruction only in tutorial
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Tutorial") {
            StartCoroutine(FadeImageAfterDelay(beginningInstructionsImage, 3f));
        } else {
            beginningInstructionsImage.gameObject.SetActive(false);
        }
        if (sceneName == "Level 1.1") {GameControl.control.current_score = 0;}
        gameScoreText = gameScore.GetComponent<TextMeshProUGUI>();
        // setup game score
        gameScoreText.text = formatScore(GameControl.control.current_score);
    }

    // Update is called once per frame
    void Update() {
        // Check remaining time of this round
        /*
        if (timeLeft > 0) {
            if (timeLeft < 11 && !played_stl) {
                GetComponent<AudioSource>().clip = short_time_left;
                GetComponent<AudioSource>().Play();
                played_stl = true;
                fill.color = Color.red;
            }
            timeLeft -= Time.deltaTime;
        } else {
            PauseGame("lose");
        }
        */

        timeCost += Time.deltaTime;
        DisplayTime(timeCost, "count up");

        DisplayScore();

        // Check if player has reached end of the maze
        for (int i=0; i<playerObj.transform.childCount; i++) {
            GameObject childObj = playerObj.transform.GetChild(i).gameObject;
            if (PlayerCollision.hitFinishLine) { // score being checked already in PlayerCollision
                winStat = true;
            }
        }

        // if win, pause game        
        if (winStat == true) {
            PauseGame("win");
        }

        // Detect input about pause game manually
        if (Input.GetKeyDown(KeyCode.P)) {
            PauseGame("pause");
        }
    }

    /**
     * Accepts the time left in seconds and returns a formatted time string.
     */
    string formatTime(float secondsLeft) {
        if (secondsLeft <= 0) {
            return "00:00";
        }
        return $"{padInt((int)(secondsLeft / 60))}:{padInt((int)(secondsLeft % 60))}";
    }

    public string padInt(int time) {
        if (time < 10) {
            return $"0{time}";
        }

        return $"{time}";
    }
    void DisplayTime(float time, string mode) {
        if (mode == "count down") {
            slider.value = time / totalTime;
            timeRemainingClockContentsText.text = formatTime(time);
        } else if (mode == "count up") {
            timeRemainingClockContentsText.text = formatTime(time);
        }
    }

    public void addTime(float time) {
        if (timeLeft + time <= totalTime) {
            timeLeft += time;
        } else {
            timeLeft = totalTime;
        }
    }

    public void deduceTime(float time) {
        if (timeCost - time >= 0f) {
            timeCost -= time;
        } else {
            timeCost = 0f;
        }
    }

    string formatScore(int current_score) {
        return current_score.ToString();
    }

    void DisplayScore() {
        gameScoreText.text = formatScore(GameControl.control.current_score);
    }

    public void PauseGame(string type) {
        // unlock and unhide cursor
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (type == "pause") {
            GamePaused.SetActive(true);
            RestartButton.SetActive(true);
            ResumeButton.SetActive(true);

        } else if (type == "lose") {
            YouLose.SetActive(true);
            RestartButton.SetActive(true);
            MainMenuButton.SetActive(true);
        
        } else if (type == "win") {
            statText.text = "Time cost: " + formatTime(timeCost);
            statObj.SetActive(true);

            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            if (sceneName == "Level 1.1") {
                GameControl.control.after_first_lvl = GameControl.control.current_score;
            }
            else if (sceneName == "Level 1.2") {
                GameControl.control.after_second_lvl = GameControl.control.current_score;
            }
            else if (sceneName == "Level 1.3") {
                GameControl.control.after_third_lvl = GameControl.control.current_score;
            }
            
            if (sceneName != "Level 1.3") {
                LevelCleared.SetActive(true);
                RestartButton.SetActive(true);
                NextLevelButton.SetActive(true);
            
            } else {
                YouWin.SetActive(true);
                RestartButton.SetActive(true);
                MainMenuButton.SetActive(true);
            }
        }
    }

    public void ResumeGame() {
        // lock and hide cursor
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // hide all UI elements
        GamePaused.SetActive(false);
        YouWin.SetActive(false);
        YouLose.SetActive(false);
        LevelCleared.SetActive(false);
        ResumeButton.SetActive(false);
        RestartButton.SetActive(false);
        NextLevelButton.SetActive(false);
        MainMenuButton.SetActive(false);
        statObj.SetActive(false);
    }

    public void RestartGame() {
        Time.timeScale = 1.0f;
        winStat = false;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Level 1.1")
        {GameControl.control.current_score = 0;}
        else if (sceneName == "Level 1.2")
        {GameControl.control.current_score = GameControl.control.after_first_lvl;}
        else if (sceneName == "Level 1.3")
        {GameControl.control.current_score = GameControl.control.after_second_lvl;}

        // lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // hide all UI elements
        GamePaused.SetActive(false);
        YouWin.SetActive(false);
        YouLose.SetActive(false);
        LevelCleared.SetActive(false);
        ResumeButton.SetActive(false);
        RestartButton.SetActive(false);
        NextLevelButton.SetActive(false);
        MainMenuButton.SetActive(false);
        statObj.SetActive(false);

        // reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DisplayMessage(Text textArea, string message) {
        // TODO: refactor this function to just check membership of an Enum of all the messages.
        if (message == "Game Over!") {
            textArea.text = "Game Over!";
        } else if (message == "Game Paused") {
            textArea.text = "Game Paused";
        } else if (message == "Resume") {
            textArea.text = "Resume";
        } else if (message == "Level Cleared!") {
            textArea.text = "Level Cleared!";
        } else if (message == "Next Level") {
            textArea.text = "Next Level";
        } else if (message == "You Win!") {
            textArea.text = "You Win!";
        } else if (message == "End") {
            textArea.text = "End";
        } else if (message == "Restart") {
            textArea.text = "Restart";
        } else if (message == CollectMoreGemsMessage) {
            textArea.text = CollectMoreGemsMessage;
        } else {
            textArea.text = "ERROR: Unknown input!";
        }
    }

    public void DisplayStatus(string status) {
        // gameStatText.text = status;
        // StartCoroutine(ClearMessageAfterDelay(gameStatText, 1));
    }

    public void ClearMessage(Text textArea) {
        textArea.text = "";
    }

    IEnumerator ClearMessageAfterDelay(Text textArea, float delaySeconds) {
        yield return new WaitForSeconds(delaySeconds);
        ClearMessage(textArea);
    }

    IEnumerable SetInactiveAfterDelay(GameObject obj, bool active, bool fade, float delaySeconds) {
        yield return new WaitForSeconds(delaySeconds);
        obj.SetActive(active);
    }

    IEnumerator FadeImageAfterDelay(Image img, float delaySeconds) {
        yield return new WaitForSeconds(delaySeconds);

        // fade from opaque to transparent
        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime) {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }

        img.gameObject.SetActive(false);
    }
}
