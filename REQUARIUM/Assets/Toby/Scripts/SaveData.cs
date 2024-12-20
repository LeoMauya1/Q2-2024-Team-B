using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

[System.Serializable]
public struct SaveData
{
    public int spendingMoney;

    public int quotaMoney;
   
    public int batteries;
   
    public int spears;
   
    public float health;
    
    public int workDay;
  
    public bool isNewGame;

    public int quota;
    public static SaveData Load(string filePath, SaveData defaultData)
    {
        if (!File.Exists(filePath))
        {
            return defaultData;
        }
        using StreamReader reader = new(filePath);
        string text = reader.ReadToEnd();

        return JsonUtility.FromJson<SaveData>(text);

    }   

    public void Save(string filePath)
    {
        using StreamWriter writer = new(filePath);
        string text = JsonUtility.ToJson(this);

        writer.Write(text);
    }
}
