using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSequenceController : MonoBehaviour
{
    public GameObject[] introSequence;

    void Start() {
        StartCoroutine(showIntroSequence());
    }

    void Update() {
        if (Input.anyKeyDown) {
            StopCoroutine(showIntroSequence());
            StartCoroutine(ChangeScene("MainMenu"));
        }
    }

    IEnumerator showIntroSequence() {
        foreach (GameObject screen in introSequence) {
            screen.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            screen.SetActive(false);
        }
        StartCoroutine(ChangeScene("MainMenu"));
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