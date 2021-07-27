using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueController1_1 : MonoBehaviour
{
    public GameConstants gameConstants;
    public GameObject enemySpawner;
    public UnityEvent startNextSpawn;
    private GameObject dialogueBox;
    private Text dialogueText;
    private string[][] dialogue;
    private int progress0;
    private int progress1;
    private bool finishedSet;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = GameObject.Find("UI/Dialogue");
        dialogueText = dialogueBox.transform.Find("Panel/Dialogue_Text").GetComponent<Text>();
        dialogue = gameConstants.dialogue1_1;
        progress0 = 0;
        progress1 = 0;
        finishedSet = false;
        LoadDialogue();
    }

    void LoadDialogue() {
        dialogueText.text = dialogue[progress0][progress1];
    }

    public void LoadNextDialogue() {
        finishedSet = false;
        dialogueBox.SetActive(true);
        LoadDialogue();
    }

    IEnumerator waitForStartNextSpawn() {
        yield return new WaitForSeconds(1);
        startNextSpawn.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (!finishedSet) {
            if (progress1 != dialogue[progress0].Length) {
                if (Input.GetKeyDown("space")) {
                    progress1 += 1;
                }
                else if (progress1 == 0 && Input.GetKeyDown("k")) {
                    progress1 = dialogue[progress0].Length;
                }

                if (progress1 == dialogue[progress0].Length) {
                    finishedSet = true;
                    progress0 += 1;
                    progress1 = 0;
                    dialogueBox.SetActive(false);
                    enemySpawner.SetActive(true);
                    
                    if (progress0 != 0 && progress0 < dialogue.Length) {
                        StartCoroutine(waitForStartNextSpawn());
                    }
                }
                else {
                    LoadDialogue();
                }
            }
        }
    }
}