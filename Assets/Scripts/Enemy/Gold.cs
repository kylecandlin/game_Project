using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour {
    // Foreign Scripts and Variables
    public Player_Basic_Move controlScript;
    public Player_Stats statsScript;
    public Inventory inventoryScript;
    public GameObject Player, InventoryObj;

    void Update()
    {
        // Foreign Scripts and Variables update
        Player = GameObject.Find("Player");
        InventoryObj = GameObject.Find("InventoryMenu");
        inventoryScript = InventoryObj.GetComponent<Inventory>();
        controlScript = Player.GetComponent<Player_Basic_Move>();
        statsScript = Player.GetComponent<Player_Stats>();
    }

    // Collider detection
    void OnTriggerEnter2D(Collider2D other)
    {
        // when there is a collision with player 
        // give player gold and remove coin
        if (other.gameObject.tag == "Player")
        {
            GoldAmount(25);
            Destroy(gameObject);            
        }
    }

    // function for giving player gold
    void GoldAmount(int amount) {
        inventoryScript.totalGold +=amount;
    }
}
