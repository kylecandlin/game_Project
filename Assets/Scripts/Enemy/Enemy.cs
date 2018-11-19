using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject Player;
    public Transform targetpos;
    public float speed;
    public float attackDis;
    public Player_Stats statsScript;
    public Transform statBar;

    private int EnemyHealth;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
        statsScript = Player.GetComponent<Player_Stats>();
        speed = 2;
        attackDis = 6f;
        EnemyHealth = 50;
       
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        IsDead();
        if (Input.GetKey(KeyCode.G)) {
            EnemyHealth -= 20;
        }
        statBar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y+2, 0));        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            statsScript.AlterStats(-10,0,0);
        }
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, targetpos.position) < attackDis)
        {
            transform.LookAt(targetpos.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }

    void IsDead() {
        if (EnemyHealth <= 0) {
            Destroy(gameObject);
        }
    }

    
}
