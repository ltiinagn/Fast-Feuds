using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageSelectionController : MonoBehaviour
{
    public GameConstants gameConstants;
    public GameObject skillTree;
    private int skill_IncreaseStartingHealth;

    void Start() {
        int i = -1;
        bool end = false;
        while (i < gameConstants.levelNames.Length - 1 && !end) {
            i += 1;
            Debug.Log(PlayerPrefs.GetInt("completeLevel1-1"));
            if (PlayerPrefs.GetInt("complete" + gameConstants.levelNames[i]) != 1) {
                end = true;
            }
        }
        i += 1;
        for (; i < gameConstants.levelNames.Length - 1; i++) {
            GameObject levelButton = GameObject.Find("UI/" + gameConstants.levelNames[i] + "_Button");
            if (levelButton) {
                levelButton.SetActive(false);
            }
        }
    }

    public void OnClicked()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(name);
        if (name == "SkillTree_Button") {
            skillTree.SetActive(!skillTree.activeSelf);
            skill_IncreaseStartingHealth = PlayerPrefs.GetInt("skill_IncreaseStartingHealth");
        }
        else if (name == "SkillIncreaseStartingHealth_Button") {
            skill_IncreaseStartingHealth += 1;
            PlayerPrefs.SetInt("skill_IncreaseStartingHealth", skill_IncreaseStartingHealth);
        }
        else if (name == "Back_Button") {
            StartCoroutine(ChangeScene("MainMenu"));
        }
        else if (name == "ClearSave_Button") {
            PlayerPrefs.DeleteAll();
        }
        else {
            StartCoroutine(ChangeScene(name.Split('_')[0]));
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