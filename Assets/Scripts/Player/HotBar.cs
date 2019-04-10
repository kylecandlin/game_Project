using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBar : MonoBehaviour {
    // Database Variables
    string logicGateRecieve_Link = "http://part1-17.wbs.uni.worc.ac.uk/Companion/ItemsData.php", 
        logicGateSend_Link = "http://part1-17.wbs.uni.worc.ac.uk/Companion/UpdateGateGame.php";
    private int databaseAndGate, databaseNotGate, databaseXorGate;
    string[] items;

    // Player Details
    public PlayerDetails PlayerDetails;
    public GameObject PlayerDetailsObj;

    // Local Logic Gate Variables
    public Text andText, xorText, notText; // UI display values
    public int andNumber, xorNumber, notNumber, selectedGateAmount; // Number of Logic gates
    public Sprite not, and, xor, selectedGateImage; // Logic gate images
    public string selectedGateName, databaseGateName; // current selected gate and database version
    private AudioSource pickupSound; // Audio on pickup

    private void Start()
    {
        PlayerDetails = PlayerDetailsObj.GetComponent<PlayerDetails>();
        pickupSound = GetComponent<AudioSource>();        
    }

    private void Update()
    {
        StartCoroutine(RecieveLogicGateNumber());

    }

    // Gameobject pickup for logic gates
    public void HotBarUpdate(string name) {
        Debug.Log("HotbarUpdate     "+name);
        pickupSound.Play();
        switch (name)
        {
            case ("andPrefab"):
                DetectAndSet("AND");
                UpdateNumber(1, "AND",false);          
                break;
            case ("xorPrefab"):
                DetectAndSet("XOR");
                UpdateNumber(1, "XOR",false);                
                break;
            case ("notPrefab"):
                DetectAndSet("NOT");
                UpdateNumber(1, "NOT",false);               
                break;
        }       
        DisplayNumbers();
    }

    // Changes number of logic gates
    public void UpdateNumber(int additionVal, string gateName, bool dAS) {
       
        string updateSymbol = null; // symbol for database

        // conversions from addition value to symbol
        if (additionVal > 0) { updateSymbol = "+"; }
        else if (additionVal < 0) { updateSymbol = "-"; }
        else { Debug.Log("Error: UpdateNumber zero value    value: "+ additionVal); } // error check if value zero
        
        // alters Logic gate number and updates database
        switch (gateName)
        {
            case ("NOT"):
               
                notNumber += additionVal;
                databaseGateName = "not_Gate";
                if (dAS == true) {
                    DetectAndSet("NOT");
                }                
                break;

            case ("AND"):
             
                andNumber += additionVal;
                databaseGateName = "and_Gate";
                if (dAS == true)
                {
                    DetectAndSet("AND");
                }
                break;

            case ("XOR"):
             
                xorNumber += additionVal;
                databaseGateName = "xor_Gate";
                if (dAS == true)
                {
                   DetectAndSet("XOR");
                }
                break;
        }
        for (int i = 0; i < Mathf.Abs(additionVal); i++)
        {
            Debug.Log("database name:   "+ databaseGateName);
            StartCoroutine(SendLogicGateNumber(updateSymbol, databaseGateName.ToString()));
        }
        DisplayNumbers();

    }

    public void DetectAndSet(string logicName) {
        selectedGateName = logicName; // allows for global access
        switch (selectedGateName)
        {
            case ("NOT"):
                selectedGateAmount = notNumber;
                selectedGateImage = not;
                break;
            case ("AND"):
                selectedGateAmount = andNumber;
                selectedGateImage = and;
                break;
            case ("XOR"):
                selectedGateAmount = xorNumber;
                selectedGateImage = xor;
                break;
        }
    }

    public void DisplayNumbers() {
        andText.text = andNumber.ToString();
        xorText.text = xorNumber.ToString();
        notText.text = notNumber.ToString();
    }

    // Number of Gates stored in the database
    IEnumerator RecieveLogicGateNumber()
    {
        WWWForm UserNameForm = new WWWForm();
        UserNameForm.AddField("usernamePOST", PlayerDetails.username);
        WWW itemsData = new WWW(logicGateRecieve_Link, UserNameForm);
        yield return itemsData;
        string itemsDataString = itemsData.text;
        items = itemsDataString.Split(';');
     
        databaseAndGate = int.Parse(ReturnValue(items[0], "and_Gate:"));
        databaseNotGate = int.Parse(ReturnValue(items[0], "not_Gate:"));
        databaseXorGate = int.Parse(ReturnValue(items[0], "xor_Gate:"));
        andNumber = databaseAndGate;
        notNumber = databaseNotGate;
        xorNumber = databaseXorGate;
        DisplayNumbers();
    }

    // Returns value of each gate
    string ReturnValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }

    // Sends Local number of logic gates
    IEnumerator SendLogicGateNumber(string symbol, string gateType) {
        WWWForm LogicNumberForm = new WWWForm();
        LogicNumberForm.AddField("usernamePOST", PlayerDetails.username);
        LogicNumberForm.AddField("symbol", symbol);
        LogicNumberForm.AddField("gateType", gateType);
        WWW LogicNumberSend = new WWW(logicGateSend_Link, LogicNumberForm);
        yield return LogicNumberSend;
        
    }
}
