using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class HealthMonitor : MonoBehaviour
{
    public CharacterConstants characterConstants;
    public UnityEvent onPlayerDeath;
    public IntVariable characterHealth;
    public Text healthText;
    public Image heartMultiply;
    public Image[] hearts;
    private bool dead;

    public void Start()
    {
        dead = false;
        int healthIncrease = PlayerPrefs.GetInt("skill_IncreaseStartingHealth");
        characterHealth.SetValue(characterConstants.characterHealth * (1 + healthIncrease));
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        if (characterHealth.Value <= 5)
        {
            heartMultiply.enabled = false;
            for (int j = 0; j < hearts.Length; j++)
            {
                if (j < characterHealth.Value)
                {
                    hearts[j].enabled = true;
                }
                else
                {
                    hearts[j].enabled = false;
                }
            }
            healthText.text = "";
        }
        else if (characterHealth.Value > 5) {
            heartMultiply.enabled = true;
            healthText.text = "x" + characterHealth.Value.ToString();
            for (int j = 0; j < hearts.Length; j++)
            {
                hearts[j].enabled = false;
            }
        }
        if (!dead && characterHealth.Value == 0) {
            dead = true;
            onPlayerDeath.Invoke();
        }
    }

    void Update()
    {
        UpdateHealth();
    }
}