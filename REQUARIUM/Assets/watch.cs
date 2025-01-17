using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Watch : MonoBehaviour
{
    private TextMeshProUGUI watchText;
    private TextMeshPro thisWatch;



    private void Start()
    {
        thisWatch = GetComponent<TextMeshPro>();
        watchText = GameObject.Find("Timer Text").GetComponent<TextMeshProUGUI>();

    }
        void Update()
        {
            thisWatch.text = watchText.text;
            
        }
}
