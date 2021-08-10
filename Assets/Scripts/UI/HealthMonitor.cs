using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class HealthMonitor : MonoBehaviour
{
    public CharacterConstants characterConstants;
    public UnityEvent onPlayerDeath;
    public IntVariable characterHealth;
    public Text healthText;
    public GameObject Hp1;

    public void Start()
    {
        int healthIncrease = PlayerPrefs.GetInt("skill_IncreaseStartingHealth");
        characterHealth.SetValue(characterConstants.characterHealth + healthIncrease);
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        if (characterHealth.Value <= 7)
        {
            Hp1.SetActive(true);
        }

        healthText.text = "~" + characterHealth.Value.ToString();
        if (characterHealth.Value == 0) {
            onPlayerDeath.Invoke();
        }
    }

    void Update()
    {
        UpdateHealth();
    }
}