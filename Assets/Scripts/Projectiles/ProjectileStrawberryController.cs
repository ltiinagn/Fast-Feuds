using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStrawberryController : MonoBehaviour
{
    public GameObject character;
    public Vector3 direction;
    public float moveSpeed; // Set in inspector
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        StartCoroutine(waitForSetInactive());
    }

    IEnumerator waitForSetInactive() {
        yield return new WaitForSeconds(10.0f);
        SetInactive();
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
}
