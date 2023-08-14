using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableKey : MonoBehaviour, IInteractable
{
    public LockableDoor lockableDoor;

    public void Interact()
    {
        this.lockableDoor.Unlock();
    }

    public bool IsInteractable()
    {
        return true;
    }
}
