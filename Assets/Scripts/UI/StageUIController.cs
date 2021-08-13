using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class StageUIController : MonoBehaviour
{
    public GameConstants gameConstants;
    public GameObject bossHPContainer;
    public GameObject bossBlackBorderTop;
    public GameObject bossBlackBorderBottom;
    public GameObject ready;
    public GameObject FIGHT;
    public GameObject endGameOverlay;
    public GameObject pauseMenu;
    public GameObject stageCompleteMenu;
    public GameObject gameOverMenu;
    public GameObject skillReward;
    Text skillRewardText;
    public GameObject healthBarContainer;
    public GameObject celeryBarContainer;
    public GameObject playstyleContainer;
    public GameObject bossName_Text;
    public UnityEvent startNextSpawn;
    public UnityEvent startNextDialogue;
    string[] levelNames;
    string[] buttonPaths;
    int levelIndex;
    bool start;

    void Start() {
        skillRewardText = skillReward.GetComponent<Text>();
        levelNames = gameConstants.levelNames;
        start = true;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            Time.timeScale = pauseMenu.activeSelf ? 0.0f : 1.0f;
        }
    }

    public void showReadyFight() {
        StartCoroutine(showReadyFightCoroutine());
    }

    IEnumerator showReadyFightCoroutine() {
        yield return showHUDCoroutine();
        if (!SceneManager.GetActiveScene().name.Contains("0-T")) {
            ready.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            ready.SetActive(false);
            FIGHT.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            FIGHT.SetActive(false);
        }
        startNextSpawn.Invoke();
    }

    IEnumerator showHUDCoroutine() {
        if (start) {
            start = false;
            if (SceneManager.GetActiveScene().name.Contains("-B")) {
                bossBlackBorderBottom.SetActive(true);
                bossBlackBorderTop.SetActive(true);
                bossName_Text.SetActive(true);
            }
            if (SceneManager.GetActiveScene().name.Contains("1-B")) {
                bossHPContainer.SetActive(true);
                bossName_Text.transform.GetComponent<Text>().text = "Thicc Mike";
            }
            else if (SceneManager.GetActiveScene().name.Contains("2-B")) {
                bossName_Text.transform.GetComponent<Text>().text = "BuFF Cake";
                RectTransform bossName_TextRectTransform = bossName_Text.GetComponent<RectTransform>();
                bossName_TextRectTransform.anchoredPosition = new Vector2(bossName_TextRectTransform.anchoredPosition.x, bossName_TextRectTransform.anchoredPosition.y - 10.0f);
            }
            else if (SceneManager.GetActiveScene().name.Contains("3-B")) {
                bossName_Text.transform.GetComponent<Text>().text = "Die(T) Choke";
                RectTransform bossName_TextRectTransform = bossName_Text.GetComponent<RectTransform>();
                bossName_TextRectTransform.anchoredPosition = new Vector2(bossName_TextRectTransform.anchoredPosition.x, bossName_TextRectTransform.anchoredPosition.y - 10.0f);
            }
        }
        healthBarContainer.SetActive(true);
        celeryBarContainer.SetActive(true);
        playstyleContainer.SetActive(true);
        yield return new WaitForSeconds(1.0f);
    }

    public void hideHUD() {
        StartCoroutine(hideHUDCoroutine());
    }

    IEnumerator hideHUDCoroutine() {
        if (!SceneManager.GetActiveScene().name.Contains("0-T")) {
            healthBarContainer.SetActive(false);
            celeryBarContainer.SetActive(false);
            playstyleContainer.SetActive(false);
        }
        yield return new WaitForSeconds(1.0f);
        startNextDialogue.Invoke();
    }

    public void showGameOver() {
        Time.timeScale = 0.0f;
        gameOverMenu.SetActive(true);
    }

    public void showStageComplete() {
        StartCoroutine(showStageCompleteCoroutine());
    }

    IEnumerator showStageCompleteCoroutine() {
        Time.timeScale = 0.0f;
        if (SceneManager.GetActiveScene().name.Contains("3-B")) {
            endGameOverlay.SetActive(true);
            Transform panel = endGameOverlay.transform.Find("Panel");
            Image image = panel.GetComponent<Image>();
            for (int i = 0; i <= 255; i++) {
                image.color = new Color(1.0f,1.0f,1.0f,(float) i / 255.0f);
                yield return new WaitForSecondsRealtime(0.01f);
            }
            foreach (Transform child in panel) {
                child.gameObject.SetActive(true);
            }
            yield return new WaitForSecondsRealtime(3.0f);
        }
        levelIndex = System.Array.IndexOf(levelNames, SceneManager.GetActiveScene().name);
        bool completedBefore = PlayerPrefs.GetInt("complete"+levelNames[levelIndex]) == 1 ? true : false;
        if (!completedBefore) {
            PlayerPrefs.SetInt("complete"+levelNames[levelIndex], 1);
            int skillPoints = PlayerPrefs.GetInt("skillPoints");
            int reward = 0;
            string rewardText = "";
            if (levelIndex <= 3) {
                reward = 1;
                rewardText = "1 skill point gained!";
            }
            else if (levelIndex <= 5) {
                reward = 2;
                rewardText = "2 skill points gained!";
            }
            skillPoints += reward;
            PlayerPrefs.SetInt("skillPoints", skillPoints);
            PlayerPrefs.Save();
            skillRewardText.text = rewardText;
            skillReward.SetActive(true);
        }
        levelIndex += 1;
        stageCompleteMenu.SetActive(true);
        stageCompleteMenu.transform.Find("Panel/NextStage_Button").gameObject.SetActive(false);
        yield return null;
    }

    public void OnClicked()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        if (name == "Resume_Button") {
            transform.parent.Find("PauseMenu").gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else if (name == "Restart_Button") {
            StartCoroutine(ChangeScene(SceneManager.GetActiveScene().name));
        }
        else if (name == "StageSelection_Button") {
            StartCoroutine(ChangeScene("StageSelection"));
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