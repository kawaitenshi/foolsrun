using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameStatus : MonoBehaviour
{
    public AudioClip short_time_left;
    private bool played_stl = false;
    public GameObject playerObj;
    public ScoreManager scoreManager;

    // game status texts and buttons
    public GameObject GamePaused;
    public GameObject YouWin;
    public GameObject YouLose;
    public GameObject ResumeButton;
    public GameObject RestartButton;
    public GameObject NextLevelButton;

    // text timer related
    public GameObject timeRemainingObj;
    public GameObject timeRemainingClockContents;
    private Text timeRemainingText;
    private TextMeshProUGUI timeRemainingClockContentsText;

    // new timer UI related
    public float timeLeft = 90;
    public int requiredScoreToWin = 6;
    private float totalTime;
    public Slider slider; // Slider for time
    public Image fill; // Fill for the slider
    public bool winStat = false;
    private Rigidbody _rigidbody;
    
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
        ResumeButton.SetActive(false);
        RestartButton.SetActive(false);
        NextLevelButton.SetActive(false);

        // get infomation of the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Tutorial") {
            StartCoroutine(FadeImageAfterDelay(beginningInstructionsImage, 3f));
        } else {
            // StartCoroutine(FadeImageAfterDelay(beginningInstructionsImage, 0f));
            beginningInstructionsImage.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        // Check remaining time of this round
        if (timeLeft > 0) {
            if (timeLeft < 11 && !played_stl) {GetComponent<AudioSource>().clip = short_time_left;
                   GetComponent<AudioSource>().Play();
                   played_stl = true;
                   fill.color = Color.red;}
            timeLeft -= Time.deltaTime;
        } else {
            PauseGame("lose");
        }
        DisplayTime(timeLeft);

        // Check if player has reached end of the maze
        for (int i=0; i<playerObj.transform.childCount; i++) {
            GameObject childObj = playerObj.transform.GetChild(i).gameObject;
            if (PlayerCollision.hitFinishLine && scoreManager.GetScore() >= requiredScoreToWin) {
                winStat = true;
            }
            // else if (PlayerCollision.hitFinishLine)
            // {
            //     DisplayMessage(gameStatText, CollectMoreGemsMessage);
            //     StartCoroutine(ClearMessageAfterDelay(gameStatText, 2));
            //
            // }
        }
        
        if (winStat == true) {
            PauseGame("win");
        }

        // Detect input about pause and resume
        if (Input.GetKeyDown(KeyCode.P)) {
            PauseGame("pause");
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            if (timeLeft > 0 & winStat == false) {
                ResumeGame();
            } else {
                RestartGame();
            }
        }
    }

    /**
     * Accepts the time left in seconds and returns a formatted time string.
     */
    string formatTime(float secondsLeft) {
        if (secondsLeft <= 0) {
            return "0:00";
        }
        return $"{(int) (secondsLeft / 60)}:{padInt((int)(secondsLeft % 60))}";
    }

    public string padInt(int time) {
        if (time < 10) {
            return $"0{time}";
        }

        return $"{time}";
    }
    void DisplayTime(float time) {
        slider.value = timeLeft / totalTime;
        timeRemainingClockContentsText.text = formatTime(timeLeft);
    }

    public void addTime(float time) {
        timeLeft += time;
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
        
        } else if (type == "win") {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            
            if (sceneName != "Level 2") {
                YouWin.SetActive(true);
                RestartButton.SetActive(true);
                NextLevelButton.SetActive(true);
            
            } else {
                YouWin.SetActive(true);
                RestartButton.SetActive(true);
            }
        }
    }

    public void ResumeGame() {
        Time.timeScale = 1.0f;

        // lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // hide all UI elements
        GamePaused.SetActive(false);
        YouWin.SetActive(false);
        YouLose.SetActive(false);
        ResumeButton.SetActive(false);
        RestartButton.SetActive(false);
        NextLevelButton.SetActive(false);
    }

    public void RestartGame() {
        Time.timeScale = 1.0f;
        winStat = false;

        // lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // hide all UI elements
        GamePaused.SetActive(false);
        YouWin.SetActive(false);
        YouLose.SetActive(false);
        ResumeButton.SetActive(false);
        RestartButton.SetActive(false);
        NextLevelButton.SetActive(false);

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
