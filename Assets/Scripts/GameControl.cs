using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl control;

    // gems per level
    public int first_lvl_gems;
    public int second_lvl_gems;
    public int third_lvl_gems;

    // game time by level
    public float first_lvl_time;
    public float second_lvl_time;
    public float third_lvl_time;

    // game score by level
    public int first_lvl_score;
    public int second_lvl_score;
    public int third_lvl_score;

    public int current_score;
    // Start is called before the first frame update
    void Awake()
    {
        if (control == null) {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
//    void Update()
//    {
//        DisplayScore();
//    }
}


