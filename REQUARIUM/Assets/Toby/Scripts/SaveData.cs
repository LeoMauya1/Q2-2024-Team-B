using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

[System.Serializable]
public struct SaveData
    {
        public static SaveData daveSata;
   // public SaveData(string filePath)
        //{
            
        //}
        //[JsonInclude]
        public int money;
        //[JsonInclude]
        public int batteries;
        //[JsonInclude]
        public int spears;
        //[JsonInclude]
        public float health;
        //[JsonInclude]
        public static int workDay;
        //[JsonInclude]
        public static bool isNewGame;
    }
