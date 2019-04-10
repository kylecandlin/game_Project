using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Basic_Move : MonoBehaviour
{
    // Foreign Scripts and Variables
    public Player_Stats Player_Stats;
    public float Player_Stats_CurrentStamina;

    public GameObject Gold;

    // Respectively: Value for walking, run multiplier for walkspeed, 
    // speed of movement, jump force, y update component
    public float walkSpeed, runSpeedMultiplier, moveSpeed, jumpForce, y; 
    public bool canDoubleJump; // Boolean defining whether the player is allowed a second jump 
    public string grounded; // String value to identify when the player is grounded 
    private float angle; // The Angle of the slope beneath the player

    public LayerMask groundLayer; // Ground interaction layer for player

    Rigidbody2D rb2d; // Players 2D Rigid Body
    Camera MainCamera;
    [Range(0f, 30f)]
    public float cameraZoom;

    void Start()
    {
        // Assign Initial values and parameters  
        rb2d = GetComponent<Rigidbody2D>(); // Assign 2D Rigid Body
        MainCamera = Camera.main;
        MainCamera.enabled = true;
        cameraZoom = 7;
        walkSpeed = 4f;
        runSpeedMultiplier = 2f;
        moveSpeed = walkSpeed;
        jumpForce = 7;
    }

    void Update()
    {
        // Update Foreign Scripts and Variables
        Player_Stats = this.GetComponent<Player_Stats>();
        Player_Stats_CurrentStamina = Player_Stats.currentStamina;

        MainCamera.orthographicSize = cameraZoom; // sets camera zoom
        Player_Stats.Regeneration(0.2f, 1, 1, true); // Base regeration

        SlopeAngle();
        Walk();
        Jump();
    }

    // Applies Walk motions
    void Walk()
    {
        // allows the player to sprint if they have enough stamina
        if (Input.GetKey(KeyCode.LeftShift) && Player_Stats_CurrentStamina > 0f) {
            Player_Stats.AlterStats(0, -1, 0);
            moveSpeed = walkSpeed * runSpeedMultiplier;
        }
        else {
            moveSpeed = walkSpeed; // if the player isnt sprinting set to walkspeed
        }

        float moveHorizontal = Input.GetAxis("Horizontal"); // recieve input
        Vector2 movement_x = new Vector2(moveHorizontal * moveSpeed, 0); // create movement vector x
        Vector2 movement_y = new Vector2(0, y); // create movement vector y
        rb2d.position += movement_x * Time.deltaTime; // Horizontal player movement application
        rb2d.AddForce(movement_y, ForceMode2D.Impulse); // Vertical player movement application
    }

    // Applies Jump motions
    void Jump() {
        y = 0; // Resets y component
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded()) // First Jump
            {
                rb2d.velocity = new Vector2(0, 0); // Resets players x velocity
                y += jumpForce; // Applies Jump force to the y component 
                canDoubleJump = true;
            }

            else if (canDoubleJump) // Airbourne Jump
            {
                canDoubleJump = false;
                rb2d.velocity = new Vector2(0, 0);
                y += jumpForce;
            }
        }
    }

    // Identifies if the player is Grounded
    bool IsGrounded()
    {
        float distance = 1.7f; // Length of Raycast
        Vector2 position = transform.position; // Origin of Raycast
        Vector2 direction = new Vector2((0), (-1)); // Direction of Raycast
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer); // Cast Raycast
        if (hit.collider != null) { // If hit is detected from Raycast        
            grounded = "Grounded";
            return true; // Player on ground
        }
        grounded = "Not Grounded";
        return false; // Player not on ground
    }

        // Calculates the angle of the slope beneath the player
    float SlopeAngle()
    {
        RaycastHit2D[] hits = new RaycastHit2D[2]; // Stores Raycast hits
        int h = Physics2D.RaycastNonAlloc(transform.position, -Vector2.up, hits); // Cast Raycast downwards
        if (h > 1) { // Detect when more than one Raycast hits
            angle = Mathf.Abs(Mathf.Atan2(hits[1].normal.x, hits[1].normal.y) * Mathf.Rad2Deg); // Calculate angle            
        }
        return angle; // Return Angle float value
    }

}