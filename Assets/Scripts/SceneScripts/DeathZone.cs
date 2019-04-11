using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    public GameObject Player, enemyObj;
    public Level1Control Level1Control;
    public Vector3 respawnCoordinates;
    public Enemy Enemy;

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
        if (other.gameObject.layer == 9) {
            Enemy = other.GetComponent<Enemy>();
            other.gameObject.transform.position = Enemy.startPos;
        }
    }

}
