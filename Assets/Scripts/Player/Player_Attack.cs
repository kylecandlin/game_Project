using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour {

    public LayerMask Enemy;
    public Enemy enemyScript;
    public GameObject rightArmPlayer, playerObj;

    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    bool damage;

    // Use this for initialization
    void Start () {
       
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
            if (AngleDeg <= 90 && AngleDeg >= -60)
            {
                rightArmPlayer.transform.rotation = Quaternion.Euler(0, 0, AngleDeg + 30);
            }
            part.Play();
        }
        else {
            part.Stop();
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
}
