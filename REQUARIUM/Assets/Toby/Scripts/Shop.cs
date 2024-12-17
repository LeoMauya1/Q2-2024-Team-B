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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = player.GetComponent<PlayerInfo>();
    }

    public void AddBattery()
    {
        if (playerInfo.saveData.money >= batteryCost)
        {
            playerInfo.saveData.batteries += 1;
        }
        else
        {
            //rejection
        }
    }
    public void AddSpear()
    {
        if (playerInfo.saveData.money >= spearCost)
        {
            playerInfo.saveData.spears += 1;
        }
        else
        {
            //rejection
        }
    }
    public void BuyOxygen()
    {
        if (playerInfo.saveData.money >= oxygenTankCost)
        {
            playerInfo.saveData.health += 50;
        }
        else
        {
            //rejection
        }
    }
    public void AddTime()
    {
        if (playerInfo.saveData.money >= timerCost)
        {
            timer.remainingTime += 60;
        }
        else
        {
            //rejection
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
