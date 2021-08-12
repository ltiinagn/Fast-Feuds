using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChickenMovesController : MonoBehaviour
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
        health = enemyConstants.enemyHealth;
    }

    void OnTriggerEnter(Collider col) {
        health -= 1;
        if (health == 0) {
            onEnemyDeath.Invoke();
            Destroy(transform.parent.gameObject);
        }
    }

    public void characterMoved() {
        movesLeft -= 1;
        MovesText.GetComponent<TextMesh>().text = movesLeft.ToString();
        if (movesLeft == 0 && health!= 0) {
            onCharacterHit.Invoke();
            onEnemyDeath.Invoke();
            Destroy(transform.parent.gameObject);
        }
    }
}
