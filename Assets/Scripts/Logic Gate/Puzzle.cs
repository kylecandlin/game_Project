﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Puzzle : MonoBehaviour {

    // Foreign Scripts and Variables
    public GameObject Player, interactable, Door;
    public Interactable interactableScript;
    public HotBar HotBar;
    Rigidbody2D playerRb2d;
    public Vector3 target;

    public GameObject LogicSlot, LogicSlotImage, output, greenExplosion;
    public Sprite not, and, xor, selectedGateImage;
    public SpriteRenderer logicSlotImage_r, puzzle_r, output_r;
    public string[] unlockSquence;
    public string selectedGateName, sceneName;
    public bool unlockedBool = false, completed = false;
    public float moveUp;

    // Use this for initialization
    void Start() {
        Player = GameObject.Find("Player");
        playerRb2d = Player.GetComponent<Rigidbody2D>();
        interactableScript = interactable.GetComponent<Interactable>();
        logicSlotImage_r = LogicSlotImage.GetComponent<SpriteRenderer>();
        puzzle_r = GetComponent<SpriteRenderer>();
        output_r = output.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        target = Player.transform.position; // opens target gameobject at player position and follows position
        this.transform.position = new Vector3(target.x, target.y + 2);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ray of mouse position
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity); // detects raycast hit
            try
            {
                Debug.Log(hit.collider.gameObject.name);
            }
            catch {
                Debug.Log("No gameobject in ray path");
            }

            if (hit && hit.collider.gameObject.name == "LogicSlot" && completed == false)
            {
                logicSlotImage_r.sprite = HotBar.selectedGateImage;
                for (int i = 0; i < unlockSquence.Length; i++) {
                    if (HotBar.selectedGateName == unlockSquence[i])
                    {
                        Debug.Log("Unlocked");
                        unlockedBool = true;
                    }
                    else {
                        unlockedBool = false;
                    }
                }
            }
        }
        if (unlockedBool == true) {
            Unlocked();
        }
    }

    // waits for a given time and removes elements
    IEnumerator Success() {
        yield return new WaitForSeconds(0.5f);
        output_r.color = Color.green;
        yield return new WaitForSeconds(1.5f);
        // Removes puzzle and associated interaction area
        Destroy(this.gameObject);
        Destroy(interactable);

        Instantiate(greenExplosion, Player.transform.position, Quaternion.identity);

        // unfreezes motion and locks rotation
        playerRb2d.constraints = RigidbodyConstraints2D.None;
        playerRb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        ChangeScene();
        MoveDoor();
    }

    // function called when puzzle is successfully completed
    public void Unlocked() {
        completed = true;
        StartCoroutine(Success());        
    }

    public void MoveDoor() {       
            Door.transform.Translate(new Vector2(moveUp,0));      
    }

    public void ChangeScene (){
        if (sceneName != null) {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }     
    }

}
