using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBigMacSauceController : MonoBehaviour
{
    public Vector3 direction;
    public float moveSpeed; // Set in inspector
    // Start is called before the first frame update
    void Start()
    {

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
