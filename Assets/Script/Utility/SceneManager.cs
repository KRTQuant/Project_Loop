using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantA.Utils;
using UnityEngine.SceneManagement;

public class SceneManager : MonoSingleton<SceneManager>
{
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
