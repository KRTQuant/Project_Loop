using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class LastTeleporter : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] bool isTriggered;
    [SerializeField] GameObject overlayUI;
    [SerializeField] int delayTime = 1000;

    private SceneManager sceneManager => SceneManager.Instance;

    private void OnCollisionEnter(Collision collision)
    {
        if (isTriggered || collision.transform == Player.transform) return ;

        this.TriggerUI().Forget();
    }

    private async UniTask TriggerUI()
    {
        await UniTask.Delay(delayTime);
        this.overlayUI.SetActive(true);
        this.isTriggered = true;
        this.sceneManager.LoadScene("MainMenu");
    }

}
