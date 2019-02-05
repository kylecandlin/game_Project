using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Transform inventoryTransform;
    public Inventory Inventory;

    void OnMouseEnter()
    {
        inventoryTransform.GetComponent<Inventory>().selectedSlot = this.transform;

    }
    void OnMouseExit()
    {
        inventoryTransform.GetComponent<Inventory>().selectedSlot = null;
    }


    public Inventory inventoryScript;
    public GameObject[] slot_Location;
    public GameObject inventoryMenu;
    private GameObject currentItem;
    public GameObject test;
    public Button button;
    private bool child = false;
    private GameObject item2;

    // Use this for initialization
    void Start () {
        child = false;
        button.onClick.AddListener(ButtonClicked);     

    }
	
	// Update is called once per frame
	void Update () {
        item2 = Instantiate(test, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        item2.transform.SetParent(inventoryScript.slotLocation[2].transform, false);
        if (transform.childCount > 0)
        {
            child = true;
            currentItem = transform.GetChild(0).gameObject;
            // Debug.Log("child: " + currentItem + "  child count: "+transform.childCount);
        }
        else {
            child = false;
          //  Debug.Log("slot empty:  " + transform.childCount);
        }            
    }

    void ButtonClicked() {
        Debug.Log("pressed");
        if (child) {
            Debug.Log("child status: " + child);
            
            //Destroy(currentItem);
            //currentItem = Inventory.slotLocation[1];

            
        }        
    } 
        

}
