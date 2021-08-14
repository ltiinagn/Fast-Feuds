using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject startButton;
    public GameObject exitButton;

    void Start() {

    }

    public void startButtonClicked() {
        StartCoroutine(ChangeScene("StageSelection"));
    }

    public void exitButtonClicked() {
        Application.Quit();
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