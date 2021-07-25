using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner1_2 : MonoBehaviour
{
    public GameConstants gameConstants;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;
    List<Vector3> keyList;

    private GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnPowerup() {
        keyList = new List<Vector3>(keyMap.Values);
        keyList.Remove(character.transform.position);
        int index = Random.Range(0, keyList.Count);
        // Instantiate(gameConstants.powerupInvulnerablePrefab, keyList[index], Quaternion.identity);
        Instantiate(gameConstants.powerupDestroyAllEnemiesPrefab, keyList[index], Quaternion.identity);
    }
}
