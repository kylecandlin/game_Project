using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGateUI : MonoBehaviour {
    public Puzzle Puzzle;

    public void Clicked() {
        Puzzle.Insert(this.name);
    }
}
