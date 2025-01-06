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

    //private float targetDistance = 5;
    public UnityEvent rejection;

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
        oxygen.text = string.Format($"${oxygenTankCost} per 1/2 Oxygen Refill");
        timers.text = string.Format($"${timerCost} per minute added to time");
    }

    public void AddBattery()
    {
        if (SaveDataManager.Instance.daveSata.spendingMoney >= batteryCost)
        {
            SaveDataManager.Instance.daveSata.batteries += 1;
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
            SaveDataManager.Instance.daveSata.health += 50;
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
