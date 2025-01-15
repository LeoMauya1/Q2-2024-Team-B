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
    public Animator animator;

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

    public void LoadNewGame(string scenename)
    {
        SaveDataManager.Instance.daveSata.isNewGame = true;
        SaveDataManager.Instance.daveSata = SaveDataManager.Instance.defaultData;
        StartCoroutine(sceneTransition(scenename));
       
    }


    private IEnumerator sceneTransition(string scenename)
    {
        animator.SetBool("sceneTransitionIn", true);
        yield return new WaitForSeconds(3.1f);
        SceneManager.LoadScene(scenename);

    }

}
