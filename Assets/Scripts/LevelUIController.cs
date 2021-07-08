using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{
    void Start() {
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameObject.transform.parent.Find("PauseMenu").gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void OnClicked()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        if (name == "Resume_Button") {
            gameObject.transform.parent.Find("PauseMenu").gameObject.SetActive(false);
            Time.timeScale = 1.0f;
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