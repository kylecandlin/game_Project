using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Transform inventoryTransform;
    public Inventory Inventory;
    public GameObject[] slot_Location;
    public GameObject inventoryMenu, useBtnPrefab, useBtn;
    public GameObject test;
    public Button button;
    private bool child = false;
    private GameObject itemlocal;

    public Player_Stats PlayerStats;
    public GameObject Player;

    void OnMouseEnter()
    {
        inventoryTransform.GetComponent<Inventory>().selectedSlot = this.transform;

    }
    void OnMouseExit()
    {
        inventoryTransform.GetComponent<Inventory>().selectedSlot = null;
    }

    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");
        PlayerStats = Player.GetComponent<Player_Stats>();
        inventoryMenu = GameObject.FindGameObjectWithTag("InventoryMenu");
        Inventory = inventoryMenu.GetComponent<Inventory>();
        child = false;
        button.onClick.AddListener(ButtonClicked);   
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.childCount > 0)
        {
            child = true;
        }
        else {
            child = false;
            button.GetComponent<Image>().color = button.colors.normalColor;
            Destroy(useBtn);
        }            
    }

    void ButtonClicked() {
        Debug.Log("pressed"+ Inventory.currentItem);
        if (Inventory.currentItem != null && !child) {
            Inventory.SlotCalled(int.Parse(this.name) - 1);
        }
        if (child && Inventory.selected == false)
        {
            Inventory.selected = true;
            button.GetComponent<Image>().color = Color.blue;
            Debug.Log("child status: " + child);
            Inventory.currentItem = this.transform.GetChild(0).gameObject;
            Debug.Log("slot  " + Inventory.currentItem);
            if (useBtn == null) {
                useBtn = Instantiate(useBtnPrefab, this.transform.position, Quaternion.identity) as GameObject;
                useBtn.transform.SetParent(Inventory.InventoryPanel.transform, false);
                useBtn.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 15, 0);
            }            
        }
        else {
            Inventory.currentItem = null;
            Inventory.selected = false;
        }
    }

    // on use button pressed
    public void UseButtonClicked() {
        // recall foreign scripts for reference
        inventoryMenu = GameObject.FindGameObjectWithTag("InventoryMenu");
        Inventory = inventoryMenu.GetComponent<Inventory>();
        Player = GameObject.Find("Player");
        PlayerStats = Player.GetComponent<Player_Stats>();
        PlayerStats.AlterStats(PlayerStats.maxHealth,0,0); // health poition alterations
        Destroy(Inventory.currentItem.transform.gameObject); // removes item once used
    }
}
