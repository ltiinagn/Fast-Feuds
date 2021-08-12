using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageSelectionController : MonoBehaviour
{
    public GameConstants gameConstants;
    public GameObject skillTree;
    public GameObject[] stageButtons;
    public GameObject[] levels;
    public GameObject[] levelBackgrounds;
    public GameObject[] levelText;
    public GameObject[] stageBorders;
    private int skill_IncreaseStartingHealth;

    public Image black;
    //public Animator anim;

    void Start() {
        int i = -1;
        bool end = false;
        while (i < gameConstants.levelNames.Length - 1 && !end) {
            i += 1;
            //Debug.Log(PlayerPrefs.GetInt("complete" + gameConstants.levelNames[i]));
            if (PlayerPrefs.GetInt("complete" + gameConstants.levelNames[i]) != 1) {
                end = true;
            }
        }
        string level = gameConstants.levelNames[i].Substring(0, 6);
        string stage = gameConstants.levelNames[i].Substring(0, 8);
        foreach (GameObject gameObj in levels) {
            gameObj.SetActive(true);
            if (gameObj.name.Contains(level)) {
                break;
            }
        }
        foreach (GameObject gameObj in stageButtons) {
            gameObj.SetActive(true);
            if (gameObj.name.Contains(stage)) {
                break;
            }
        }
        foreach (GameObject gameObj in levelBackgrounds) {
            gameObj.SetActive(true);
            if (gameObj.name.Contains(level)) {
                break;
            }
        }
        foreach (GameObject gameObj in levelText) {
            gameObj.SetActive(true);
            if (gameObj.name.Contains(level)) {
                break;
            }
        }
        foreach (GameObject gameObj in stageBorders) {
            gameObj.SetActive(true);
            if (gameObj.name.Contains(level)) {
                break;
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