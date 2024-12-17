using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public SaveData defaultData;
    [HideInInspector]public SaveData daveSata;

    public string filePath;
    private void OnEnable()
    {
        daveSata = SaveData.Load(Application.persistentDataPath + filePath, defaultData);
    }

    private void OnDisable()
    {
        daveSata.Save(Application.persistentDataPath + filePath);
    }
}
