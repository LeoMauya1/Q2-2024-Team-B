using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    public UnityEvent onNewGame;
    public UnityEvent onContinue;
    public Image LoadingBarFill;
    public GameObject loadingScreen;
    private float progressBar;
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

    public void LoadNewGame(int sceneID)
    {
        SaveDataManager.Instance.daveSata.isNewGame = true;
        SaveDataManager.Instance.daveSata = SaveDataManager.Instance.defaultData;
        StartCoroutine(LoadSceneAsync(sceneID));
        
        
        
        SceneManager.LoadScene(sceneID);
    }

    IEnumerator LoadSceneAsync(int sceneID)
   {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);


        loadingScreen.SetActive(true);


        while (!operation.isDone)
       {

            progressBar = Mathf.Clamp01(operation.progress/ 0.9f);

           LoadingBarFill.fillAmount = progressBar;
            
           yield return null;
        }
   }
}
