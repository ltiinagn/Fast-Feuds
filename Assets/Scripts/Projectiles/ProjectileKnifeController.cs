using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileKnifeController : MonoBehaviour
{
    public GameObject character;
    public Vector3 direction;
    public float moveSpeed; // Set in inspector
    public int phase;
    private int waited;
    private float speedIncrease;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        waited = 0;
        character = GameObject.Find("Character");
        spriteRenderer = transform.parent.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    IEnumerator waitForFire() {
        GetComponent<Collider>().enabled = false;
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        yield return new WaitForSeconds(1.5f);
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider>().enabled = true;
        if (phase == 1) {
            if (speedIncrease < 4) {
                speedIncrease += 0.2f;
            }
            StartCoroutine(waitForSetInactive());
        }
        else if (phase == 2) {
            if (speedIncrease < 12) {
                speedIncrease += 0.4f;
            }
        }
        waited = 2;
    }

    IEnumerator waitForSetInactive() {
        yield return new WaitForSeconds(5.0f);
        SetInactive();
    }

    // Update is called once per frame
    void Update()
    {
        if (waited == 0) {
            waited = 1;
            StartCoroutine(waitForFire());
        }
        if (waited == 2) {
            if (phase == 1) {
                direction = -transform.forward;
                Quaternion neededRotation = Quaternion.LookRotation(-(character.transform.position - transform.position));
                transform.parent.rotation = Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * 50.0f);
                moveSpeed = 3.0f + speedIncrease;
            }
            else if (phase == 2) {
                moveSpeed = 2.0f + speedIncrease;
            }
            transform.parent.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void OnBecameInvisible() {
        waited = 0;
        transform.parent.gameObject.SetActive(false);
    }

    void SetInactive() {
        waited = 0;
        transform.parent.gameObject.SetActive(false);
    }
}
