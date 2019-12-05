using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenu : MonoBehaviour
{
    public void MeneScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadingScene()
    {
        SceneManager.LoadScene("Loading");
    }

    public void ExitScene()
    {
        Application.Quit();
    }
     
}
