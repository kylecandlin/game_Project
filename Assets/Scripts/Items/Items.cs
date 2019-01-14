using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Items {

    

 //   static TextAsset notName = Resources.Load(Path.Combine("JSON", "Items")) as TextAsset;
  //  Items_List file = JsonUtility.FromJson<Items_List>(notName.text);

    public string Name;
    public string Description;
    public int Damage;
    public string Rarity;
    public int Value;
    public string Type;

    public static Items CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Items>(jsonString);
    }
    
   
}

