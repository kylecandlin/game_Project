using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public Transform inventoryTransform;
    public Inventory Inventory;

    void OnMouseEnter()
    {
        inventoryTransform.GetComponent<Inventory>().selectedSlot = this.transform;

    }
    void OnMouseExit()
    {
     
    }
}
