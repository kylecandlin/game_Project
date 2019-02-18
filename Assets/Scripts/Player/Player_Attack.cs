using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour {

    public LayerMask Enemy;
    public Enemy enemyScript;
    public GameObject rightArmPlayer, playerObj, playerBody;
    public SpriteRenderer playerObj_s;
    public AudioSource attackSound;

    public ParticleSystem part, defensiveCastInstance, defensiveCast;
    public List<ParticleCollisionEvent> collisionEvents;
    bool damage, coolDownCompleted;
    public float nextCoolDown, coolDownTime;

    // Use this for initialization
    void Start () {
 
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
        Debug.Log(AngleDeg);
        
        
        if (Input.GetMouseButton(0))
        {
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
        else if (!Input.GetMouseButton(0))
        {
            attackSound.Pause();
            part.Stop();
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
        Debug.Log("collision");
        enemyScript = other.GetComponent<Enemy>();
        damage = true;
        Damage();
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
