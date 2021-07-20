using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    public GameConstants gameConstants;
    public CharacterConstants characterConstants;
    public UnityEvent onCharacterHit;
    public UnityEvent onCharacterMove;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;

    Vector3 prevPos;
    bool invulnerable;
    bool invulnerablePowerup;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        invulnerable = false;
        invulnerablePowerup = false;
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        prevPos = this.transform.position;
        speed = characterConstants.characterSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0 && Input.anyKeyDown)
        {
            foreach (KeyValuePair<string, Vector3> control in keyMap) {
                if (Input.GetKeyDown(control.Key)) {
                    if (control.Value != prevPos && !invulnerable) {
                        StartCoroutine(moveCharacter(prevPos, control.Value));
                    }
                }
            }
        }
    }

    IEnumerator moveCharacter(Vector3 from, Vector3 to) {
        float startTime = Time.time;
        float distance = Vector3.Distance(from, to);
        invulnerable = true;

        while (true) {
            float distCovered = (Time.time - startTime) * speed;
            float fracDist = distCovered / distance;
            gameObject.transform.parent.gameObject.transform.position = Vector3.Lerp(from, to, fracDist);

            float dist = Vector3.Distance(prevPos, this.transform.position);
            Vector3 dir = this.transform.position - prevPos;

            // RaycastHit[] hits;
            // hits = Physics.RaycastAll(prevPos, dir, dist);
            // foreach (RaycastHit hit in hits) {
            //     if (hit.transform.tag != "TileDanger" && hit.transform.tag != "Bullet1") {
            //         Debug.Log(hit.transform.tag);
            //         hit.transform.gameObject.SendMessage("OnTriggerEnter", hit.collider);
            //     }
            // }
            prevPos = this.transform.position;

            if (fracDist >= 1) {
                invulnerable = false;
                onCharacterMove.Invoke();
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator StartInvulnerablePowerup() {
        invulnerablePowerup = true;
        yield return new WaitForSeconds(gameConstants.invulnerablePowerupDuration);
        invulnerablePowerup = false;
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Powerup")) {
            if (col.gameObject.name == "PowerupInvulnerable") {
                StartCoroutine(StartInvulnerablePowerup());
            }
            col.gameObject.SendMessage("Destroy");
            // onCharacterHit.Invoke();
        }
        else {
            if (!invulnerable && !invulnerablePowerup) {
                if (col.gameObject.CompareTag("EnemyType1")) {
                    onCharacterHit.Invoke();
                }
                else if (col.gameObject.CompareTag("TileDanger")) {
                    onCharacterHit.Invoke();
                }
                else if (col.gameObject.CompareTag("Bullet1")) {
                    col.gameObject.SendMessage("SetInactive");
                    onCharacterHit.Invoke();
                }
            }
        }
    }

    public void playerDeath() {
        Destroy(gameObject.transform.parent.gameObject);
    }
}