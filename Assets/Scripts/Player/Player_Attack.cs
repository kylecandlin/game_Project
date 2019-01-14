using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour {

    public string hitb;
    public LayerMask Enemy;
    public Enemy enemyScript;
    public GameObject enemyObj;

    // Use this for initialization
    void Start () {
        enemyObj = GameObject.Find("Enemy");
        enemyScript = enemyObj.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update() {
        attack();
    }

    public void attack()
    {
        float distance = 2f; // Length of Raycast
        Vector2 position = transform.position; // Origin of Raycast
        Vector2 direction = new Vector2(1, 0); // Direction of Raycast
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, Enemy); // Cast Raycast
        if (hit.collider != null) // If hit is detected from Raycast
        {
            if (Input.GetMouseButtonDown(0))
            {
                enemyScript.EnemyHealth -= 5;
             }
            hitb = "detected";
            return;
        }
        hitb = "false";
        return;
    }
}
