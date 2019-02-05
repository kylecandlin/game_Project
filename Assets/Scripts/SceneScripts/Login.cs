using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {
    public string inputUsername; public string inputPassword; public int inputHealth; public int inputStamina; public int inputMana; public int inputMaxHealth; public int inputMaxStamina; public int inputMaxMana;
    string playerInsert_Link = "http://part1-17.wbs.uni.worc.ac.uk/ProjectApp/InsertUser.php";
    public InputField usernameInputField, passwordInputField;

    // Use this for initialization
    void Start () {
		
	}
	


    public void InsertData(string playerUsername, string playerPassword)
    {
        WWWForm insertForm = new WWWForm();
        insertForm.AddField("usernamePOST", playerUsername);
        insertForm.AddField("passwordPOST", playerPassword);
        /*insertForm.AddField("healthPOST", testPlayerHealth);
        insertForm.AddField("staminaPOST", testPlayerStamina);
        insertForm.AddField("manaPOST", testPlayerMana);
        insertForm.AddField("maxHealthPOST", testPlayerMaxHealth);
        insertForm.AddField("maxStaminaPOST", testPlayerMaxStamina);
        insertForm.AddField("maxManaPOST", testPlayerMaxMana);
        */
        WWW filePush = new WWW(playerInsert_Link, insertForm);
    }

    public void Clicked() {
        inputUsername = usernameInputField.text;
        inputPassword = passwordInputField.text;
        InsertData(inputUsername, inputPassword);
    }
}
