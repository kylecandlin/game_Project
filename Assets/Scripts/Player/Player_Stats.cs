using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_Stats : MonoBehaviour
{
    // Variables
    public float currentHealth; public float maxHealth; public Slider healthBar; public Slider healthBarInventory; // Health attributes   
    public float currentMana; public float maxMana; public Slider manaBar; public Slider manaBarInventory;// Mana attributes 
    public float currentStamina; public float maxStamina; public Slider staminaBar; public Slider staminaBarInventory;// Stamina attributes  
    public float regenTime; public float tempTime; // Regeneration time 

    // Scripts and foreign variables
    public Player_Basic_Move controlScript;

    public string inputUsername; public string inputPassword; public int inputHealth; public int inputStamina; public int inputMana; public int inputMaxHealth; public int inputMaxStamina; public int inputMaxMana;
    string playerInsert_Link = "http://part1-17.wbs.uni.worc.ac.uk/ProjectApp/InsertUser.php";

    //Initialization
    void Start()
    {
        regenTime = 0.05f; // time to regenerate stats (one increment)
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            InsertData(inputUsername, inputPassword, inputHealth, inputStamina, inputMana, inputMaxHealth, inputMaxStamina, inputMaxMana);
        }

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
        healthBar.value = healthBarInventory.value = currentHealth / maxHealth;
        manaBar.value = manaBarInventory.value = currentMana / maxMana;
        staminaBar.value = staminaBarInventory.value = currentStamina / maxStamina;
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


    public void InsertData(string playerUsername, string playerPassword, int testPlayerHealth, int testPlayerStamina, int testPlayerMana, int testPlayerMaxHealth, int testPlayerMaxStamina, int testPlayerMaxMana)
    {
        WWWForm insertForm = new WWWForm();
        insertForm.AddField("usernamePOST", playerUsername);
        insertForm.AddField("passwordPOST", playerPassword);
        insertForm.AddField("healthPOST", testPlayerHealth);
        insertForm.AddField("staminaPOST", testPlayerStamina);
        insertForm.AddField("manaPOST", testPlayerMana);
        insertForm.AddField("maxHealthPOST", testPlayerMaxHealth);
        insertForm.AddField("maxStaminaPOST", testPlayerMaxStamina);
        insertForm.AddField("maxManaPOST", testPlayerMaxMana);

        WWW filePush = new WWW(playerInsert_Link, insertForm);


    }


}