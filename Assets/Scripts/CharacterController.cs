using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    string[] rowNames = {"Row1", "Row2", "Row3", "Row4"};
    Dictionary <string, Vector3> keyMap = new Dictionary<string, Vector3>();
    float aboveGround = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (string rowName in rowNames) {
            foreach (Transform child in GameObject.Find(rowName).transform)
            {
                keyMap.Add(child.name, child.position);
            }
        }
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
                }
            }
        }
    }
}
