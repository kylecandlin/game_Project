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
    public float jumpForce;
    float y;
    public bool canDoubleJump;

    public LayerMask groundLayer;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
  
    // Use this for initialization
    void Start()
    {
        
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        moveSpeed = walkSpeed;
        jumpForce = 300;
        walkSpeed = 4f;
        runSpeedMultiplier = 2f;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void Update()
    {
        // Update Foreign Scripts and Variables
        Player_Stats = this.GetComponent<Player_Stats>();
        Player_Stats_CurrentStamina = Player_Stats.currentStamina;

        Controls();
        Player_Stats.Regeneration(2, 10, 0, true);
        Walk();
        Jump();
    }
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.4f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    void Walk()
    {
       
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

    
        Vector2 movement_x = new Vector2(moveHorizontal * moveSpeed, 0);
        Vector2 movement_y = new Vector2(0,y);

    
        rb2d.position += movement_x * Time.deltaTime;
        rb2d.AddForce(movement_y);

    }

    void Jump() {
        y = 0;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                y += jumpForce;
                canDoubleJump = true;
                
            }
            else {
                if (canDoubleJump) {
                   
                    canDoubleJump = false;
                    y += jumpForce;
                    
                }
            }
            
            
            

        }

    }

    void Controls()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Player_Stats.AlterStats(-10, 0, 0);
        }

    }

    
}