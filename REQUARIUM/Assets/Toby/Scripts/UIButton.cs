using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    public List<AudioSource> sourcesOn;
    public void LoadScene(string scene)
    {
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

}