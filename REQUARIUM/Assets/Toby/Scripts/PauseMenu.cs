using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject UIElements;
    public TextMeshProUGUI batteryText;
    public TextMeshProUGUI quotaText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI spearText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timerText;
    public KeyCode pause;
    //public Timer timer;
    public bool isOpen;
    // Update is called once per frame

    public void Start()
    {
    }


    void Update()
    {
        if (Input.GetKeyDown(pause) && isOpen == true)
        {
            Continue();
        }
        else if (Input.GetKeyDown(pause) && isOpen == false)
        {
            Pause();
        }
        batteryText.text = string.Format($"Batteries: {SaveDataManager.Instance.daveSata.batteries}");
        quotaText.text = string.Format($"Quota: {SaveDataManager.Instance.daveSata.quota}");
        moneyText.text = string.Format($"Money: {SaveDataManager.Instance.daveSata.spendingMoney}");
        spearText.text = string.Format($"Spears: {SaveDataManager.Instance.daveSata.spears}");
        dayText.text = string.Format($"Day: {SaveDataManager.Instance.daveSata.workDay}");
        //timerText.text = string.Format($"{timer.timerText}");
    }
    public void Pause()
    {
        isOpen = true;
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        UIElements.SetActive(false);
    }
    public void Continue()
    {
        isOpen = false;
        PausePanel.SetActive(false);
        UIElements.SetActive(true);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title Screen");
    }
}
