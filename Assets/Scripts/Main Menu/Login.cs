using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public string inputUsername; public string inputPassword; public int inputHealth; public int inputStamina; public int inputMana; public int inputMaxHealth; public int inputMaxStamina; public int inputMaxMana;
    string playerInsert_Link = "http://part1-17.wbs.uni.worc.ac.uk/Companion/InsertUser.php",
    playerDownload_Link = "http://part1-17.wbs.uni.worc.ac.uk/Companion/ItemsData.php",
    login_Link = "http://part1-17.wbs.uni.worc.ac.uk/Companion/checkGameUser.php";
    public InputField usernameInputField, passwordInputField;
    public GameObject PlayerDetailsObj, mainMenuCanvas;
    public PlayerDetails PlayerDetails;

    // Database variables
    public string file;
    public string[] items;
    public bool coRoutineDone;
    public int databaseAndGate, databaseNotGate, databaseXorGate;

    public void Start()
    {
        PlayerDetails = PlayerDetailsObj.GetComponent<PlayerDetails>();
        if (PlayerDetails.username.ToString() != "")
        {
            Debug.Log(PlayerDetails.username);
            this.gameObject.SetActive(false);
            mainMenuCanvas.SetActive(true);
        }
        else if (PlayerDetails.username.ToString() == "")
        {
            this.gameObject.SetActive(true);
            mainMenuCanvas.SetActive(false);
        }
    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }

    public void Update()
    {
    }

    public void InsertData(string playerUsername, string playerPassword)
    {
        WWWForm insertForm = new WWWForm();
        insertForm.AddField("usernamePOST", playerUsername);
        insertForm.AddField("passwordPOST", playerPassword);
        WWW filePush = new WWW(playerInsert_Link, insertForm);
    }

    public void Clicked()
    {
        if (usernameInputField != null && passwordInputField != null) {
            inputUsername = usernameInputField.text;
            inputPassword = passwordInputField.text;
            InsertData(inputUsername, inputPassword);
            PlayerDetails.UpdateLogin(inputUsername, inputPassword);
        }       
    }

    public void LoginBtnClick()
    {
        if (usernameInputField != null && passwordInputField != null)
        {
            inputUsername = usernameInputField.text;
            inputPassword = passwordInputField.text;
            PlayerDetails.UpdateLogin(inputUsername, inputPassword);
            StartCoroutine(loginFormDownload());
        }

    }

    public void PlayGuestBtn() {
        PlayerDetails.UpdateLogin("Guest", "");
        mainMenuCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }

    IEnumerator loginFormDownload() {
        
        string loginSuccess;
        WWWForm insertForm2 = new WWWForm();
        insertForm2.AddField("usernamePOST", usernameInputField.text);
        insertForm2.AddField("passwordPOST", passwordInputField.text);

        WWW loginForm = new WWW(login_Link, insertForm2);
        yield return loginForm;
        loginSuccess = loginForm.text.ToString();
        print(loginSuccess);
        if (loginForm.error == null)
        {
            Debug.Log("Connection Successful...");
            print(loginForm.text.ToString());
            if (loginSuccess == " 1")
            {
                Debug.Log("Successfull login");
                mainMenuCanvas.SetActive(true);
                this.gameObject.SetActive(false);
            }
            else if (loginSuccess == " 0")
            {
                Debug.Log("Failed");
            }
           
        }
        else
        {
            Debug.Log("Error...");
        }
        
    }

   
}

