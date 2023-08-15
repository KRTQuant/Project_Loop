using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class InteractableKey : MonoBehaviour, IInteractable
{
    public LockableDoor lockableDoor;

    private AudioManager audioManager => AudioManager.Instance;

    public void Interact()
    {
        this.lockableDoor.Unlock();
        this.audioManager.Play("Unlock");
        Destroy(this.gameObject);
    }

    public bool IsInteractable()
    {
        return true;
    }
}
