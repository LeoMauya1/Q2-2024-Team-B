using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayText : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = string.Format($"Quota: ${SaveDataManager.Instance.daveSata.quota - SaveDataManager.Instance.daveSata.quotaMoney}");
        if (SaveDataManager.Instance.daveSata.quotaMoney >= SaveDataManager.Instance.daveSata.quota)
        {
            text.text = string.Format($"Quota Complete");
        }
    }
}
