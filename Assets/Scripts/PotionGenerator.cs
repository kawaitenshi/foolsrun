using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This gets attached to a potion generation platform
public class PotionGenerator : MonoBehaviour
{
    private Vector3 position;

    public float timeToSpawn; // Time before potion spawns again
    public GameObject activePotion; // The potion to generate on this spot

    // Start is called before the first frame update
    void Start()
    {
      position = new Vector3(0f, 10f, 0f);
      position += gameObject.transform.position;
//      var potion = Instantiate(activePotion, position, Quaternion.identity);
//      potion.transform.parent = gameObject.transform;
    }

    void OnCollisionEnter(Collision collision) {
        print("collision occurred with {collision.collider.name}");
        if (collision.collider.CompareTag("Player")) {
            StartCoroutine(spawn());
            }
        }

//    // Call coroutine to generate a potion on this platform
//    public void generate()
//    {
//
//    }

    // Wait timeToSpawn seconds before spawning potion
    IEnumerator spawn()
    {
      // Instantiate new object as child of platform
      var potion = Instantiate(activePotion,
                               position,
                               Quaternion.identity);
      potion.transform.parent = gameObject.transform;

      yield return new WaitForSeconds(timeToSpawn);
    }

}
