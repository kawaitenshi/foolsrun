using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerCollision : MonoBehaviour
{
    public GameObject character1;
    public GameObject character2;
    public GameObject character3;
    public static bool hitFinishLine;
    public static bool inHallway;
    public ScoreManager scoreManager;
    public GameStatus gameStatus;

    public GameObject camera;
    public MovePB moveHuman;
    public MoveChicken moveChicken;
    public MoveDragon moveDragon;

    public AudioClip gem_collect;
    public AudioClip chicken_squawk;
    public AudioClip potion_hit;

    void Start() {
        hitFinishLine = false;
        inHallway = false;
    }

    void Update() {
        int layerMask = 1 << 6;
        RaycastHit hitInfo;
        bool hitHallway = Physics.Raycast(transform.position, Vector3.down, out hitInfo, 100f, layerMask);

        if (hitHallway) {
            inHallway = true;
        } else {
            inHallway = false;
        }

        /*
        Color rayColor;
        if (hitHallway) {
            rayColor = Color.green;
        } else {
            rayColor = Color.red;
        }
        Debug.DrawRay(transform.position, Vector3.down * 100f, rayColor);
        */
    }

    void OnCollisionEnter(Collision collision) {
        print($"collision occurred with {collision.collider.name}");

        if (collision.collider.CompareTag("Gem")) {
            GetComponent<AudioSource>().clip = gem_collect;
            GetComponent<AudioSource>().Play();

        } else if (collision.collider.CompareTag("ChickenPotion")) {
            collision.collider.gameObject.GetComponent<potionCollision>().Explode();
            GetComponent<AudioSource>().clip = potion_hit;
            GetComponent<AudioSource>().Play();

            if (character2.activeSelf) {
                print("Changing to character 1");
                character1.transform.position = character2.transform.position;
                character1.transform.rotation = character2.transform.rotation;
                character2.SetActive(false);
                character1.SetActive(true);
                camera.GetComponent<CameraController>().PlayerTransform = character1.transform.Find("Focus");

            } else if (character3.activeSelf) {
                print("Changing to character 1");
                character1.transform.position = character3.transform.position;
                character1.transform.rotation = character3.transform.rotation;
                character3.SetActive(false);
                character1.SetActive(true);
                camera.GetComponent<CameraController>().PlayerTransform = character1.transform.Find("Focus");
            }

        } else if (collision.collider.CompareTag("HumanPotion")) {
            collision.collider.gameObject.GetComponent<potionCollision>().Explode();
            GetComponent<AudioSource>().clip = potion_hit;
            GetComponent<AudioSource>().Play();

            if (character1.activeSelf) {
                print("Changing to character 2");
                GetComponent<AudioSource>().clip = chicken_squawk;
                GetComponent<AudioSource>().Play();
                character2.transform.position = character1.transform.position;
                character2.transform.rotation = character1.transform.rotation;
                character1.SetActive(false);
                character2.SetActive(true);
                camera.GetComponent<CameraController>().PlayerTransform = character2.transform.Find("Focus");

            } else if (character3.activeSelf) {
                print("Changing to character 2");
                character2.transform.position = character3.transform.position;
                character2.transform.rotation = character3.transform.rotation;
                character3.SetActive(false);
                character2.SetActive(true);
                camera.GetComponent<CameraController>().PlayerTransform = character2.transform.Find("Focus");
            }

        } else if (collision.collider.CompareTag("DragonPotion")) {
            collision.collider.gameObject.GetComponent<potionCollision>().Explode();
            GetComponent<AudioSource>().clip = potion_hit;
            GetComponent<AudioSource>().Play();

            if (character2.activeSelf) {
                print("Changing to character 3");
                character3.transform.position = character2.transform.position;
                character3.transform.rotation = character2.transform.rotation;
                character2.SetActive(false);
                character3.SetActive(true);
                camera.GetComponent<CameraController>().PlayerTransform = character3.transform.Find("Focus");

            } else if (character1.activeSelf) {
                print("Changing to character 3");
                character3.transform.position = character1.transform.position;
                character3.transform.rotation = character1.transform.rotation;
                character1.SetActive(false);
                character3.SetActive(true);
                camera.GetComponent<CameraController>().PlayerTransform = character3.transform.Find("Focus");
            }
        }
        else if (collision.collider.CompareTag("AddTenSec")) {
            if (gameObject != null)
                {Destroy(collision.collider.gameObject);}
            GetComponent<AudioSource>().clip = gem_collect;
            GetComponent<AudioSource>().Play();

            GameObject gameUi = GameObject.Find("Game UI");
            // UnityEngine.Component gameStatus  = gameUi.GetComponent("Game Status");
            gameUi.BroadcastMessage("addTime", 10);
            gameUi.BroadcastMessage("DisplayStatus", "+10 seconds");
        }

        else if (collision.collider.CompareTag("AddFiveSec")) {
            if (gameObject != null)
                {Destroy(collision.collider.gameObject);}
            GetComponent<AudioSource>().clip = gem_collect;
            GetComponent<AudioSource>().Play();

            GameObject gameUi = GameObject.Find("Game UI");
            // UnityEngine.Component gameStatus  = gameUi.GetComponent("Game Status");
            gameUi.BroadcastMessage("addTime", 5);
            gameUi.BroadcastMessage("DisplayStatus", "+5 seconds");
        }

        if (collision.collider.CompareTag("FinishLine")) {
            if (scoreManager.GetScore() >= gameStatus.requiredScoreToWin)
            {hitFinishLine = true;}
        }
    }
}
