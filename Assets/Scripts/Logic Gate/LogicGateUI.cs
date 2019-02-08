using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGateUI : MonoBehaviour {
    public HotBar Hotbar;

    public void Clicked() {
        Hotbar.Insert(this.name);
    }
}
