using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBar : MonoBehaviour {

    public Text andText, orText, notText;
    public int andNumber, orNumber, notNumber;

    public void HotBarUpdate(string name) {
        switch (name)
        {
            case ("andPrefab"):
                andNumber ++;
                break;
            case ("orPrefab"):
                orNumber ++;
                break;
            case ("notPrefab"):
                notNumber++;
                break;
        }
        andText.text = andNumber.ToString();
        orText.text = orNumber.ToString();
        notText.text = notNumber.ToString();
    }
}
