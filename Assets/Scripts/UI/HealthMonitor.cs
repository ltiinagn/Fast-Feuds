using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class HealthMonitor : MonoBehaviour
{
    public CharacterConstants characterConstants;
    public UnityEvent onPlayerDeath;
    public IntVariable characterHealth;
    public Text healthText;

    public void Start()
    {
        characterHealth.SetValue(characterConstants.characterHealth);
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthText.text = "Health: " + characterHealth.Value.ToString();
        if (characterHealth.Value == 0) {
            onPlayerDeath.Invoke();
        }
    }
}