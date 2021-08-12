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
    public GameObject skillPointsTextGameObject;
    Text skillPointsText;
    private int skillPoints;
    private int skill_IncreaseStartingHealth;
    private int skill_Skill2;
    private int skill_Skill3;

    public Image black;
    //public Animator anim;

    void Start() {
        skillPointsText = skillPointsTextGameObject.GetComponent<Text>();
        skillPoints = PlayerPrefs.GetInt("skillPoints");
        skillPointsText.text = "Skill Points: " + skillPoints.ToString();
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
        Transform selectedGameObjectTransform = EventSystem.current.currentSelectedGameObject.transform;
        string name = EventSystem.current.currentSelectedGameObject.name;
        if (name == "SkillTree_Button") {
            skillTree.SetActive(!skillTree.activeSelf);
            skill_IncreaseStartingHealth = PlayerPrefs.GetInt("skill_IncreaseStartingHealth");
            skill_Skill2 = PlayerPrefs.GetInt("skill_Skill2");
            skill_Skill3 = PlayerPrefs.GetInt("skill_Skill3");
            //if skill 1/2/3 selected
        }
        else if (name == "AddSkillLevel") {
            if (skillPoints > 0) {
                bool set = false;
                if (selectedGameObjectTransform.parent.name == "Skill_IncreaseStartingHealth" && skill_IncreaseStartingHealth < 3) {
                    skill_IncreaseStartingHealth += 1;
                    PlayerPrefs.SetInt("skill_IncreaseStartingHealth", skill_IncreaseStartingHealth);
                    set = true;
                }
                else if (selectedGameObjectTransform.parent.name == "Skill_Skill2" && skill_Skill2 < 3) {
                    skill_Skill2 += 1;
                    PlayerPrefs.SetInt("skill_Skill2", skill_Skill2);
                    set = true;
                }
                else if (selectedGameObjectTransform.parent.name == "Skill_Skill3" && skill_Skill3 < 3) {
                    skill_Skill3 += 1;
                    PlayerPrefs.SetInt("skill_Skill3", skill_Skill3);
                    set = true;
                }
                if (set) {
                    PlayerPrefs.SetInt("skillPoints", skillPoints);
                    skillPoints -= 1;
                    skillPointsText.text = "Skill Points: " + skillPoints.ToString();
                }
            }
        }
        else if (name == "DecreaseSkillLevel") {
            bool set = false;
            if (selectedGameObjectTransform.parent.name == "Skill_IncreaseStartingHealth" && skill_IncreaseStartingHealth > 0) {
                skill_IncreaseStartingHealth -= 1;
                PlayerPrefs.SetInt("skill_IncreaseStartingHealth", skill_IncreaseStartingHealth);
                set = true;
            }
            else if (selectedGameObjectTransform.parent.name == "Skill_Skill2" && skill_Skill2 > 0) {
                skill_Skill2 -= 1;
                PlayerPrefs.SetInt("skill_Skill2", skill_Skill2);
                set = true;
            }
            else if (selectedGameObjectTransform.parent.name == "Skill_Skill3" && skill_Skill3 > 0) {
                skill_Skill3 -= 1;
                PlayerPrefs.SetInt("skill_Skill3", skill_Skill3);
                set = true;
            }
            if (set) {
                PlayerPrefs.SetInt("skillPoints", skillPoints);
                skillPoints += 1;
                skillPointsText.text = "Skill Points: " + skillPoints.ToString();
            }
        }
        else if (name == "Back_Button") {
            StartCoroutine(ChangeScene("MainMenu"));
        }
        else if (name == "ClearSave_Button") {
            PlayerPrefs.DeleteAll();
            StartCoroutine(ChangeScene("StageSelection"));
        }
        else if (name == "UnlockAllStages_Button") {
            foreach (string stage in gameConstants.levelNames) {
                PlayerPrefs.SetInt("complete" + stage, 1);
            }
            StartCoroutine(ChangeScene("StageSelection"));
        }
        else if (name == "MaxSkillPoints_Button") {
            PlayerPrefs.SetInt("skillPoints", 9);
            StartCoroutine(ChangeScene("StageSelection"));
        }
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