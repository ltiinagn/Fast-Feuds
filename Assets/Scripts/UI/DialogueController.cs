using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public GameConstants gameConstants;
    public GameObject enemySpawner;
    private GameObject dialogueBox;
    private Text dialogueText;
    private string[] dialogue;
    private int dialogueProgress;
    private int[] spawnSequence;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
        dialogueBox = GameObject.Find("UI/Dialogue");
        dialogueText = dialogueBox.transform.Find("Panel/Dialogue_Text").GetComponent<Text>();
        dialogue = gameConstants.dialogue1_1;
        dialogueProgress = 0;
        LoadDialogue();
    }

    void LoadDialogue() {
        dialogueText.text = dialogue[dialogueProgress];
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueProgress != dialogue.Length) {
            if (Input.GetKeyDown("space")) {
                dialogueProgress += 1;
            }
            else if (dialogueProgress == 0 && Input.GetKeyDown("k")) {
                dialogueProgress = dialogue.Length;
            }

            if (dialogueProgress == dialogue.Length) {
                dialogueBox.SetActive(false);
                enemySpawner.SetActive(true);
                gameObject.SetActive(false);
            }
            else {
                LoadDialogue();
            }
        }
    }
}