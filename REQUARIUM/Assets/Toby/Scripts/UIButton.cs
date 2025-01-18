using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    public List<AudioSource> sourcesOn;
    public Animator animator;

    public void LoadScene(string scene)
    {
        StartCoroutine(sceneTransition(scene));
        
        
    }
    public void LoadSceneWithNoTransition(string scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }
    public void Quit()
    {

        Application.Quit();
    }
    public void PlaySound (AudioSource audioSource)
    {
        foreach (AudioSource source in  sourcesOn)
        {
            source.Stop();
        }
        audioSource.Play();
    }
    private IEnumerator sceneTransition(string scene)
    {
        
        animator.SetBool("sceneTransitionIn", true);
        yield return new WaitForSeconds(3.1f);
        SceneManager.LoadScene(scene);
        Debug.Log("lock in");


    }

}