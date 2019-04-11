using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour {

    public LayerMask Enemy;
    public Enemy enemyScript;
    public GameObject rightArmPlayer, playerObj, playerBody, player;
    public SpriteRenderer playerObj_s;
    public AudioSource attackSound;

    public ParticleSystem part, defensiveCastInstance, defensiveCast;
    public List<ParticleCollisionEvent> collisionEvents;
    bool damage, coolDownCompleted, mainCoolDown = true;
    public float nextCoolDown, coolDownTime;

    // Foreign Scripts and variables
    public Player_Stats Player_Stats;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        Player_Stats = player.GetComponent<Player_Stats>();
        attackSound.Play();
        attackSound.Pause();
        attackSound.volume = 0.2f;
        playerObj_s = playerBody.GetComponent<SpriteRenderer>();
        coolDownTime = 10;
        coolDownCompleted = true;
        collisionEvents = new List<ParticleCollisionEvent>();
        part = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update() {
        
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookAt = mouseScreenPosition;

        float AngleRad = Mathf.Atan2(lookAt.y - playerObj.transform.position.y, lookAt.x - playerObj.transform.position.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        Debug.Log("energy "+ Player_Stats.currentEnergy);
        if (Input.GetMouseButtonDown(0)) {
            mainCoolDown = true;
            Player_Stats.AlterStats(0,0,-10);
        }
        if (Input.GetMouseButton(0) && Player_Stats.currentEnergy > 0f && mainCoolDown == true)
        {
            Player_Stats.AlterStats(0,0,-1);
            attackSound.UnPause();
            if (AngleDeg <= 90 && AngleDeg >= -90)
            {
                playerBody.transform.rotation = Quaternion.Euler(0, 0, 0);
                rightArmPlayer.transform.rotation = Quaternion.Euler(0, 0, AngleDeg + 30);
            }
            else  {
                playerBody.transform.rotation = Quaternion.Euler(0, -180, 0);
                rightArmPlayer.transform.rotation = Quaternion.Euler(0, -180, -AngleDeg + 210);
            }
            part.Play();
        }
        else if (!Input.GetMouseButton(0) || Player_Stats.currentEnergy<=0f)
        {
            attackSound.Pause();
            part.Stop();
            mainCoolDown = false;
        }
        if (Input.GetMouseButton(1) && Time.time > nextCoolDown) {
            if (defensiveCastInstance) {
                defensiveCastInstance.Stop();
            }
            defensiveCastInstance = Instantiate(defensiveCast, playerObj.transform.position, Quaternion.identity);
            nextCoolDown = Time.time + 1;
        }       

    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == 9) {
            Debug.Log("Particle Collision with Enemy");
            enemyScript = other.GetComponent<Enemy>();
            damage = true;
            Damage();
        }    
    }

    void Damage() {              
            if (damage)
            {
                enemyScript.EnemyHealth -= 0.2f;
                damage = false;
            }     
    }

    IEnumerator CoolDown() {
        yield return new WaitForSeconds(10f);
        coolDownCompleted = true;
    }
}
