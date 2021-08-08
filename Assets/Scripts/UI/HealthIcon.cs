using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthIcon : MonoBehaviour
{
    //public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite heartIcon;

    public CharacterConstants characterConstants;
    public UnityEvent onPlayerDeath;
    public IntVariable characterHealth;
    public Text healthText;


    // Start is called before the first frame update
    void Start()
    {
        int healthIncrease = PlayerPrefs.GetInt("skill_IncreaseStartingHealth");
        characterHealth.SetValue(characterConstants.characterHealth + healthIncrease);
        
        UpdateHealth();
        /*
        if (characterHealth.Value > numOfHearts)
        {
            numOfHearts = characterHealth.Value;
        } 
        */
       
    }

    public void UpdateHealth()
    {
        healthText.text = "Health: " + characterHealth.Value.ToString();

        if (characterHealth.Value == 0)
        {
            onPlayerDeath.Invoke();
        }
    }
    void Update()
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
    // Update is called once per frame

}
