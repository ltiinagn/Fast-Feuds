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

    public Image black;
    //public Animator anim;

    void Start() {
        int i = -1;
        bool end = false;
        while (i < gameConstants.levelNames.Length - 1 && !end) {
            i += 1;
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
        //set Fade bool to true, stay on scene till fade completes,
        //then wait till it's black to show next scene

        //anim.SetBool("Fade", true);
        //yield return new WaitUntil(()=>black.color.a==1);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


}