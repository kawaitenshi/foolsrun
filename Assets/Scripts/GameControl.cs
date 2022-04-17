using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl control;

    // game score by level
    public int after_first_lvl;
    public int after_second_lvl;
    public int after_third_lvl;

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


