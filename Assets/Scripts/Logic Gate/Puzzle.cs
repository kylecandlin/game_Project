using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class Puzzle : MonoBehaviour {

    // Foreign Scripts and Variables
    public GameObject Player, interactable, Door, nextSceneText, playerUI;
    public Interactable interactableScript;
    public HotBar HotBar;
    public LogicSlotScript LogicSlotScript;
    Rigidbody2D playerRb2d;
    private Vector3 target;

    public GameObject output, greenExplosion, LogicSlotImage;
    public Transform logicSlotImage_t;
    public GameObject[] LogicSlot;
    public Sprite not, and, xor, selectedGateImage;
    private SpriteRenderer logicSlotImage_r; 
    public SpriteRenderer puzzle_r, output_r;
    public string[] unlockSquence, gateArray;
    public string selectedGateName, sceneName;
    private bool unlockedBool = false, completed = false;
    private int thresVal, moveUp;
    

    // Use this for initialization
    void Start() {
        moveUp = 10;
        Player = GameObject.Find("Player");
        playerRb2d = Player.GetComponent<Rigidbody2D>();
        interactableScript = interactable.GetComponent<Interactable>();
        puzzle_r = GetComponent<SpriteRenderer>();
        output_r = output.GetComponent<SpriteRenderer>();
        gateArray = new string[LogicSlot.Length];
    }

    // Update is called once per frame
    void Update() {

        thresVal = HotBar.selectedGateAmount;
        target = Player.transform.position; // opens target gameobject at player position and follows position
        this.transform.position = new Vector3(target.x, target.y + 2);




        for (int i = 0; i < LogicSlot.Length; i++) {
            LogicSlotScript = LogicSlot[i].GetComponent<LogicSlotScript>();
            if (LogicGateLogic((int)LogicSlotScript.inputValues[0], (int)LogicSlotScript.inputValues[1], i))
            {
                if (LogicSlotScript.outputObj != null)
                {
                    LogicSlotScript.outputObj.GetComponent<SpriteRenderer>().color = Color.green;
                }
                if (LogicSlotScript.nextSlotObj != null)
                {
                    // decide whether to change top or bottom
                    if ((int)LogicSlotScript.nextSlotInput[0] == 0)
                    { // change top
                        LogicSlotScript.nextSlotObj.GetComponent<LogicSlotScript>().ChangeInputs(true, LogicSlotScript.Values.one, false, LogicSlotScript.Values.one);
                    }
                    if ((int)LogicSlotScript.nextSlotInput[0] == 1)
                    { // change bottom
                        LogicSlotScript.nextSlotObj.GetComponent<LogicSlotScript>().ChangeInputs(false, LogicSlotScript.Values.one, true, LogicSlotScript.Values.one);
                    }
                }

            }
            else if (LogicGateLogic((int)LogicSlotScript.inputValues[0], (int)LogicSlotScript.inputValues[1], i) == false)
            {
                if (LogicSlotScript.outputObj != null)
                {
                    LogicSlotScript.outputObj.GetComponent<SpriteRenderer>().color = Color.red;
                }
                if (LogicSlotScript.nextSlotObj != null)
                {
                    // decide whether to change top or bottom
                    if ((int)LogicSlotScript.nextSlotInput[0] == 0)
                    { // change top
                        LogicSlotScript.nextSlotObj.GetComponent<LogicSlotScript>().ChangeInputs(true, LogicSlotScript.Values.zero, false, LogicSlotScript.Values.zero);
                    }
                    if ((int)LogicSlotScript.nextSlotInput[0] == 1)
                    { // change bottom
                        LogicSlotScript.nextSlotObj.GetComponent<LogicSlotScript>().ChangeInputs(false, LogicSlotScript.Values.zero, true, LogicSlotScript.Values.zero);
                    }
                }
            }
            else {
                LogicSlotScript.outputObj.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }


        // Mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ray of mouse position
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity); // detects raycast hit
           
            // loops through logic slots to identify which one is clicked
            for (int i = 0; i < LogicSlot.Length; i++) {
                if (hit && hit.collider.gameObject == LogicSlot[i] && completed == false && thresVal >0)
                {


                    


                    logicSlotImage_t = LogicSlot[i].transform.GetChild(0);
                    logicSlotImage_r = logicSlotImage_t.GetComponent<SpriteRenderer>();
                    if (gateArray[i] != HotBar.selectedGateName) {
                        HotBar.UpdateNumber(1, gateArray[i], false);
                        HotBar.UpdateNumber(-1, HotBar.selectedGateName, true);
                                                          
                        Debug.Log("andnumber    " + HotBar.andNumber+"xornumber " + HotBar.xorNumber+"notnumber " + HotBar.notNumber);
                        Debug.Log("Gate Array" + gateArray[i]);
                    }                   
                    Debug.Log("selected gate amount " + HotBar.selectedGateAmount);
                    logicSlotImage_r.sprite = HotBar.selectedGateImage;   
                    gateArray[i] = HotBar.selectedGateName;
                    if (Enumerable.SequenceEqual(unlockSquence, gateArray)) {
                        Unlocked();
                    }                               
                }
            }
        }
    }

    // waits for a given time and removes elements
    IEnumerator Success() {
        yield return new WaitForSeconds(0.5f);
        output_r.color = Color.green;
        yield return new WaitForSeconds(1.5f);
        puzzle_r.enabled = false;
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
            r.enabled = false;
        interactable.SetActive(false);
        Instantiate(greenExplosion, Player.transform.position, Quaternion.identity);
        if (nextSceneText != null) {
            yield return new WaitForSeconds(0.5f);
            playerUI.SetActive(false);
            Instantiate(nextSceneText, Player.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }

        // unfreezes motion and locks rotation
        playerUI.SetActive(true);
        playerRb2d.constraints = RigidbodyConstraints2D.None;
        playerRb2d.constraints = RigidbodyConstraints2D.FreezeRotation;      
        Complete();
    }
  
    // function called when puzzle is successfully completed
    public void Unlocked() {
        completed = true;
        StartCoroutine(Success());        
    }

    public void Complete() {
        if (Door != null) {
            Door.transform.Translate(new Vector2(0, moveUp));
        }            
        if (sceneName != "")
        {
          
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        Destroy(this.gameObject);
    }

    public void ChangeScene (){
        if (sceneName != null) {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }     
    }

    public bool LogicGateLogic(int topInput, int bottomInput, int i) {
        switch (gateArray[i]) {
            case ("NOT"):
                if (topInput == 0 && bottomInput == 3 || topInput == 3 && bottomInput == 0) {
                    return true;
                }
                else {
                    return false;
                }

            case ("AND"):
                if (topInput == 1 && bottomInput == 1) {

                    return true;
                }
                else {
                    return false;
                }

            case ("XOR"):
                if (topInput == 0 && bottomInput == 1 || topInput == 1 && bottomInput == 0) {
                    return true;
                }
                else {
                    return false;
                }
            default:
                return false;
        }
        
    }

}
