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

    public Image[] poweruphp;

    public GameObject Hp1;

    public GameObject Hp2;
    
    // Start is called before the first frame update
    void Start()
    {
        //health_no_multiply = GetComponent<SpriteRenderer>();
        
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
        if (characterHealth.Value == 0)
        {
            onPlayerDeath.Invoke();
        }
    }

    void Update()
    {
        if (characterHealth.Value <= 7)
        {
            

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
        }

        else
        {
            Hp2.SetActive(false);

            for (int i = 7; i < poweruphp.Length + 7; i++)
            {
                if (i <= characterHealth.Value)
                {
                    poweruphp[i - 7].enabled = true;
                }
                else
                {
                    poweruphp[i - 7].enabled = false;
                }
            }
        }

    }
}
