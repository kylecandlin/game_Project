using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {
    public string inputUsername; public string inputPassword; public int inputHealth; public int inputStamina; public int inputMana; public int inputMaxHealth; public int inputMaxStamina; public int inputMaxMana;
    string playerInsert_Link = "http://part1-17.wbs.uni.worc.ac.uk/Companion/InsertUser.php", 
    playerDownload_Link = "http://part1-17.wbs.uni.worc.ac.uk/Companion/ItemsData.php";
    public InputField usernameInputField, passwordInputField;
    public GameObject PlayerDetailsObj;
    public PlayerDetails PlayerDetails;

    // Database variables
    public string file;
    public string[] items;
    public bool coRoutineDone;
    public int databaseAndGate;
    public int databaseNotGate;
    public int databaseXorGate;

    public void Start()
    {
        PlayerDetails = PlayerDetailsObj.GetComponent<PlayerDetails>();
    }

    // Use this for initialization
    IEnumerator DataRecieve () {
        coRoutineDone = false;
        WWW itemsData = new WWW(playerDownload_Link);
        yield return itemsData;
        string itemsDataString = itemsData.text;
        items = itemsDataString.Split(';');
        coRoutineDone = true;
        databaseAndGate = int.Parse(GetDataValue(items[0], "and_Gate:"));
        databaseNotGate = int.Parse(GetDataValue(items[0], "not_Gate:"));
        databaseXorGate = int.Parse(GetDataValue(items[0], "xor_Gate:"));
        print(databaseAndGate);
    }

    string GetDataValue(string data, string index) {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))value = value.Remove(value.IndexOf("|"));
            return value;                
    }

    public void Update()
    {
        try { StartCoroutine(DataRecieve()); }
        catch { Debug.Log("database error"); }      
    }

    public void InsertData(string playerUsername, string playerPassword)
    {
        WWWForm insertForm = new WWWForm();
        insertForm.AddField("usernamePOST", playerUsername);
        insertForm.AddField("passwordPOST", playerPassword);
        WWW filePush = new WWW(playerInsert_Link, insertForm);   
    }

    public void Clicked() {
        inputUsername = usernameInputField.text;
        inputPassword = passwordInputField.text;
        InsertData(inputUsername, inputPassword);
        PlayerDetails.UpdateLogin(inputUsername,inputPassword);
    }

    public void LoginBtnClick()
    {
        
    }
}
