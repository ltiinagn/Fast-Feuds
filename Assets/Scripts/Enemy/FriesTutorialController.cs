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
    private bool dying = false;
    private Vector3 start;
    private Vector3 end;
    private float speed;
    private bool faceRight = true;
    List<SpriteRenderer> spriteDescendants = new List<SpriteRenderer> {};
    private Animator animator;
    // private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyList = new List<Vector3>(keyMap.Values);
        character = GameObject.Find("Character");
        start = transform.position;
        keyList.Remove(start);
        end = keyList[Random.Range(0, keyList.Count)];
        speed = 0.5f;
        spriteParent = transform.parent.gameObject.transform;
        foreach (Transform spriteChild in transform.parent.Find("Sprite"))
        {
            spriteDescendants.Add(spriteChild.GetComponent<SpriteRenderer>());
            foreach (Transform spriteGrandchild in spriteChild)
            {
                if (null == spriteGrandchild)
                {
                    continue;
                }
                spriteDescendants.Add(spriteGrandchild.GetComponent<SpriteRenderer>());
            }
        };
        animator = transform.parent.Find("Sprite").GetComponent<Animator>();
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
        if (moveRight != faceRight && !dying)
        {
            faceRight = !faceRight;
            spriteParent.Rotate(new Vector3(0, 0, 180));
        }
        Vector3 direction = (to - from).normalized;
        to = from + direction;
        float fracDist = Time.deltaTime * speed;
        transform.parent.position = Vector3.Lerp(from, to, fracDist);
    }

    IEnumerator fadeIntoOblivion(List<SpriteRenderer> sprites, float startTime, float totalDuration)
    {
        float counter = 0;
        float fadeDuration = totalDuration - startTime;

        yield return new WaitForSeconds(startTime);

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            foreach (SpriteRenderer spriteRenderer in sprites)
            {
                spriteRenderer.material.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, counter / fadeDuration));
            }
            yield return null;
        }
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
        StartCoroutine(fadeIntoOblivion(spriteDescendants, 0, 0.75f));
        // audioSource.PlayOneShot(audioSource.clip);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Destroy(transform.parent.gameObject, 0.75f); // audioSource.clip.length);
    }
}
