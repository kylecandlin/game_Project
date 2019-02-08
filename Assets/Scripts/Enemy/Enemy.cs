using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Foreign Scripts and Variables
    public GameObject Player;
    public Transform targetpos;
    public Player_Stats statsScript;
    public Transform statBar;

    // Enemy Stats
    public float speed, attackDis;
    public int EnemyHealth;
    public GameObject[] itemDrops;    

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
        statsScript = Player.GetComponent<Player_Stats>();
        targetpos = Player.GetComponent<Transform>();
        speed = 2;
        attackDis = 6f;
        EnemyHealth = 50;       
    }

    // Update is called once per frame
    void Update()
    {
        FSM();
        IsDead();
        if (Input.GetKey(KeyCode.G)) {
            EnemyHealth -= 20;
        }
        statBar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y+2, 0));        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            statsScript.AlterStats(-10,0,0);
        }
    }

    // Enemy Finite State Machine
    private void FSM()
    {
        if (Vector3.Distance(transform.position, targetpos.position) < attackDis)
        {
            Attack();            
        }
        else{
            Wander();
        }
    }

    void Wander() {
        var Wanderange = 10;
        var xPos = transform.position.x;
        if (xPos >= xPos - Wanderange)
        {
            transform.LookAt(new Vector3(0, 0, 0));
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        else if (xPos <= xPos + Wanderange) {
            transform.LookAt(new Vector3(0, 0, 0));
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
    }

    void Attack() {
        transform.LookAt(targetpos.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    void IsDead() {
        if (EnemyHealth <= 0) { 
            // Drops items in array on death
            for (int i = 0; i < itemDrops.Length; i ++) {
                Instantiate(itemDrops[i], new Vector3(0,0,0), Quaternion.identity);
            }
            Destroy(gameObject); // removes enemy object
        }
    }

    
}
