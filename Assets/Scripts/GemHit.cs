using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemHit : MonoBehaviour
{
    public ScoreManager ScoreMan;

    private void Start()
    {
        ScoreMan = FindObjectOfType<ScoreManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
          print("Player gem collision happened");
          if (gameObject != null)
            {Destroy(this.gameObject);}
            ScoreMan.UpdateScore();
        }
    }
}
