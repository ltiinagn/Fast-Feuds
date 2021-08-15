using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoneController : MonoBehaviour
{
    public Vector3 direction;
    public float moveSpeed; // Set in inspector
    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator WaitForDestroy() {
        yield return new WaitForSeconds(2.0f);
        SetInactive();
    }

    void OnEnable() {
        StartCoroutine(WaitForDestroy());
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
