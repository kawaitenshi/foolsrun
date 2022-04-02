using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawnLower : MonoBehaviour
{
    // TODO: Fiddle with detection radius
    private const float DETECTION_RADIUS = 4.0f;
    public GameObject activePotion; // The potion to generate on this spot
    private bool fell_already;
    float elapsed = 0f;

    private Transform _transform;

    private Vector3 _origin;
    private Vector3 position;


//     Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _origin = _transform.position;
    }

    private bool isPlayerCollider(Collider collider)
    {
        return collider.tag.Equals("Player");
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        Collider[] hitColliders = Physics.OverlapSphere(_origin, DETECTION_RADIUS);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (isPlayerCollider(hitColliders[i]) && fell_already == false)
            {
                position = new Vector3(0f, 6f, 0f);
                position += gameObject.transform.position;
                var potion = Instantiate(activePotion,
                                           position,
                                           Quaternion.identity);
                potion.transform.parent = gameObject.transform;
                fell_already = true;
                elapsed = 0f;
            }
        }
        if (elapsed >= 4f) {fell_already = false;}
   }
}
