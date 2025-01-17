using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerTextPause;
    public EnemyManager enemyManager;
    public float remainingTime;

    void Start()
    {
        remainingTime = enemyManager.dolphinTimer + SaveDataManager.Instance.daveSata.timeAddition;
        remainingTime -= SaveDataManager.Instance.daveSata.timeRemoval;
    }
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            timerText.color = Color.red;
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerTextPause.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void RestartTimer()
    {
        remainingTime = enemyManager.dolphinTimer;
        timerText.color = Color.white;
    }
}