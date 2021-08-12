using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBigMacSauceController : MonoBehaviour
{
    public Vector3 direction;
    public float moveSpeed; // Set in inspector
    public bool reenabled;
    private float delay;

    // Start is called before the first frame update
    void Start()
    {
        delay = 0.5f - 0.04f *  moveSpeed;
        reenabled = true;
    }

    IEnumerator initialNoCollision() {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        yield return new WaitForSeconds(delay);
        boxCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (reenabled) {
            reenabled = false;
            StartCoroutine(initialNoCollision());
        }
        transform.parent.transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void OnBecameInvisible() {
        reenabled = true;
        transform.parent.gameObject.SetActive(false);
    }

    void SetInactive() {
        reenabled = true;
        transform.parent.gameObject.SetActive(false);
    }
}
