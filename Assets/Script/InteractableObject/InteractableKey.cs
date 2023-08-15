using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class InteractableKey : MonoBehaviour, IInteractable
{
    public LockableDoor lockableDoor;

    private AudioManager AudioManager => AudioManager.Instance;

    public void Interact()
    {
        this.lockableDoor.Unlock();
        if(this.AudioManager != null)
        {
            Debug.Log("Play Unlock");
            this.AudioManager.Play("Unlock");
        }
        Destroy(this.gameObject);
    }

    public bool IsInteractable()
    {
        return true;
    }
}
