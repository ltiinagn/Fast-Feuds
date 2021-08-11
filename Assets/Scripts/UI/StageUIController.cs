using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class StageUIController : MonoBehaviour
{
    public GameConstants gameConstants;
    public GameObject bossHPContainer;
    public GameObject bossBlackBorderTop;
    public GameObject bossBlackBorderBottom;
    public GameObject ready;
    public GameObject FIGHT;
    public UnityEvent startNextSpawn;
    string[] levelNames;
    int levelIndex;

    void Start() {
        levelNames = gameConstants.levelNames;
        string[] buttonPaths = {"GameOverMenu/Panel/Restart_Button", "GameOverMenu/Panel/QuitToMenu_Button", "PauseMenu/Panel/Resume_Button", "PauseMenu/Panel/QuitToMenu_Button", "StageCompleteMenu/Panel/NextStage_Button", "StageCompleteMenu/Panel/QuitToMenu_Button"};
        foreach (string buttonPath in buttonPaths) {
            EventTrigger trigger = transform.parent.Find(buttonPath).GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnClicked((PointerEventData) data); });
            trigger.triggers.Add(entry);
        }
        if (SceneManager.GetActiveScene().name.Contains("-B")) {
            bossBlackBorderBottom.SetActive(true);
            bossBlackBorderTop.SetActive(true);
        }
        if (SceneManager.GetActiveScene().name.Contains("1-B")) {
            bossHPContainer.SetActive(true);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0.0f;
            transform.parent.Find("PauseMenu").gameObject.SetActive(true);
        }
    }

    public void showReadyFight() {
        StartCoroutine(showReadyFightCoroutine());
    }

    IEnumerator showReadyFightCoroutine() {
        ready.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        ready.SetActive(false);
        FIGHT.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        FIGHT.SetActive(false);
        startNextSpawn.Invoke();
    }

    public void showGameOver() {
        Time.timeScale = 0.0f;
        transform.parent.Find("GameOverMenu").gameObject.SetActive(true);
    }

    public void showStageComplete() {
        Time.timeScale = 0.0f;
        levelIndex = System.Array.IndexOf(levelNames, SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("complete"+levelNames[levelIndex], 1);
        PlayerPrefs.Save();
        levelIndex += 1;
        GameObject stageCompleteMenu = transform.parent.Find("StageCompleteMenu").gameObject;
        stageCompleteMenu.SetActive(true);
        if (levelIndex == levelNames.Length) {
            stageCompleteMenu.transform.Find("Panel/NextStage_Button").gameObject.SetActive(false);
        }
    }

    public void OnClicked(PointerEventData data)
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        if (name == "Resume_Button") {
            transform.parent.Find("PauseMenu").gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else if (name == "Restart_Button") {
            StartCoroutine(ChangeScene(SceneManager.GetActiveScene().name));
        }
        else if (name == "QuitToMenu_Button") {
            StartCoroutine(ChangeScene("MainMenu"));
        }
        else if (name == "NextStage_Button") {

        }
    }

    IEnumerator ChangeScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}