using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{
    void Start() {
        string[] buttonPaths = {"GameOverMenu/Restart_Button", "GameOverMenu/QuitToMenu_Button", "PauseMenu/Resume_Button", "PauseMenu/QuitToMenu_Button"};
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