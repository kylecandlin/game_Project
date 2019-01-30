using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {

    public Player_Basic_Move Player_Basic_Move;
    public GameObject Player;
    public float interactionRadius;
    private bool displaying;
    public bool canMove; // sets if the player can move during interaction

    public GameObject test;
    SpriteRenderer testSprite;

    // Components
    Rigidbody2D rb2d;
    CircleCollider2D cc2d;
    SpriteRenderer sprite;

    // Use this for initialization
    void Start () {
        testSprite = test.GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");
        rb2d = Player.GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.enabled = false;
        cc2d.radius = interactionRadius;
        testSprite.enabled = false;

        foreach (Renderer r in test.GetComponentsInChildren<Renderer>())
            r.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {        
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            Debug.Log("enter");
            sprite.enabled = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            
            sprite.transform.position = (new Vector2(rb2d.position.x + 1, rb2d.position.y +2));
            if (Input.GetKeyDown(KeyCode.E)) {           
                displaying = !displaying;
                testSprite.enabled = displaying;
                Debug.Log("displaying" + displaying);
                foreach (Renderer r in test.GetComponentsInChildren<Renderer>())
                    r.enabled = displaying;
            }
            if (canMove == false && displaying == true) {
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;                
            }
            else if (displaying == false) {
                rb2d.constraints = RigidbodyConstraints2D.None;
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
                
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            sprite.enabled = false;
        }
    }
}
