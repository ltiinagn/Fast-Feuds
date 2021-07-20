using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTypeAController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public UnityEvent onEnemyDeath;
    public UnityEvent onCharacterHit;
    public GameObject MovesText;
    private int health;
    private int movesLeft;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(int movesAllowed) {
        movesLeft = movesAllowed;
        MovesText.GetComponent<TextMesh>().text = movesLeft.ToString();
        health = enemyConstants.enemyTypeAHealth;
    }

    void OnTriggerEnter(Collider col) {
        health -= 1;
        Debug.Log("damaged by character!");
        if (health == 0) {
            onEnemyDeath.Invoke();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public void characterMoved() {
        movesLeft -= 1;
        MovesText.GetComponent<TextMesh>().text = movesLeft.ToString();
        Debug.Log("characterMoved");
        if (movesLeft == 0 && health!= 0) {
            onCharacterHit.Invoke();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
