using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRedBubbleController : MonoBehaviour
{
    private BoxCollider boxCollider;
    private int waited;
    public float margin1;
    public float margin2;

    // Start is called before the first frame update
    void Start()
    {
        waited = 0;
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    IEnumerator waitForSetInactive() {
        // TODO: first set of animation
        yield return new WaitForSeconds(margin1);
        boxCollider.enabled = true;
        // TODO: second set of animation
        yield return new WaitForSeconds(margin2);
        boxCollider.enabled = false;
        SetInactive();
    }

    // Update is called once per frame
    void Update()
    {
        if (waited == 0) {
            waited = 1;
            StartCoroutine(waitForSetInactive());
        }
    }

    void OnBecameInvisible() {
        transform.parent.gameObject.SetActive(false);
    }

    void SetInactive() {
        waited = 0;
        transform.parent.gameObject.SetActive(false);
    }
}
