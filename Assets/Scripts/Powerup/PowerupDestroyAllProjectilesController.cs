using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupDestroyAllProjectilesController : MonoBehaviour
{
    public GameConstants gameConstants;
    // public Vector3 direction;
    // private int moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForDestroy());
    }

    IEnumerator WaitForDestroy() {
        yield return new WaitForSeconds(gameConstants.powerupVisibilityDuration);
        for (int i = 0; i < gameConstants.powerupDisappearDuration * 2; i++) {
            transform.parent.transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = !transform.parent.transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled;
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void OnBecameInvisible() {
        gameObject.SetActive(false);
    }

    public void UsePowerup() {
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject gameObject in gameObjects) {
            Destroy(gameObject);
        }
        Destroy(transform.parent.gameObject);
    }
}
