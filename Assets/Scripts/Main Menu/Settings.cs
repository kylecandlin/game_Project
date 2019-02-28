using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    public Slider MasterVolumeSlider; 
    public void Update()
    {
        AudioListener.volume = MasterVolumeSlider.value;
    }
}
