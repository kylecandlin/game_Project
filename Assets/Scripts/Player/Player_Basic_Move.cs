using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Basic_Move : MonoBehaviour
{

    // Foreign Scripts
    public Player_Stats Player_Stats;
    public float Player_Stats_CurrentStamina;

    public GameObject Gold;

    public float walkSpeed; // The default value for walking
    public float runSpeedMultiplier; // The value used to multiply walk speed by 
    public float moveSpeed; // The calculated output for speed of movement
    public float jumpForce; // The Force of the jump
    float y;
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
        jumpForce = 5;
    }

    void Update()
    {
        // Update Foreign Scripts and Variables
        Player_Stats = this.GetComponent<Player_Stats>();
        Player_Stats_CurrentStamina = Player_Stats.currentStamina;

        MainCamera.orthographicSize = cameraZoom;
        Player_Stats.Regeneration(0.2f, 1, 1, true); // Base regeration

        SlopeAngle();
        Walk();
        Jump();
        OtherControls();
    }

    // Applies Walk motions
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
        Vector2 movement_y = new Vector2(0, y);

        rb2d.position += movement_x * Time.deltaTime;
        rb2d.AddForce(movement_y, ForceMode2D.Impulse);
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
        if (hit.collider != null) // If hit is detected from Raycast
        {
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
        if (h > 1) // Detect when more than one Raycast hits
        {
            angle = Mathf.Abs(Mathf.Atan2(hits[1].normal.x, hits[1].normal.y) * Mathf.Rad2Deg); // Calculate angle            
        }
        return angle; // Return Angle float value
    }

    // Other player controls
    void OtherControls()
    {
        // List of other Controls
        if (Input.GetKeyDown(KeyCode.X))
        {
            Player_Stats.AlterStats(-10, 0, -10);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(Gold, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

}