using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRedBubbleController : MonoBehaviour
{
    private BoxCollider boxCollider;
    private int waited;
    public float margin1;
    public float margin2;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        waited = 0;
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        animator = transform.parent.Find("Sprite").GetComponent<Animator>();
        animator.SetFloat("margin1Inverse", 1 / margin1);
        animator.SetFloat("margin2Inverse", 1 / margin2);
    }

    IEnumerator waitForSetInactive() {
        animator.SetTrigger("onGrow");
        yield return new WaitForSeconds(margin1);
        boxCollider.enabled = true;
        animator.SetTrigger("onBurst");
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
