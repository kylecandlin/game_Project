using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player_Stats : MonoBehaviour
{
    // Variables
    public float currentHealth; public float maxHealth; public Slider healthBar; public Slider healthBarInventory; // Health attributes   
    public float currentEnergy; public float maxEnergy; public Slider energyBar; public Slider energyBarInventory;// Mana attributes 
    public float currentStamina; public float maxStamina; public Slider staminaBar; public Slider staminaBarInventory;// Stamina attributes  
    public float regenTime; public float tempTime; // Regeneration time 
    public bool godMode, playerDieBool;
    public AudioSource playerDie;

    // Scripts and foreign variables
    public Player_Basic_Move controlScript;
    public GameObject PlayerDetailsObj, InventoryObj;
    public PlayerDetails PlayerDetails;
    public Gold Gold;
    public Inventory Inventory;
    public Text textHealth, textStamina, textEnergy, username;

    // Database Variables
    private string inputUsername, inputPassword;
    private float inputHealth, inputStamina, inputMana;
    string playerInsert_Link = "http://part1-17.wbs.uni.worc.ac.uk/Companion/InsertUser.php",
    update_Link = "http://part1-17.wbs.uni.worc.ac.uk/Companion/updateStat.php";

    //Initialization
    void Start()
    {
        PlayerDetails = PlayerDetailsObj.GetComponent<PlayerDetails>();
        InventoryObj = GameObject.Find("InventoryMenu");
        Inventory = InventoryObj.GetComponent<Inventory>();
        regenTime = 0.05f; // time to regenerate stats (one increment)
        tempTime = regenTime;
        maxHealth = 100;
        maxEnergy = 80;
        maxStamina = 300;
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        currentStamina = maxStamina;
        godMode = false;
        playerDieBool = true;
        DisplayStats();
        playerDie = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Script setters and updaters
        controlScript = this.GetComponent<Player_Basic_Move>(); // Finding the script called Control   
        DisplayStats();

        if (currentHealth <= 0 && godMode == false && playerDieBool) {
            StartCoroutine(PlayerDeath());
        }
        inputHealth = currentHealth;
        inputStamina = currentStamina;
        InsertData();
        // update player details in menu
        textHealth.text = (int)currentHealth+"/" +maxHealth;
        textStamina.text = (int)currentStamina + "/" + maxStamina;
        textEnergy.text = (int)currentEnergy+"/"+maxEnergy;
        username.text = PlayerDetails.username;
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
        energyBar.value = energyBarInventory.value = currentEnergy / maxEnergy;
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
        if (currentEnergy + manaChange > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        if (currentEnergy + manaChange < 0)
        {
            currentEnergy = 0;
        }
        if (currentEnergy + manaChange <= maxEnergy && currentEnergy + manaChange >= 0)
        {
            currentEnergy += manaChange;
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

    public void InsertData()
    {
        WWWForm updateForm = new WWWForm();
        updateForm.AddField("usernamePOST", PlayerDetails.username);
        updateForm.AddField("healthPOST", inputHealth.ToString());
        updateForm.AddField("staminaPOST", inputStamina.ToString());
        WWW filePush2 = new WWW(update_Link, updateForm);
    }

    IEnumerator PlayerDeath() {
        playerDieBool = false;
        playerDie.PlayOneShot(playerDie.clip);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("1", LoadSceneMode.Single);
    }

    // upgrade max health stat
    public void HealthUpgrade() {        
        if (Inventory.totalGold >= 50) {
            Inventory.totalGold -= 50;
            if (maxHealth <= 140) {
                maxHealth += 10;
            }            
        }        
    }

    public void StaminaUpgrade() {
        if (Inventory.totalGold >= 25) {
            Inventory.totalGold -= 25;
            if (maxStamina <= 380) {
                maxStamina += 20;
            }
        }
    }
}