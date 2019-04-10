using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LayerMask groundLayer; // Ground interaction layer for player

    // Raycast angles for detecting boundaries
    public float distance = 1.5f, RightLowAngle = 290.0f, LeftLowAngle = 250.0f;

    // Foreign Scripts and Variables
    public GameObject Player;
    public Transform targetpos;
    public Player_Stats statsScript;

    // Enemy Stats
    public float speed, attackDis, EnemyHealth;
    public GameObject[] itemDrops;
    public bool freezeMotion;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
        statsScript = Player.GetComponent<Player_Stats>();
        targetpos = Player.GetComponent<Transform>();
        speed = 3;
        attackDis = 6f;
        EnemyHealth = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (freezeMotion == false) {
            FSM();
        }        
        IsDead();
    }
    void OnTriggerStay2D(Collider2D other) // detects whent the player and the enemy collide
    {
        if (other.gameObject.tag == "Player")
        {
            statsScript.AlterStats((-50 * Time.deltaTime), 0, 0); // if they collide take damage over time
        }
    }

    // Enemy Finite State Machine
    private void FSM()
    {
        if (Vector3.Distance(transform.position, targetpos.position) < attackDis) // if player is within enemy attack range
        {
            Attack();
        }
        else
        {
            Idle();
        }
    }

    void Idle()
    { // wanders until hits wall/runs out of floor, then changes direction
        transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        DetectBoundary();
    }

    void Attack()
    { // enemy follows player when in range
        DetectBoundary();
        EnemyMove();
    }

    void IsDead()
    { // determines what the enemy does once health reaches 0
        if (EnemyHealth <= 0)
        {
            // Drops items in array on death
            for (int i = 0; i < itemDrops.Length; i++)
            {
                Instantiate(itemDrops[i], transform.position, Quaternion.identity);
            }
          
            Destroy(gameObject); // removes enemy object
        }
    }

    bool IsFloorRight() // detects whether there is floor to the right of the enemy
    {
        Vector2 position = transform.position; // Origin of Raycast
        Vector2 direction = GetDirectionVector2D(RightLowAngle); // Direction of Raycast
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer); // Cast Raycast
        Debug.DrawRay(position, direction * distance, Color.cyan);
        if (hit.collider != null)
        { // If hit is detected from Raycast        
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsFloorLeft() // detects whether there is floor to the left of the enemy
    {
        Vector2 position = transform.position; // Origin of Raycast
        Vector2 direction = GetDirectionVector2D(LeftLowAngle); // Direction of Raycast
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer); // Cast Raycast
        Debug.DrawRay(position, direction * distance, Color.cyan);
        if (hit.collider != null) // If hit is detected from Raycast   
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool DetectWall() // detects whether a wall is infront of the enemy
    {
        Vector2 position = transform.position; // Origin of Raycast
        RaycastHit2D hitWallR = Physics2D.Raycast(position, new Vector2(1, 0), distance, groundLayer); // Cast Raycast
        Debug.DrawRay(position, new Vector2(1, 0) * distance, Color.red);
        RaycastHit2D hitWallL = Physics2D.Raycast(position, new Vector2(-1, 0), distance, groundLayer); // Cast Raycast
        Debug.DrawRay(position, new Vector2(-1, 0) * distance, Color.red);
        if (hitWallR.collider != null || hitWallL.collider != null) // If hit is detected from Raycast 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2 GetDirectionVector2D(float angle) // converts angle in degrees into radians
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    public void EnemyMove() // Makes enemy move in a specified direction
    {
        ChangeDirection();
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    public void ChangeDirection() // Detects which direction to face when locked onto the player
    {
        if (transform.position.x >= targetpos.position.x + 1) // if player is to right of enemy, face right
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else if (transform.position.x < targetpos.position.x - 1) // if player is to left of enemy, face left
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void DetectBoundary() // Detects when to change direction, depending on floor space and walls
    {
        if (!IsFloorLeft() || DetectWall()) // detects floor to the left and wall
        {
            transform.Rotate(0, -90, 0); // rotates the enemy 180 degrees and moves away from collision points
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        else if (!IsFloorRight() || DetectWall()) // detects floor to the right and wall
        {
            transform.Rotate(0, -90, 0); // rotates the enemy 180 degrees and moves away from collision points
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }
}