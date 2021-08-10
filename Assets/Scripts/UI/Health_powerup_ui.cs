using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Health_powerup_ui : MonoBehaviour
{
    //public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite heartIcon;

    public CharacterConstants characterConstants;
    public UnityEvent onPlayerDeath;
    public IntVariable characterHealth;

    public IntVariable characterPowerUpHp;

    public Image[] poweruphp;

    private SpriteRenderer health_no_multiply;
    // Start is called before the first frame update
    void Start()
    {
        health_no_multiply = GetComponent<SpriteRenderer>();
        int healthIncrease = PlayerPrefs.GetInt("skill_IncreaseStartingHealth");
        characterHealth.SetValue(characterConstants.characterHealth + healthIncrease);

        UpdateHealth();
    }

    public void UpdateHealth()
    {
        if (characterHealth.Value == 0)
        {
            onPlayerDeath.Invoke();
        }
    }

    void Update()
    {
        if (characterHealth.Value <= 7)
            {
            for (int i = 0; i < hearts.Length; i++)
            {

                if (i < characterHealth.Value)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }
        }

        else
        {
            for (int i=7; i<poweruphp.Length+7; i++)
            {
                if (i < characterHealth.Value)
                {
                    poweruphp[i-7].enabled = true;
                }
                else
                {
                    poweruphp[i - 7].enabled = false;
                }
            }
        }

    }
}
