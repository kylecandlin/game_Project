using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour {

    public Player_Basic_Move controlScript;
    public Player_Stats statsScript;
    public Inventory inventoryScript;
    public GameObject Player;
    public GameObject InventoryObj;

    void Start()
    {
  
    }

    void Update()
    {
        Player = GameObject.Find("Player");
        InventoryObj = GameObject.Find("InventoryMenu");
        inventoryScript = InventoryObj.GetComponent<Inventory>();
        controlScript = Player.GetComponent<Player_Basic_Move>();
        statsScript = Player.GetComponent<Player_Stats>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GoldAmount(25);
            Destroy(gameObject);            
        }
    }

    void GoldAmount(int amount) {
        inventoryScript.totalGold +=amount;
    }
}
