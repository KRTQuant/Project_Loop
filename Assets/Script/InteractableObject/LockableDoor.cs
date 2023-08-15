using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockableDoor : InteractableDoor
{
    protected bool isUnlock;
    protected GameObject key;

    public void Unlock()
    {
        Debug.Log("Door has been unlock");
        this.isUnlock = true;
    }

    public override void Interact()
    {
        if(!isUnlock)
        {
            this.audioView.Play("Locked");
            return ;  
        }

        base.Interact();
    }

    public override bool IsInteractable()
    {
        return this.isUnlock;
    }
}
