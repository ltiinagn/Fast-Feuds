using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupAddHealthController : MonoBehaviour
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
            gameObject.transform.parent.transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = !gameObject.transform.parent.transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled;
            yield return new WaitForSeconds(0.5f);
        }
        UsePowerup();
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
        Destroy(gameObject.transform.parent.gameObject);
    }
}
