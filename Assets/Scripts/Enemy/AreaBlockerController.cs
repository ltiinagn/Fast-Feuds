using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBlockerController : MonoBehaviour
{
    private BoxCollider boxCollider;
    private float xInitialPosition;
    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        xInitialPosition = 11.0f;
        animator = transform.parent.Find("Sprite").GetComponent<Animator>();
    }

    IEnumerator ActivatePeriodically(bool wait)
    {
        yield return new WaitForSeconds(5.0f);
        while (true)
        {
            float xPosition = xInitialPosition + Random.Range(-1, 2) * 2.0f;
            transform.parent.position = new Vector3(xPosition, transform.parent.position.y, transform.parent.position.z);
            animator.SetTrigger("onActive");
            yield return new WaitForSeconds(3.0f);
            boxCollider.enabled = true;
            yield return new WaitForSeconds(3.0f);
            boxCollider.enabled = false;
            yield return new WaitForSeconds(3.0f);
        }
    }

    public void SetSpawnerInactive()
    {
        gameObject.SetActive(false);
    }

    public void SetSpawnerActive()
    {
        gameObject.SetActive(true);
        StartCoroutine(ActivatePeriodically(false));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
