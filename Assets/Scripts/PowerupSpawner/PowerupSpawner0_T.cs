using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerupSpawner0_T : MonoBehaviour
{
    public GameConstants gameConstants;
    public UnityEvent onWaveComplete;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;
    List<Vector3> keyList;

    private GameObject character;
    private bool spawned = false;

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

    IEnumerator waitForStartNextDialogue() {
        onWaveComplete.Invoke();
        yield return null;
    }

    public void spawnPowerup() {
        if (!spawned) {
            spawned = true;
            keyList = new List<Vector3>(keyMap.Values);
            keyList.Remove(character.transform.position);
            int index = Random.Range(0, keyList.Count);
            Instantiate(gameConstants.powerupWeaponPrefab, keyList[index], Quaternion.identity);
            keyList.RemoveAt(index);
            StartCoroutine(waitForStartNextDialogue());
        }
    }
}
