using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Basic_Move : MonoBehaviour
{

    // Foreign Scripts
    public Player_Stats Player_Stats;
    public float Player_Stats_CurrentStamina;

    public float moveSpeed;
    public float walkSpeed;
    public float runSpeedMultiplier;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public float jumpSpeed;
  
    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        moveSpeed = walkSpeed;
        jumpSpeed = 30;
        walkSpeed = 4f;
        runSpeedMultiplier = 2f;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void Update()
    {
        Player_Stats = this.GetComponent<Player_Stats>();
        Player_Stats_CurrentStamina = Player_Stats.currentStamina;
        Controls();
        Player_Stats.Regeneration(2, 10, 0, true);
        Walk();
    }

    void Walk()
    {
        float y = 0;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            y += jumpSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift) && Player_Stats_CurrentStamina > 0f)
        {
            Player_Stats.AlterStats(0, -1, 0);
            moveSpeed = walkSpeed * runSpeedMultiplier;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
     
        float moveHorizontal = Input.GetAxis("Horizontal");

    
        Vector2 movement = new Vector2(moveHorizontal * moveSpeed, y);

    
        rb2d.position += movement * Time.deltaTime;

    }
    void Controls()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Player_Stats.AlterStats(-10, 0, 0);
        }

    }
}