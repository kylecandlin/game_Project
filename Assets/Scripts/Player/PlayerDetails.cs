using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetails : MonoBehaviour {

    public string username = null, password = null;

	// Use this for initialization
	void Start () {
        username = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdateLogin(string updateUsername, string updatePassword) {
        username = updateUsername;
        password = updatePassword;
    }
}
