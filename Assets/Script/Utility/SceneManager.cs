using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantA.Utils;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
