using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LogicSlotScript : MonoBehaviour {
    // Input values for each instance of slot
    public enum Values { one = 1, zero = 0, none = 3 };
    public Values[] inputValues = new Values[2];

    // whether it effects the top or bottom input of the next slot
    public enum TopOrBottom { top, bottom}; 
    public TopOrBottom[] nextSlotInput = new TopOrBottom[1];

    // the output that changes colour on correct logic gate and next slot
    public GameObject outputObj, nextSlotObj;
    public void Awake()
    {
        int val = (int)inputValues[0];
        int val2 = (int)inputValues[1];
        Debug.Log("input values "+val+"    "+val2);
    }

    public void ChangeInputs(bool changeTop, Values top, bool changeBottom, Values bottom) {
        if (changeTop) {
            inputValues[0] = top;
        }
        if (changeBottom) {
            inputValues[1] = bottom;
        }
       
    }
}
