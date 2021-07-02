using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public CharacterConstants characterConstants;
    private int health;

    string[] rowNames = {"Row1", "Row2", "Row3", "Row4"};
    Dictionary <string, Vector3> keyMap = new Dictionary<string, Vector3>();
    Vector3 prevPos;
    float aboveGround = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        health = characterConstants.character_health;

        foreach (string rowName in rowNames) {
            foreach (Transform child in GameObject.Find(rowName).transform)
            {
                keyMap.Add(child.name, child.position);
            }
        }
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
                    this.transform.position = new Vector3(control.Value.x, aboveGround, control.Value.z);
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
            Debug.Log("damaged by enemy!");
            health -= 1;
            if (health == 0) {
                Destroy(gameObject);
            }
        }
    }
}
