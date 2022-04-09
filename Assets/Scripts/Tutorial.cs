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
        //Debug.Log(noteLine);

    }

    public void NextIns() {

    }
}
