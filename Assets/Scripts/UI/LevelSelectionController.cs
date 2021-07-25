using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    public GameObject skillTree;
    private int skill_IncreaseStartingHealth;

    void Start() {
        // if (name == "SkillPoints")
        // {
        //     eachChild.gameObject.SetActive(false);

        // }
    }

    public void OnClicked()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        if (name == "SkillTree_Button") {
            skillTree.SetActive(!skillTree.activeSelf);
            skill_IncreaseStartingHealth = PlayerPrefs.GetInt("skill_IncreaseStartingHealth");
            //if skill 1/2/3 selected
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
        // else if(name == "Skill1Select"){

        // }
        //skilltree, level selection, clear save, back (for debug prolly)
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