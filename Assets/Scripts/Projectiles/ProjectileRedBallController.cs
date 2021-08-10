using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRedBallController : MonoBehaviour
{
    public ProjectileRedBallSpawner spawner;
    public Vector3 direction;
    public float moveSpeed; // Set in inspector
    public Vector3 endPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void OnBecameInvisible() {
        transform.parent.gameObject.SetActive(false);
    }

    void SetInactive() {
        transform.parent.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider col) {
        if (col.transform.parent.position == endPosition) {
            spawner.spawnCount -= 1;
            SetInactive();
        }
        else if (col.gameObject.CompareTag("Character")) {
            spawner.spawnCount -= 1;
            spawner.initialSpawnCount -= 1;
            SetInactive();
        }
    }
}
