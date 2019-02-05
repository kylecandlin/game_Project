using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControl : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    private GameObject enemyInstance;
    public Rigidbody2D enemyRb2d;

	// Use this for initialization
	void Start () {
        Instantiate(playerPrefab, new Vector3(-10, 0, 0), Quaternion.identity);
        enemyInstance = Instantiate(enemyPrefab, new Vector3(85,-3,0), Quaternion.identity);
        enemyRb2d = enemyInstance.GetComponent<Rigidbody2D>();
        enemyRb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
