using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public GameConstants gameConstants;
    public CharacterConstants characterConstants;
    public UnityEvent onCharacterHit;
    public UnityEvent onCharacterMove;
    public UnityEvent onCharacterAddHealth;
    public GameObject keyMapper;
    public GameObject sphereCollider;
    public GameObject dialogueBox;
    Text dialogueText;
    Dictionary<string, Vector3> keyMap;

    private Transform sprite;
    Vector3 prevPos;
    bool weapon = false;
    string playStyle = "straightCutFry";
    bool faceRight = false;
    int bulletsPerDash;
    bool moving;
    bool invulnerablePowerup;
    float speed;
    private Animator characterAnimator;
    private AudioSource characterAudio;
    public AudioClip[] movementAudioClips;
    public AudioClip[] gruntingAudioClips;
    public AudioClip[] punchingAudioClips;
    public AudioClip[] ouchAudioClips;
    public AudioClip screamAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText = dialogueBox.transform.Find("Panel/Dialogue_Text").GetComponent<Text>();
        bulletsPerDash = 0;
        moving = false;
        invulnerablePowerup = false;
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        prevPos = this.transform.position;
        speed = characterConstants.characterSpeed;
        sprite = gameObject.transform.parent.Find("Sprite").transform;
        sprite.Rotate(new Vector3(0, 180, 0));
        faceRight = true;
        characterAnimator = gameObject.transform.parent.Find("Sprite").GetComponent<Animator>();
        characterAudio = gameObject.transform.parent.Find("AudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((!dialogueBox.activeSelf || dialogueBox.activeSelf && dialogueText.text.Contains("move to a tile")) && Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
                playStyle = playStyle == "straightCutFry" ? "meateor" : "straightCutFry";
                Debug.Log(playStyle);
            }
            else {
                foreach (KeyValuePair<string, Vector3> control in keyMap)
                {
                    if (Input.GetKeyDown(control.Key))
                    {
                        if (!moving)
                        {
                            if (playStyle == "straightCutFry" && control.Value != prevPos) {
                                StartCoroutine(moveStraightCutFry(prevPos, control.Value));
                            }
                            else if (playStyle == "meateor") {
                                StartCoroutine(moveMeateor(prevPos, control.Value));
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator moveMeateor(Vector3 from, Vector3 to) {
        bool moveRight = from.x - to.x < 0 ? true : false;
        if (moveRight != faceRight)
        {
            faceRight = !faceRight;
            sprite.Rotate(new Vector3(0, 180, 0));
        }
        characterAnimator.SetBool("isMoving", true);
        moving = true;
        bulletsPerDash = 1;
        characterAudio.PlayOneShot(movementAudioClips[Random.Range(0, movementAudioClips.Length)]);

        float startTime = Time.time;
        float fracDist = 0;
        Vector3 fromUp = new Vector3(from.x, from.y + 10, from.z);
        float distance = Vector3.Distance(from, fromUp);

        while (fracDist < 1) {
            float distCovered = (Time.time - startTime) * speed;
            fracDist = distCovered / distance;
            transform.parent.position = Vector3.Lerp(from, fromUp, fracDist);
            yield return null;
        }

        startTime = Time.time;
        fracDist = 0;
        Vector3 toUp = new Vector3(to.x, to.y + 10, to.z);
        distance = Vector3.Distance(toUp, to);

        while (fracDist < 1) {
            float distCovered = (Time.time - startTime) * speed;
            fracDist = distCovered / distance;
            transform.parent.position = Vector3.Lerp(toUp, to, fracDist);
            yield return null;
        }
        characterAnimator.SetBool("isMoving", false);
        prevPos = this.transform.position;
        sphereCollider.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        sphereCollider.SetActive(false);
        moving = false;
        bulletsPerDash = 1;
        onCharacterMove.Invoke();
    }

    IEnumerator moveStraightCutFry(Vector3 from, Vector3 to)
    {
        float startTime = Time.time;
        float distance = Vector3.Distance(from, to);
        bool moveRight = from.x - to.x < 0 ? true : false;
        if (moveRight != faceRight)
        {
            faceRight = !faceRight;
            sprite.Rotate(new Vector3(0, 180, 0));
        }
        characterAnimator.SetBool("isMoving", true);
        moving = true;
        bulletsPerDash = 1;
        characterAudio.PlayOneShot(movementAudioClips[Random.Range(0, movementAudioClips.Length)]);
        characterAudio.PlayOneShot(gruntingAudioClips[Random.Range(0, gruntingAudioClips.Length)]);
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
            transform.parent.position = Vector3.Lerp(from, to, fracDist);
            yield return null;

            // float dist = Vector3.Distance(prevPos, this.transform.position);
            // dir = this.transform.position - prevPos;

            // RaycastHit[] hits;
            // hits = Physics.RaycastAll(prevPos, dir, dist);
            // foreach (RaycastHit hit in hits)
            // {
            //     if (hit.transform.tag != "TileDanger" && hit.transform.tag != "ProjectileCollider")
            //     {
            //         Debug.Log(hit.transform.tag);
            //         hit.transform.gameObject.SendMessage("OnTriggerEnter", hit.collider);
            //     }
            // }
        }
        characterAnimator.SetBool("isMoving", false);
        prevPos = this.transform.position;
        moving = false;
        bulletsPerDash = 0;
        onCharacterMove.Invoke();
    }

    IEnumerator StartInvulnerablePowerup()
    {
        invulnerablePowerup = true;
        yield return new WaitForSeconds(gameConstants.invulnerablePowerupDuration);
        invulnerablePowerup = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Powerup"))
        {
            if (col.gameObject.name == "PowerupInvulnerable")
            {
                StartCoroutine(StartInvulnerablePowerup());
                col.gameObject.GetComponent<PowerupInvulnerableController>().UsePowerup();
            }
            else if (col.gameObject.name == "PowerupDestroyAllEnemies")
            {
                col.gameObject.GetComponent<PowerupDestroyAllEnemiesController>().UsePowerup();
            }
            else if (col.gameObject.name == "PowerupAddHealth")
            {
                col.gameObject.GetComponent<PowerupAddHealthController>().UsePowerup();
                onCharacterAddHealth.Invoke();
            }
            else if (col.gameObject.name == "PowerupWeapon") {
                col.gameObject.GetComponent<PowerupWeaponController>().UsePowerup();
                weapon = true;
            }
            // onCharacterHit.Invoke();
        }
        else if (col.gameObject.CompareTag("EnemyCollider") || col.gameObject.CompareTag("ChickenMoving"))
        {
            characterAudio.PlayOneShot(punchingAudioClips[Random.Range(0, punchingAudioClips.Length)]);
        }
        else if (col.gameObject.CompareTag("EnemyTutorialCollider") && weapon)
        {
            col.gameObject.GetComponent<FriesTutorialController>().DestroyEnemy();
            characterAudio.PlayOneShot(punchingAudioClips[Random.Range(0, punchingAudioClips.Length)]);
        }
        else
        {
            if (!invulnerablePowerup)
            {
                if (col.gameObject.CompareTag("TileDanger"))
                {
                    characterAudio.PlayOneShot(ouchAudioClips[Random.Range(0, ouchAudioClips.Length)]);
                    onCharacterHit.Invoke();
                }
                else if (col.gameObject.CompareTag("ProjectileCollider"))
                {
                    if (bulletsPerDash > 0)
                    {
                        bulletsPerDash -= 1;
                    }
                    else
                    {
                        characterAnimator.SetTrigger("onHit");
                        characterAudio.PlayOneShot(ouchAudioClips[Random.Range(0, ouchAudioClips.Length)]);
                        col.gameObject.SendMessage("SetInactive");
                        onCharacterHit.Invoke();
                    }
                }
                else if (col.gameObject.CompareTag("MeleeCollider"))
                {
                    characterAnimator.SetTrigger("onHit");
                    characterAudio.PlayOneShot(ouchAudioClips[Random.Range(0, ouchAudioClips.Length)]);
                    onCharacterHit.Invoke();
                }
            }
        }
    }

    public void playerDeath()
    {
        characterAnimator.SetTrigger("onDeath");
        characterAudio.PlayOneShot(screamAudioClip);
        Destroy(gameObject.transform.parent.gameObject, 1);
    }
}
