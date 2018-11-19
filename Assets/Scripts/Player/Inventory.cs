using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public GameObject Tabs;
    public GameObject Panels;
    public GameObject InventoryTab;
    public GameObject PlayerTab;
    public GameObject SettingsTab;
    public GameObject InventoryPanel;
    public GameObject PlayerPanel;
    public GameObject SettingsPanel;
    public GameObject InventoryBackground;
    public GameObject InventorySlotPrefab;
    public GameObject prefab;
    private Camera cam;
    private bool displaying;
    private RectTransform rect;
    public float x;
    public float y;

    public int totalGold;
    public Text textGold;

    public int inventorySize;
    private int maxCol;
    private int maxRow;

	// Use this for initialization
	void Start () {
        y = -30;
        x = 30;
        maxCol = 10;
        maxRow = 3;
        inventorySize = 30;
        cam = Camera.main;
        Tabs.SetActive(false);
        Panels.SetActive(false);
        InventoryPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        PlayerPanel.SetActive(false);
        InventoryBackground.SetActive(false);
        rect = InventoryBackground.GetComponent<RectTransform>();
        PopulateInventorySlots();
        totalGold = 0;
    }
	
	// Update is called once per frame
	void Update () {
        textGold.text = "Gold: " + totalGold.ToString();
        rect.sizeDelta = new Vector2(Screen.width,Screen.height);
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
                SettingsPanel.SetActive(false);
                break;
            case 1:
                InventoryPanel.SetActive(false);
                PlayerPanel.SetActive(true);
                SettingsPanel.SetActive(false);
                break;
            case 2:
                InventoryPanel.SetActive(false);
                PlayerPanel.SetActive(false);
                SettingsPanel.SetActive(true);
                break;
        }

    }

    public void PopulateInventorySlots() {
        for (int i = 0; i < maxCol; i++) {
            for (int j = 0; j < maxRow; j++) {
                
                prefab = Instantiate(InventorySlotPrefab, new Vector3((i * x * 2)+30,(j+1) * 2* y,0), Quaternion.identity) as GameObject;
                prefab.transform.SetParent (InventoryPanel.transform, false);
            }
        }
    }

    public void Quit() {
        Application.Quit();
    }

}
