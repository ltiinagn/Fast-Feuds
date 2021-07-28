using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageUIController : MonoBehaviour
{
    public GameConstants gameConstants;
    string[] levelNames;
    int levelIndex;

    void Start() {
        levelNames = gameConstants.levelNames;
        string[] buttonPaths = {"GameOverMenu/Panel/Restart_Button", "GameOverMenu/Panel/QuitToMenu_Button", "PauseMenu/Panel/Resume_Button", "PauseMenu/Panel/QuitToMenu_Button", "StageCompleteMenu/Panel/NextStage_Button", "StageCompleteMenu/Panel/QuitToMenu_Button"};
        foreach (string buttonPath in buttonPaths) {
            EventTrigger trigger = gameObject.transform.parent.Find(buttonPath).GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnClicked((PointerEventData) data); });
            trigger.triggers.Add(entry);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0.0f;
            gameObject.transform.parent.Find("PauseMenu").gameObject.SetActive(true);
        }
    }

    public void showGameOver() {
        Time.timeScale = 0.0f;
        gameObject.transform.parent.Find("GameOverMenu").gameObject.SetActive(true);
    }

    public void showStageComplete() {
        Time.timeScale = 0.0f;
        levelIndex = System.Array.IndexOf(levelNames, SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("complete"+levelNames[levelIndex], 1);
        PlayerPrefs.Save();
        levelIndex += 1;
        GameObject stageCompleteMenu = gameObject.transform.parent.Find("StageCompleteMenu").gameObject;
        stageCompleteMenu.SetActive(true);
        if (levelIndex == levelNames.Length) {
            stageCompleteMenu.transform.Find("Panel/NextStage_Button").gameObject.SetActive(false);
        }
    }

    public void OnClicked(PointerEventData data)
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        if (name == "Resume_Button") {
            gameObject.transform.parent.Find("PauseMenu").gameObject.SetActive(false);
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