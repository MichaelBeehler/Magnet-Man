using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public void Play()
    {
        SceneManager.LoadScene("Chamber1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}