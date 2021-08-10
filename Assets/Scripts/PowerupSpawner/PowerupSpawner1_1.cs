using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner1_1 : MonoBehaviour
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

    IEnumerator spawnPowerupInterval() {
        int index;
        int invulnerableIndex = Random.Range(0, 5);
        for (int i = 0; i < 5; i++) {
            index = Random.Range(0, keyList.Count);
            if (i != invulnerableIndex) {
                Instantiate(gameConstants.powerupAddHealthPrefab, keyList[index], Quaternion.identity);
            }
            else {
                Instantiate(gameConstants.powerupInvulnerablePrefab, keyList[index], Quaternion.identity);
            }
            keyList.RemoveAt(index);

            yield return new WaitForSeconds(1.0f);
        }
    }

    public void spawnPowerup() {
        keyList = new List<Vector3>(keyMap.Values);
        keyList.Remove(character.transform.position);
        StartCoroutine(spawnPowerupInterval());
    }
}
