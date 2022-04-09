using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject NextButton;
    public GameObject Note1;
    public GameObject Note2;
    public GameObject Note3;
    public GameObject Note4;
    public GameObject Note5;
    public GameObject Note6;
    public GameObject Note7;

    // Start is called before the first frame update
    void Start() {
        NextButton.SetActive(false);
        Note1.SetActive(false);
        Note2.SetActive(false);
        Note3.SetActive(false);
        Note4.SetActive(false);
        Note5.SetActive(false);
        Note6.SetActive(false);
        Note7.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void PopIns(string noteLine) {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        NextButton.SetActive(true);

        if (noteLine == "noteLine1") {
            Note1.SetActive(true);
        } else if (noteLine == "noteLine2") {
            Note3.SetActive(true);
        } else if (noteLine == "noteLine3") {
            Note5.SetActive(true);
        } else if (noteLine == "noteLine4") {
            Note6.SetActive(true);
        }
    }

    public void NextIns() {

        if (Note1.activeSelf) {
            Note1.SetActive(false);
            Note2.SetActive(true);
        
        } else if (Note2.activeSelf) {
            Note2.SetActive(false);
            NextButton.SetActive(false);

            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        
        } else if (Note3.activeSelf) {
            Note3.SetActive(false);
            Note4.SetActive(true);

        
        } else if (Note4.activeSelf) {
            Note4.SetActive(false);
            NextButton.SetActive(false);

            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        
        } else if (Note5.activeSelf) {
            Note5.SetActive(false);
            NextButton.SetActive(false);
        
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        
        } else if (Note6.activeSelf) {
            Note6.SetActive(false);
            Note7.SetActive(true);

        } else if (Note7.activeSelf) {
            Note7.SetActive(false);
            NextButton.SetActive(false);
        
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
