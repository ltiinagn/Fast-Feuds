using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBlockerController : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    private GameObject sprite;
    private BoxCollider boxCollider;
    private SpriteRenderer spriteRenderer;
    private Color red;
    private Color yellow;
    private float xInitialPosition;

    // Start is called before the first frame update
    void Start()
    {
        sprite = transform.parent.Find("Sprite").gameObject;
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        spriteRenderer = sprite.transform.Find("Body").GetComponent<SpriteRenderer>();
        sprite.SetActive(false);
        red = new Color(1.0f, 0.0f, 0.0f, 0.3f);
        yellow = new Color(1.0f, 0.92f, 0.016f, 0.3f);
        xInitialPosition = 11.0f;

        StartCoroutine(ActivatePeriodically());
    }

    IEnumerator ActivatePeriodically() {
        yield return new WaitForSeconds(30.0f);
        while (true) {
            float xPosition = xInitialPosition + Random.Range(-1, 2) * 2.0f;
            transform.parent.position = new Vector3(xPosition, transform.parent.position.y, transform.parent.position.z);
            spriteRenderer.color = yellow;
            sprite.SetActive(true);
            yield return new WaitForSeconds(3.0f);
            spriteRenderer.color = red;
            boxCollider.enabled = true;
            yield return new WaitForSeconds(3.0f);
            boxCollider.enabled = false;
            sprite.SetActive(false);
            yield return new WaitForSeconds(3.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
