using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This gets attached to a potion generation platform
public class generatePotion : MonoBehaviour
{
    private bool hasPotion; // This platform has a potion
    public float timeToSpawn; // Time before potion spawns again

    // Start is called before the first frame update
    void Start()
    {
      hasPotion = true;
    }

    // Call coroutine to generate a potion on this platform
    void generate()
    {
      StartCoroutine(spawn());
    }

    // Wait timeToSpawn seconds before spawning potion
    IEnumerator spawn()
    {
      yield return new WaitForSeconds(timeToSpawn);
      // Instantiate new object as child of platform
      // For now, we will just generate dragon potions
      // Add other potions later
      //Instantiate()
    }

}
