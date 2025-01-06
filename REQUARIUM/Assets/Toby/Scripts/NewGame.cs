using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public UnityEvent onNewGame;
    public UnityEvent onContinue;

    public void OnEnable()
    {
        if (SaveDataManager.Instance.daveSata.isNewGame)
        {
            onNewGame.Invoke();
        }
        else
        {
            onContinue.Invoke();
        }
    }

    public void LoadNewGame()
    {
        SaveDataManager.Instance.daveSata.isNewGame = true;
        SaveDataManager.Instance.daveSata = SaveDataManager.Instance.defaultData;
        SceneManager.LoadScene("Main Game");
    }
}
