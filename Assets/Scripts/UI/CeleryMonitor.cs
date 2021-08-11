using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class CeleryMonitor : MonoBehaviour
{
    public Image[] celeryArray;
    private int celeryCount;

    public void Start()
    {
        celeryCount = PlayerPrefs.GetInt("skill_Skill3");
        InitializeCelery();
    }

    void InitializeCelery() {
        for (int j = 0; j < celeryCount; j++)
        {
            Debug.Log(j);
            celeryArray[j].enabled = true;
        }
    }

    public void UpdateCelery()
    {

    }

    void Update()
    {
        UpdateCelery();
    }
}