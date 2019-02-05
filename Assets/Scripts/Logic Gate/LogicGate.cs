using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicGate : MonoBehaviour
{
    // Variables and GameObjects
    public GameObject Player, HotBarUI;    
    public HotBar HotBarScript;

    // Update is called once per frame
    void Update()
    {
        // Variables and GameObjects allocation
        Player = GameObject.Find("Player");
        HotBarUI = GameObject.FindGameObjectWithTag("HotBarUI");
        HotBarScript = HotBarUI.GetComponent<HotBar>();
    }

    // Collider detection
    void OnTriggerEnter2D(Collider2D other)
    {
        // when there is a collision with player 
        // send prefab name to HotBar script
        if (other.gameObject.tag == "Player")
        {           
            HotBarScript.HotBarUpdate(this.name);
            Destroy(gameObject);
        }
    }
   
}
