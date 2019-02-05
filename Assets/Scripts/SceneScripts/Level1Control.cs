using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Control : MonoBehaviour {

    public GameObject Player;
    public bool respawn = false;
    public Vector3 respawnCoordinates;

	// Use this for initialization
	void Start () {
        respawnCoordinates = new Vector3(-9.64321f, -2.859218f, 0);
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Respawn() {
        Player.transform.position = respawnCoordinates;
    }
}
