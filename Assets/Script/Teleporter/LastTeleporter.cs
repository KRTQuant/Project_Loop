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
        if (isTriggered || !collision.gameObject.CompareTag("Player")) return ;

        this.TriggerUI().Forget();
    }

    private async UniTask TriggerUI()
    {
        this.overlayUI.SetActive(true);
        await UniTask.Delay(delayTime);
        this.isTriggered = true;
        this.sceneManager.LoadScene("MainMenu");
    }

}
