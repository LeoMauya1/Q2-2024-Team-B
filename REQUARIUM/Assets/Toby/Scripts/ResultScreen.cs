using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{
    public TextMeshProUGUI quotaText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dayText;
    public GameObject regularText;
    public GameObject firedSheet;
    public int quotaIncreaseAmountMax;
    public int quotaIncreaseAmountMin;
    void Start()
    {
        IncreaseQuota();
        SaveDataManager.Instance.daveSata.workDay += 1;
        if (SaveDataManager.Instance.daveSata.workDay > 0)
        {
            SaveDataManager.Instance.daveSata.isNewGame = false;
        }
        if (SaveDataManager.Instance.daveSata.quotaMoney < SaveDataManager.Instance.daveSata.quota)
        {
            regularText.SetActive(false);
            firedSheet.SetActive(true);
        }
    }
    public void IncreaseQuota()
    {
        SaveDataManager.Instance.daveSata.quota += Artifact.randyTheRandom.Next(quotaIncreaseAmountMin, quotaIncreaseAmountMax);
    }
    // Update is called once per frame
    void Update()
    {
        quotaText.text = string.Format($"Quota: {SaveDataManager.Instance.daveSata.quota}");
        moneyText.text = string.Format($"Money: {SaveDataManager.Instance.daveSata.quotaMoney}");
        dayText.text = string.Format($"Move To Next Day: {SaveDataManager.Instance.daveSata.workDay}");
    }
}
