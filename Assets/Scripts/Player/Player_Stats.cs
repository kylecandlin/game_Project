using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_Stats : MonoBehaviour
{
    // Variables
    public float currentHealth; public float maxHealth; public Slider healthBar; // Health attributes  
    public float currentMana; public float maxMana; public Slider manaBar;// Mana attributes 
    public float currentStamina; public float maxStamina; public Slider staminaBar;// Stamina attributes  
    public float regenTime; public float tempTime; // Regeneration time 

    // Scripts and foreign variables
    public Player_Basic_Move controlScript;

    //Initialization
    void Start()
    {
        regenTime = 1; // time to regenerate stats (one increment)
        tempTime = regenTime;
        maxHealth = 80;
        maxMana = 80;
        maxStamina = 300;
        currentHealth = maxHealth;
        currentMana = maxMana;
        currentStamina = maxStamina;
        DisplayStats();
    }

    // Update is called once per frame
    void Update()
    {
        // Script setters and updaters
        controlScript = this.GetComponent<Player_Basic_Move>(); // Finding the script called Control   
        DisplayStats();

    }

    public void Regeneration(float healthRegen, float staminaRegen, float manaRegen, bool active)
    {
        if (active)
        {
            if ((tempTime -= Time.deltaTime) <= 0)
            {
                AlterStats(healthRegen, staminaRegen, manaRegen);
                tempTime = regenTime;
            }
        }
    }

    void DisplayStats()
    {
        healthBar.value = currentHealth / maxHealth;
        manaBar.value = currentMana / maxMana;
        staminaBar.value = currentStamina / maxStamina;
    }

    public void AlterStats(float healthChange, float staminaChange, float manaChange)
    {
        // Health change
        if (currentHealth + healthChange > maxHealth)
        { // allows current stat to be set to max value if the current stat ends in an odd number
            currentHealth = maxHealth;
        }
        if (currentHealth + healthChange < 0)
        {  // allows current stat to be set to zero if the current stat ends in an odd number
            currentHealth = 0;
        }
        if (currentHealth + healthChange <= maxHealth && currentHealth + healthChange >= 0)
        { //makes sure current stat plus change stat cant go beyond max stat or below zero
            currentHealth += healthChange; // applies change value to the current stat
        }

        // Mana change 
        if (currentMana + manaChange > maxMana)
        {
            currentMana = maxMana;
        }
        if (currentMana + manaChange < 0)
        {
            currentMana = 0;
        }
        if (currentMana + manaChange <= maxMana && currentMana + manaChange >= 0)
        {
            currentMana += manaChange;
        }

        // Stamina change 
        if (currentStamina + staminaChange > maxStamina)
        {
            currentStamina = maxStamina;
        }
        if (currentStamina + staminaChange < 0)
        {
            currentStamina = 0;
        }
        if (currentStamina + staminaChange <= maxStamina && currentStamina + staminaChange >= 0)
        {
            currentStamina += staminaChange;
        }


    }


}