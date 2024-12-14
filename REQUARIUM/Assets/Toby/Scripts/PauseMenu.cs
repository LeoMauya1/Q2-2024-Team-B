using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject UIElements;
    public KeyCode pause;
    // Update is called once per frame

    public void Start()
    {
    }


    void Update()
    {
        if (Input.GetKeyDown(pause))
        {
            Pause();
        }

    }
    public void Pause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        UIElements.SetActive(false);
        
    }
    public void Continue()
    {
        PausePanel.SetActive(false);
        UIElements.SetActive(true);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title Screen");
    }
}
