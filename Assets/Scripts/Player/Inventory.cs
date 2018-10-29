using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public GameObject InventoryPanel;
    private bool displaying;

	// Use this for initialization
	void Start () {
        InventoryPanel.SetActive(false);
}
	
	// Update is called once per frame
	void Update () {
        InventoryShow();
	}

    // Shows the inventory interface
    void InventoryShow() {
        if (Input.GetKeyDown("tab")) {
            displaying = !displaying;
            InventoryPanel.SetActive(displaying);
        }
    }
}
