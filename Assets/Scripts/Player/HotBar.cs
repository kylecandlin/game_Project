using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBar : MonoBehaviour {

    public Text andText, xorText, notText;
    public int andNumber, xorNumber, notNumber;

    // test
    public Sprite not, and, xor, selectedGateImage;
    public string selectedGateName;

    public void HotBarUpdate(string name) {
        Debug.Log("HotbarUpdate     "+name);
        switch (name)
        {
            case ("andPrefab"):
                andNumber ++;
                break;
            case ("xorPrefab"):
                xorNumber ++;
                break;
            case ("notPrefab"):
                notNumber++;
                break;
        }
        andText.text = andNumber.ToString();
        xorText.text = xorNumber.ToString();
        notText.text = notNumber.ToString();
    }

    public void Insert(string logicName) {
        selectedGateName = logicName; // allows for global access
        Debug.Log("Insert" + logicName);

        // detects what image to use based on the players logic gate selection
        switch (logicName)
        {
            case ("NOT"):
                selectedGateImage = not;
                break;
            case ("AND"):
                selectedGateImage = and;
                break;
            case ("XOR"):
                selectedGateImage = xor;
                break;
        }
    }
}
