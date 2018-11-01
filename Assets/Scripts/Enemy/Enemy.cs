using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Player_Basic_Move controlScript;
    public Player_Stats statsScript;
    public GameObject Player;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        controlScript = Player.GetComponent<Player_Basic_Move>();
        statsScript = Player.GetComponent<Player_Stats>();   
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            statsScript.AlterStats(-10, 0, 0);
        }
    }
}
