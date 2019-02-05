using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {

    public GameObject Player;
    public float interactionRadius; // circle radius of interaction zone
    public bool displaying; // If target being displayed
    public bool canMove; // sets if the player can move during interaction

    // GameObject the will be rendered on interaction
    public GameObject toggleTarget;
    SpriteRenderer targetSprite;

    // Components
    Rigidbody2D rb2d;
    CircleCollider2D cc2d;
    SpriteRenderer sprite;

    // Use this for initialization
    void Start () {
        targetSprite = toggleTarget.GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");
        rb2d = Player.GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.enabled = false;
        cc2d.radius = interactionRadius;
        targetSprite.enabled = false;

        foreach (Renderer r in toggleTarget.GetComponentsInChildren<Renderer>())
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
                targetSprite.enabled = displaying;
                Debug.Log("displaying " + displaying);
                foreach (Renderer r in toggleTarget.GetComponentsInChildren<Renderer>())
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
