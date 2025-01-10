using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    //public GameObject player;
    //public PlayerInfo playerInfo;
    public GameObject shopPanel;
    public int batteryCost;
    public int spearCost;
    public int oxygenTankCost;
    public int timerCost;

    public TextMeshProUGUI battery;
    public TextMeshProUGUI spear;
    public TextMeshProUGUI oxygen;
    public TextMeshProUGUI timers;
    public TextMeshProUGUI spendingMoney;
    //public float playerDistance;
    public TextMeshProUGUI spearText;
    public TextMeshProUGUI batteryText;
    public TextMeshProUGUI currentHealth;
    public TextMeshProUGUI additionalTime;
    //private float targetDistance = 5;
    public UnityEvent rejection;
    
    private float remainingTime = 300 + SaveDataManager.Instance.daveSata.timeAddition;
    //public KeyCode openShop;
    //public bool shopOpen;
    //public bool isUi;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //playerInfo = player.GetComponent<PlayerInfo>();
        battery.text = string.Format($"${batteryCost} per Battery");
        spear.text = string.Format($"${spearCost} per Spear");
        oxygen.text = string.Format($"${oxygenTankCost} per Oxygen Refill");
        timers.text = string.Format($"${timerCost} per minute added to time");
    }

    public void AddBattery()
    {
        if (SaveDataManager.Instance.daveSata.spendingMoney >= batteryCost)
        {
            SaveDataManager.Instance.daveSata.batteries += 1;
            SaveDataManager.Instance.daveSata.spendingMoney -= batteryCost;
        }
        else
        {
            Rejection();
        }
    }
    public void AddSpear()
    {
        if (SaveDataManager.Instance.daveSata.spendingMoney >= spearCost)
        {
            SaveDataManager.Instance.daveSata.spears += 1;
            SaveDataManager.Instance.daveSata.spendingMoney -= spearCost;
        }
        else
        {
            Rejection();
        }
    }
    public void BuyOxygen()
    {
        if (SaveDataManager.Instance.daveSata.spendingMoney >= oxygenTankCost)
        {
            SaveDataManager.Instance.daveSata.health = 0;
            SaveDataManager.Instance.daveSata.spendingMoney -= batteryCost;
        }
        else if (SaveDataManager.Instance.daveSata.health == 0)
        {
            Rejection();
        }
        else
        {
            Rejection();
        }
    }
    public void AddTime()
    {
        if (SaveDataManager.Instance.daveSata.spendingMoney >= timerCost)
        {
            SaveDataManager.Instance.daveSata.timeAddition += 30;
            SaveDataManager.Instance.daveSata.spendingMoney -= batteryCost;
            remainingTime += SaveDataManager.Instance.daveSata.timeAddition;
        }
        else
        {
            Rejection();
        }
    }
    
    public void Rejection()
    {
        rejection.Invoke();
    }
    void Update()
    {
        spendingMoney.text = string.Format($"${SaveDataManager.Instance.daveSata.spendingMoney} to spend.");
        batteryText.text = string.Format($"Batteries Owned: {SaveDataManager.Instance.daveSata.batteries}");
        spearText.text = string.Format($"Spears Owned: {SaveDataManager.Instance.daveSata.spears}");
        if (SaveDataManager.Instance.daveSata.health >= -50)
        {
            currentHealth.text = string.Format($"Current Oxygen Status: Dark Green");
        }
        else if (SaveDataManager.Instance.daveSata.health >= -105 && SaveDataManager.Instance.daveSata.health < -50)
        {
            currentHealth.text = string.Format($"Current Oxygen Status: Light Green");
        }
        else if (SaveDataManager.Instance.daveSata.health >= -145 && SaveDataManager.Instance.daveSata.health < -105)
        {
            currentHealth.text = string.Format($"Current Oxygen Status: Lime Green");
        }
        else if (SaveDataManager.Instance.daveSata.health >= -185 && SaveDataManager.Instance.daveSata.health < -145)
        {
            currentHealth.text = string.Format($"Current Oxygen Status: Yellow");
        }
        else if (SaveDataManager.Instance.daveSata.health >= -222 && SaveDataManager.Instance.daveSata.health < -185)
        {
            currentHealth.text = string.Format($"Current Oxygen Status: Orange");
        }
        else if (SaveDataManager.Instance.daveSata.health >= -264 && SaveDataManager.Instance.daveSata.health < -222)
        {
            currentHealth.text = string.Format($"Current Oxygen Status: OXYGEN CRITICAL");
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        additionalTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        /*if (isUi == true)
        {
            playerDistance = Vector3.Distance(this.transform.position, player.transform.position);
            if (playerDistance <= targetDistance && Input.GetKeyDown(openShop) && shopOpen == false)
            {
                shopPanel.gameObject.SetActive(true);
                Time.timeScale = 0;
                shopOpen = true;
            }
            else if (shopOpen == true && Input.GetKeyDown(openShop))
            {
                shopPanel.gameObject.SetActive(false);
                Time.timeScale = 1;
                shopOpen = false;
            }
        }*/
    }
}
