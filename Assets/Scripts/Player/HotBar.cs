using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBar : MonoBehaviour {

    public Text andText, xorText, notText;
    public int andNumber, xorNumber, notNumber;

    public void HotBarUpdate(string name) {
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
}
