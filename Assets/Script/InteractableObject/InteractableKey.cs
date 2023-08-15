using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableKey : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ObjectAudioView audioView;

    public LockableDoor lockableDoor;

    public void Interact()
    {
        this.lockableDoor.Unlock();
        this.audioView.Play("Unlock");
        Destroy(this.gameObject);
    }

    public bool IsInteractable()
    {
        return true;
    }
}
