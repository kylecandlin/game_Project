using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    public GameObject Player;
    public Level1Control Level1Control;
    public Vector3 respawnCoordinates;

    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            Debug.Log("collision");
            Player.transform.position = respawnCoordinates;
        }
    }

}
