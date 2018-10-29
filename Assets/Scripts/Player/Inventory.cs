using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public GameObject Tabs;
    public GameObject Panels;
    public GameObject InventoryTab;
    public GameObject PlayerTab;
    public GameObject InventoryPanel;
    public GameObject PlayerPanel;
    public GameObject InventoryBackground;
    private Camera cam;
    private bool displaying;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        Tabs.SetActive(false);
        Panels.SetActive(false);
        InventoryPanel.SetActive(true);
        PlayerPanel.SetActive(false);
        InventoryBackground.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        InventoryToggle();
	}

    // Shows the inventory interface
    void InventoryToggle() {
        if (Input.GetKeyDown("tab")) {
            displaying = !displaying;
            InventoryBackground.SetActive(displaying);
            Tabs.SetActive(displaying);
            Panels.SetActive(displaying);
        }
    }
  
    public void PanelSelect(int page) {
        switch (page) {
            case 0:
                InventoryPanel.SetActive(true);
                PlayerPanel.SetActive(false);
                break;
            case 1:
                InventoryPanel.SetActive(false);
                PlayerPanel.SetActive(true);
                break;
        }

    }
}
