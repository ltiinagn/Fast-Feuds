using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public GameConstants gameConstants;
    public CharacterConstants characterConstants;
    public UnityEvent onPlaystyleChange;
    public UnityEvent onCharacterHit;
    public UnityEvent onCharacterMove;
    public UnityEvent onCharacterAddHealth;
    public GameObject keyMapper;
    public GameObject dialogueBox;
    Text dialogueText;
    Dictionary<string, Vector3> keyMap;

    string sceneName;
    private Transform sprite;
    Vector3 prevPos;
    bool weapon = false;
    string playStyle = "straightCutFry";
    bool faceRight = false;
    int initialBulletsPerDash;
    int bulletsPerDash;
    bool moving;
    bool invulnerablePowerup;
    float speed; // for straightCutFry
    float upSpeed; // for meateor
    float delay; // for meateor
    private GameObject sphereCollider;
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
        sceneName = SceneManager.GetActiveScene().name;
        dialogueText = dialogueBox.transform.Find("Panel/Dialogue_Text").GetComponent<Text>();
        initialBulletsPerDash = PlayerPrefs.GetInt("skill_Skill3");
        bulletsPerDash = 0;
        moving = false;
        invulnerablePowerup = false;
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        prevPos = this.transform.position;
        speed = characterConstants.characterSpeed * (PlayerPrefs.GetInt("skill_Skill2") + 1);
        sphereCollider = transform.parent.Find("SphereCollider").gameObject;
        float scaleIncrease = PlayerPrefs.GetInt("skill_Skill3") * 0.75f;
        scaleIncrease = 3 * 0.75f;
        Vector3 sphereColliderScale = sphereCollider.transform.localScale;
        sphereColliderScale.x += scaleIncrease;
        sphereColliderScale.y += scaleIncrease;
        sphereColliderScale.z += scaleIncrease;
        sphereCollider.transform.localScale = sphereColliderScale;
        sphereCollider.SetActive(false);
        upSpeed = characterConstants.characterSpeed;
        delay = characterConstants.characterDelay / (PlayerPrefs.GetInt("skill_Skill2") + 1);
        sprite = transform.parent.Find("Sprite").transform;
        sprite.Rotate(new Vector3(0, 180, 0));
        faceRight = true;
        characterAnimator = transform.parent.Find("Sprite").GetComponent<Animator>();
        characterAudio = transform.parent.Find("AudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((!dialogueBox.activeSelf || dialogueBox.activeSelf && dialogueText.text.Contains("move to a tile")) && Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) {
                if (!sceneName.Contains("Level0") && !sceneName.Contains("Level1") && !sceneName.Contains("-B")) {
                    playStyle = playStyle == "straightCutFry" ? "meateor" : "straightCutFry";
                    onPlaystyleChange.Invoke();
                }
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
        characterAudio.PlayOneShot(movementAudioClips[Random.Range(0, movementAudioClips.Length)]);
        bulletsPerDash = initialBulletsPerDash;
        float startTime = Time.time;
        float fracDist = 0;
        Vector3 fromUp = new Vector3(from.x, from.y + 10, from.z);
        float distance = Vector3.Distance(from, fromUp);

        while (fracDist < 1) {
            float distCovered = (Time.time - startTime) * upSpeed;
            fracDist = distCovered / distance;
            transform.parent.position = Vector3.Lerp(from, fromUp, fracDist);
            yield return null;
        }

        yield return new WaitForSeconds(delay);

        startTime = Time.time;
        fracDist = 0;
        Vector3 toUp = new Vector3(to.x, to.y + 10, to.z);
        distance = Vector3.Distance(toUp, to);

        while (fracDist < 1) {
            float distCovered = (Time.time - startTime) * upSpeed;
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
        bulletsPerDash = 0;
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
        bulletsPerDash = initialBulletsPerDash;
        moving = true;
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
                    try {
                        col.gameObject.SendMessage("SetInactive");
                    } catch {

                    }

                    if (bulletsPerDash > 0)
                    {
                        bulletsPerDash -= 1;
                    }
                    else
                    {
                        characterAnimator.SetTrigger("onHit");
                        characterAudio.PlayOneShot(ouchAudioClips[Random.Range(0, ouchAudioClips.Length)]);
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
        Destroy(transform.parent.gameObject, 1);
    }
}
