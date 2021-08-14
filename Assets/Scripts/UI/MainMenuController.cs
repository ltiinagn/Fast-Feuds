using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject mainMenu;
    public GameObject startButton;
    public GameObject exitButton;

    void Start() {
        StartCoroutine(waitForShowMainMenu());
    }

    IEnumerator waitForShowMainMenu() {
        startScreen.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        startScreen.SetActive(false);
        mainMenu.SetActive(true);
        startButton.SetActive(true);
        exitButton.SetActive(true);
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