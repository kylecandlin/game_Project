using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public Transform selectedItem, selectedSlot, orginalSlot;
    public bool canGrab = false, selected = false;

    public GameObject Tabs, InventoryTab, PlayerTab, SettingsTab;
    public GameObject Panels, InventoryPanel, PlayerPanel, SettingsPanel;
    public GameObject InventoryBackground;
    public GameObject InventorySlotPrefab;
    public GameObject[] slotLocation;
    
    // Parent slots
    public GameObject SlotParent;
    public Transform SlotParentTrans;
    private InventorySlot[] slots;

    private GameObject go;
    public GameObject item1, item2, item3, currentItem;

    private Camera cam;
    private bool displaying;
    private RectTransform rect;
    public float x, y;

    public int totalGold;
    public Text textGold;

    public int inventorySize;
    private int maxCol, maxRow;

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
        currentItem = null;
        // give player 2 health potions
        item2 = Instantiate(item1, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        item3 = Instantiate(item1, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        item2.transform.SetParent(slotLocation[0].transform, false);
        item3.transform.SetParent(slotLocation[1].transform, false);
        item2.name = "item2";
        item3.name = "item3";
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) && selectedItem != null)
        {
            selectedItem.position = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0) && selectedItem != null) {
            selectedItem.localPosition = Vector3.zero;
        }


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

    int k =0;

    public void PopulateInventorySlots() {
        int slotName = 0;
        slotLocation = new GameObject[inventorySize];
        for (int i = 0; i < maxCol; i++) {
            for (int j = 0; j < maxRow; j++) {
                go = Instantiate(InventorySlotPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                go.transform.SetParent (SlotParent.transform, false);
                slotName++;
                go.name = slotName.ToString();
                slotLocation[k] = go;
                k++;   
                
            }
        }
       

    }

    public void SlotCalled(int position) {
        currentItem.transform.SetParent(slotLocation[position].transform, false);
    }

    // quits application
    public void Quit() {
        Application.Quit();
    }


   

}
