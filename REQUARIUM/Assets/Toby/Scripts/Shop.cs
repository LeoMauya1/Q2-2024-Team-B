using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject player;
    public PlayerInfo playerInfo;
    public Timer timer;
    public int batteryCost;
    public int spearCost;
    public int oxygenTankCost;
    public int timerCost;

    private float playerDistance;

    private float targetDistance = 5;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = player.GetComponent<PlayerInfo>();
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
            timer.remainingTime += 60;
        }
        else
        {
            Rejection();
        }
    }
    
    public void Rejection()
    {

    }
    void Update()
    {
        playerDistance = Vector3.Distance(this.transform.position, player.transform.position);
    }
}
