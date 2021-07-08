using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    public GameConstants gameConstants;
    public UnityEvent onCharacterHit;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;

    Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        prevPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyValuePair<string, Vector3> control in keyMap) {
                if (Input.GetKeyDown(control.Key)) {
                    float step = 1 * Time.deltaTime;
                    gameObject.transform.parent.transform.position = control.Value;
                    float dist = Vector3.Distance(prevPos, this.transform.position);
                    Vector3 dir = this.transform.position - prevPos;

                    RaycastHit[] hits;
                    hits = Physics.RaycastAll(prevPos, dir, dist);
                    foreach (RaycastHit hit in hits) {
                        Debug.Log("hit");
                        hit.transform.gameObject.SendMessage("OnTriggerEnter", hit.collider);
                    }
                    prevPos = this.transform.position;
                }
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        Debug.Log("collided");
        if (col.gameObject.CompareTag("EnemyType1")) {
            // Debug.Log("damaged by enemy!");
            // health -= 1;
            // if (health == 0) {
            //     Destroy(gameObject);
            // }
            onCharacterHit.Invoke();
        }
    }

    public void playerDeath() {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
