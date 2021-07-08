using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMapping : MonoBehaviour
{
    public GameConstants gameConstants;
    public Dictionary<string, Vector3> keyMap = new Dictionary<string, Vector3>();
    public Dictionary<string, string> keyRowMap = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (string rowName in gameConstants.rowNames) {
            foreach (Transform child in GameObject.Find(rowName).transform)
            {
                keyMap.Add(child.name, child.position);
                keyRowMap.Add(child.name, rowName);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
