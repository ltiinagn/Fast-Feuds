using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FriesTutorialController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public UnityEvent onEnemyDeath;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;
    List<Vector3> keyList;

    public GameObject character;
    GameObject dialogueBox;

    private Transform spriteParent;
    HashSet<string> spriteNames = new HashSet<string> {"Body"};
    List<SpriteRenderer> sprites = new List<SpriteRenderer> {};
    private Vector3 start;
    private Vector3 end;
    private bool faceRight = true;
    private bool dying = false;
    private float speed;
    private Animator animator;
    // private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyList = new List<Vector3>(keyMap.Values);
        foreach (Transform sprite in gameObject.transform.parent.Find("Sprite"))
        {
            if (spriteNames.Contains(sprite.name))
            {
                sprites.Add(sprite.GetComponent<SpriteRenderer>());
            }
        };
        character = GameObject.Find("Character");
        start = transform.position;
        keyList.Remove(start);
        end = keyList[Random.Range(0, keyList.Count)];
        spriteParent = gameObject.transform.parent.gameObject.transform;
        // faceRight = transform.position.x - character.transform.position.x < 0 ? true : false;
        speed = 0.5f;
        animator = gameObject.transform.parent.Find("Sprite").GetComponent<Animator>();
        // audioSource = GetComponent<AudioSource>();
        StartCoroutine(findDialogueBox());
        StartCoroutine(moveEnemyLoop());
    }

    IEnumerator findDialogueBox()
    {
        bool found = false;
        while (!found)
        {
            dialogueBox = GameObject.Find("UI/Dialogue");
            if (dialogueBox)
            {
                found = true;
            }
            yield return null;
        }
    }

    IEnumerator moveEnemyLoop()
    {
        while (!dying)
        {
            start = transform.position;
            end = new Vector3(character.transform.position.x, 0.0f, character.transform.position.z);
            if ((!dialogueBox || !dialogueBox.activeSelf) && (start-end).magnitude > 1) {
                moveEnemy(start, end);
            }
            yield return null;
        }
    }

    void moveEnemy(Vector3 from, Vector3 to)
    {
        bool moveRight = from.x - to.x < 0 ? true : false;
        if (moveRight != faceRight)
        {
            faceRight = !faceRight;
            spriteParent.Rotate(new Vector3(0, 0, 180));
        }
        // foreach (SpriteRenderer spriteRenderer in sprites)
        // {
        //     spriteRenderer.flipX = from.x - to.x > 0 ? true : false;
        // }
        Vector3 direction = (to - from).normalized;
        to = from + direction;
        float fracDist = Time.deltaTime * speed;
        gameObject.transform.parent.gameObject.transform.position = Vector3.Lerp(from, to, fracDist);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyEnemy()
    {
        dying = true;
        onEnemyDeath.Invoke();
        animator.SetTrigger("onDeath");
        // audioSource.PlayOneShot(audioSource.clip);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject.transform.parent.gameObject, 0.75f); // audioSource.clip.length);
    }
}