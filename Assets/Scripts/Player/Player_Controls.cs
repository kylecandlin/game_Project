using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controls : MonoBehaviour {


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
    }

}
