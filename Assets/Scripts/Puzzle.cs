using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour {

    // Foreign Scripts and Variables
    public GameObject Player;
    public Vector3 target;
    public Button LogicSlotButton;
    public GameObject LogicSlot;

    // Use this for initialization
    void Start() {
        Player = GameObject.Find("Player");



    }

    void TaskOnClick() {
        Debug.Log("click");
    }

    // Update is called once per frame
    void Update() {
        LogicSlotButton = LogicSlot.GetComponent<Button>();
        LogicSlotButton.onClick.AddListener(TaskOnClick);
        target = Player.transform.position;
        this.transform.position = new Vector3 (target.x, target.y +2);
    }
    public void test(){}
}
