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
    public UnityEvent onCharacterAddHealth;
    public GameObject keyMapper;
    GameObject dialogueBox;
    Dictionary<string, Vector3> keyMap;

    HashSet<string> spriteNames = new HashSet<string> {"Body", "LeftLeg", "RightLeg"};
    List<SpriteRenderer> sprites = new List<SpriteRenderer> {};
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
        foreach (Transform sprite in gameObject.transform.parent.Find("Sprite")) {
            if (spriteNames.Contains(sprite.name)) {
                sprites.Add(sprite.GetComponent<SpriteRenderer>());
            }
        };
        prevPos = this.transform.position;
        speed = characterConstants.characterSpeed;
        dialogueBox = GameObject.Find("UI/Dialogue");
    }

    // Update is called once per frame
    void Update()
    {
        if ((!dialogueBox || !dialogueBox.activeSelf) && Input.anyKeyDown)
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
        int direction = from.x - to.x > 0 ? 0 : 1;
        foreach (SpriteRenderer spriteRenderer in sprites) {
            spriteRenderer.flipX = direction == 1 ? true : false;
        }
        invulnerable = true;
        float fracDist = 0;

        Vector3 dir = to - from;
        List<Vector3> objectList = new List<Vector3>();
        RaycastHit[] hits;
        hits = Physics.RaycastAll(from, dir, distance);
        // hits = System.Array.FindAll(hits, h => h.transform.tag == "Obstacle"); // For total block

        System.Array.Sort(hits, (x,y) => x.distance.CompareTo(y.distance));

        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].transform.tag == "Obstacle" && hits[i].transform.gameObject.activeSelf) {
                if (i == 0 || i == 1) {
                    to = from;
                }
                else {
                    if (hits[i-1].collider.transform.position == hits[i].collider.transform.position) {
                        to = hits[i-2].collider.transform.position;
                    }
                    else {
                        to = hits[i-1].collider.transform.position;
                    }
                }
                break;
            }
        }

        while (fracDist < 1) {
            float distCovered = (Time.time - startTime) * speed;
            fracDist = distCovered / distance;
            gameObject.transform.parent.gameObject.transform.position = Vector3.Lerp(from, to, fracDist);

            float dist = Vector3.Distance(prevPos, this.transform.position);
            dir = this.transform.position - prevPos;

            // RaycastHit[] hits;
            // hits = Physics.RaycastAll(prevPos, dir, dist);
            // foreach (RaycastHit hit in hits) {
            //     if (hit.transform.tag != "TileDanger" && hit.transform.tag != "Bullet1") {
            //         Debug.Log(hit.transform.tag);
            //         hit.transform.gameObject.SendMessage("OnTriggerEnter", hit.collider);
            //     }
            // }
            prevPos = this.transform.position;

            yield return null;
        }
        invulnerable = false;
        onCharacterMove.Invoke();
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
                col.gameObject.SendMessage("UsePowerup");
            }
            else if (col.gameObject.name == "PowerupDestroyAllEnemies") {
                col.gameObject.SendMessage("UsePowerup");
            }
            else if (col.gameObject.name == "PowerupAddHealth") {
                col.gameObject.SendMessage("UsePowerup");
                onCharacterAddHealth.Invoke();
            }
            // onCharacterHit.Invoke();
        }
        else {
            if (!invulnerable && !invulnerablePowerup) {
                if (col.gameObject.CompareTag("TileDanger")) {
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