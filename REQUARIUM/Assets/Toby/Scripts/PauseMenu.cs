using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject artifactPanel;
    public GameObject UIElements;
    public TextMeshProUGUI batteryText;
    public TextMeshProUGUI quotaText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI spearText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timerText;
    public KeyCode pause;
    public KeyCode artifactInfo;
    public Timer timer;
    public bool isOpen;
    public bool artifactOpen;
    // Update is called once per frame

    public void Start()
    {
    }


    void Update()
    {
        if (Input.GetKeyDown(pause) && isOpen == true && artifactOpen == false)
        {
            Continue();
        }
        else if (Input.GetKeyDown(pause) && isOpen == false && artifactOpen == false)
        {
            Pause();
        }
        if (Input.GetKeyDown(artifactInfo) && isOpen == false && artifactOpen == true)
        {
            Close();
        }
        else if (Input.GetKeyDown(artifactInfo) && isOpen == false && artifactOpen == false)
        {
            Open();
        }
        batteryText.text = string.Format($"{SaveDataManager.Instance.daveSata.batteries}");
        quotaText.text = string.Format($"Quota: ${SaveDataManager.Instance.daveSata.quota - SaveDataManager.Instance.daveSata.quotaMoney}");
        if (SaveDataManager.Instance.daveSata.quotaMoney >= SaveDataManager.Instance.daveSata.quota)
        {
            quotaText.text = string.Format($"Quota Complete");
        }
        moneyText.text = string.Format($"{SaveDataManager.Instance.daveSata.spendingMoney}");
        spearText.text = string.Format($"{SaveDataManager.Instance.daveSata.spears}");
        dayText.text = string.Format($"Day {SaveDataManager.Instance.daveSata.workDay}");
        timerText.text = string.Format($"{timer.timerText}");
    }
    public void Pause()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        isOpen = true;
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        UIElements.SetActive(false);
    }
    public void Continue()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
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

    public void Open()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        artifactOpen = true;
        artifactPanel.SetActive(true);
        UIElements.SetActive(false);
    }

    public void Close()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        artifactOpen = false;
        artifactPanel.SetActive(false);
        UIElements.SetActive(true);
    }
}
