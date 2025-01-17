using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class watchText2 : MonoBehaviour
{
    private TextMeshProUGUI QuotaText;
    private TextMeshPro thisQuota;
    void Start()
    {
        thisQuota = GetComponent<TextMeshPro>();
        QuotaText = GameObject.Find("Quota Text").GetComponent<TextMeshProUGUI>();
 
    
    
    
    
    }


    void Update()
    {
        thisQuota.text = QuotaText.text;
    }


}

    

